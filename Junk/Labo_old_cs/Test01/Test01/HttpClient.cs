using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Charlotte
{
	public class HttpClient
	{
		private readonly int TIMEOUT_MILLIS = 30000;
		private readonly string HTTP_PORT = "80";
		private readonly byte[] CRLF = new byte[] { 0x0d, 0x0a };

		private string Domain;
		private string IP;
		private string Port;
		private string Path;

		public HttpClient()
			: this("http://localhost/")
		{ }

		public HttpClient(string url)
		{
			if (url.StartsWith("http://"))
			{
				url = url.Substring(7);
			}
			int pathPos = url.IndexOf('/');

			string domainPart = url.Substring(0, pathPos);
			string path = url.Substring(pathPos);

			int portPos = domainPart.IndexOf(':');

			string domain;
			string port;

			if (portPos != -1)
			{
				domain = domainPart.Substring(0, portPos);
				port = domainPart.Substring(portPos + 1);
			}
			else
			{
				domain = domainPart;
				port = HTTP_PORT;
			}
			this.Domain = domain;
			this.Port = port;
			this.Path = path;

			this.PostCtor();
		}

		public HttpClient(string domain, string port, string path)
		{
			this.Domain = domain;
			this.Port = port;
			this.Path = path;

			this.PostCtor();
		}

		private void PostCtor()
		{
			this.IP = DnsCache.I.GetIP(this.Domain);
		}

		private List<string> HeaderFields = new List<string>();
		private byte[] Body;

		public void AddHeaderField(string headerField)
		{
			this.HeaderFields.Add(headerField);
		}

		public void SetBody(byte[] body)
		{
			this.Body = body;
		}

		public void Perform()
		{
			using (TcpClient client = new TcpClient(this.IP, int.Parse(this.Port)))
			{
				client.SendTimeout = TIMEOUT_MILLIS;
				client.ReceiveTimeout = TIMEOUT_MILLIS;

				using (NetworkStream ns = client.GetStream())
				{
					this.Write(ns, (this.Body == null ? "GET " : "POST ") + this.Path + " HTTP/1.1");
					this.Write(ns, CRLF);
					this.Write(ns, "Host: " + this.Domain + (this.Port == HTTP_PORT ? "" : ":" + this.Port));
					this.Write(ns, CRLF);

					foreach (string headerField in this.HeaderFields)
					{
						this.Write(ns, headerField);
						this.Write(ns, CRLF);
					}
					if (this.Body != null)
					{
						this.Write(ns, "Content-Length: " + this.Body.Length);
						this.Write(ns, CRLF);
					}
					this.Write(ns, CRLF);

					if (this.Body != null)
					{
						this.Write(ns, this.Body);
					}
					this.ParseHeader(this.ReadHeader(ns));

					if (this.ResChunked)
					{
						List<byte[]> bodyParts = new List<byte[]>();

						for (; ; )
						{
							int nextSize = this.ReadChunkedSize(ns);

							if (nextSize == 0)
							{
								break;
							}
							bodyParts.Add(this.ReadChunkedPart(ns, nextSize));
						}
						this.ResBody = this.Join(bodyParts);
					}
					else
					{
						this.ResBody = this.ReadPart(ns, this.ResContentLength);
					}
				}
			}
		}

		private void Write(NetworkStream ns, string str)
		{
			this.Write(ns, Encoding.ASCII.GetBytes(str));
		}

		private void Write(NetworkStream ns, byte[] bytes)
		{
			ns.Write(bytes, 0, bytes.Length);
		}

		private byte Read(NetworkStream ns)
		{
			int chr = ns.ReadByte();

			if (chr == -1)
			{
				throw new Exception("read from closed stream");
			}
			return (byte)chr;
		}

		private void ReadTo(NetworkStream ns, int chr)
		{
			while (this.Read(ns) != chr)
			{ }
		}

		private byte[] ReadHeader(NetworkStream ns)
		{
			byte[] buff = new byte[65000];
			int count = 0;

			for (; ; )
			{
				buff[count++] = this.Read(ns);

				if (
					4 <= count &&
					buff[count - 4] == CRLF[0] &&
					buff[count - 3] == CRLF[1] &&
					buff[count - 2] == CRLF[0] &&
					buff[count - 1] == CRLF[1]
					)
				{
					break;
				}
			}
			byte[] ret = new byte[count];
			Array.Copy(buff, 0, ret, 0, count - 4);
			return ret;
		}

		private void ParseHeader(byte[] headerBlock)
		{
			string header = Encoding.ASCII.GetString(headerBlock);

			header = header.Replace("\r\n", "\n");
			this.ResHeaderFields.Clear();

			foreach (string line in header.Split('\n'))
			{
				if (
					line[0] == ' ' ||
					line[0] == '\t'
					)
				{
					this.ResHeaderFields[this.ResHeaderFields.Count - 1] += line.Trim();
				}
				else
				{
					this.ResHeaderFields.Add(line.Trim());
				}
			}

			this.ResContentLength = 0;
			this.ResChunked = false;

			foreach (string headerField in this.ResHeaderFields)
			{
				int valuePos = headerField.IndexOf(':');

				if (valuePos != -1)
				{
					string key = headerField.Substring(0, valuePos).Trim();
					string value = headerField.Substring(valuePos + 1).Trim();

					if (string.Compare(key, "Content-Length", true) == 0)
					{
						this.ResContentLength = int.Parse(value);
					}
					else if (string.Compare(key, "Transfer-Encoding", true) == 0)
					{
						this.ResChunked = value.ToLower().Contains("chunked");
					}
				}
			}
		}

		private int ReadChunkedSize(NetworkStream ns)
		{
			int size = 0;

			for (; ; )
			{
				int ival = this.Read(ns);

				if (0x30 <= ival && ival <= 0x39)
				{
					ival -= 0x30;
				}
				else if (0x41 <= ival && ival <= 0x46)
				{
					ival -= 0x41;
					ival += 10;
				}
				else if (0x61 <= ival && ival <= 0x66)
				{
					ival -= 0x61;
					ival += 10;
				}
				else
				{
					break;
				}
				size *= 16;
				size += ival;
			}
			this.ReadTo(ns, CRLF[1]);
			return size;
		}

		private byte[] ReadChunkedPart(NetworkStream ns, int size)
		{
			byte[] part = this.ReadPart(ns, size);
			this.ReadTo(ns, CRLF[1]);
			return part;
		}

		private byte[] ReadPart(NetworkStream ns, int size)
		{
			byte[] part = new byte[size];

			for (int count = 0; count < size; )
			{
				int readSize = ns.Read(part, count, size - count);

				if (readSize < 1)
				{
					throw new Exception("read error");
				}
				count += readSize;
			}
			return part;
		}

		private byte[] Join(List<byte[]> partList)
		{
			int count = 0;

			foreach (byte[] part in partList)
			{
				count += part.Length;
			}
			byte[] destBlock = new byte[count];
			count = 0;

			foreach (byte[] part in partList)
			{
				Array.Copy(part, 0, destBlock, count, part.Length);
				count += part.Length;
			}
			return destBlock;
		}

		private List<string> ResHeaderFields = new List<string>();
		private int ResContentLength;
		private bool ResChunked;
		private byte[] ResBody;

		public List<string> GetResHeaderFields()
		{
			return this.ResHeaderFields;
		}

		public byte[] GetResBody()
		{
			return this.ResBody;
		}
	}
}
