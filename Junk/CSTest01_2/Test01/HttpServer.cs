using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Charlotte
{
	public class HttpServer
	{
		private readonly int TIMEOUT_MILLIS = 20000;

		private int PortNo;
		private TcpListener Server;
		private Thread STh;

		public HttpServer()
			: this(80)
		{ }

		public HttpServer(int portno)
		{
			this.PortNo = portno;

			this.Server = new TcpListener(IPAddress.Any, this.PortNo);
			this.Server.Start();

			this.STh = new Thread(this.ServerTh);
			this.STh.Start();
		}

		public void End()
		{
			this.DeadFlag = true;

			using (TcpClient client = new TcpClient("localhost", this.PortNo))
			{ }

			this.STh.Join();
		}

		private bool DeadFlag;

		public void ServerTh()
		{
			for (; ; )
			{
				try
				{
					using (TcpClient client = this.Server.AcceptTcpClient())
					{
						if (this.DeadFlag)
						{
							break;
						}
						client.SendTimeout = TIMEOUT_MILLIS;
						client.ReceiveTimeout = TIMEOUT_MILLIS;

						using (NetworkStream ns = client.GetStream())
						{
							foreach (byte[] sendPart in this.GetSendData())
							{
								ns.Write(sendPart, 0, sendPart.Length);
							}
							this.ReadToEnd(ns);
						}
					}
				}
				catch
				{ }
			}
		}

		private void ReadToEnd(NetworkStream ns)
		{
			for (; ; )
			{
				int chr = ns.ReadByte();

				if (chr == -1)
				{
					break;
				}
			}
		}

		private byte[][] GetSendData()
		{
			byte[] body = Encoding.UTF8.GetBytes("<html><body><h1>It's works</h1></body></html>");

			return new byte[][]
			{
				Encoding.ASCII.GetBytes(
					"HTTP/1.1 200 OK\r\n" +
					"Content-Type: text/html; charset=UTF-8\r\n" +
					"Content-Length: " + body.Length + "\r\n" +
					"\r\n"
					),
				body,
			};
		}
	}
}
