using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class JsonTools
	{
		public static String Encode(object src)
		{
			Encoder e = new Encoder();
			e.Add(src);
			return e.Get();
		}

		private class Encoder
		{
			private StringBuilder _buff = new StringBuilder();

			public void Add(object src)
			{
				if (src is ObjectMap)
				{
					ObjectMap om = (ObjectMap)src;
					bool secondOrLater = false;

					_buff.Append("{");

					foreach (string key in om.GetKeys())
					{
						object value = om[key];

						if (secondOrLater)
							_buff.Append(",");

						_buff.Append("\"");
						_buff.Append(key);
						_buff.Append("\":");

						Add(value);

						secondOrLater = true;
					}
					_buff.Append("}");
				}
				else if (src is ObjectList)
				{
					ObjectList ol = (ObjectList)src;
					bool secondOrLater = false;

					_buff.Append("[");

					foreach (object value in ol.GetList())
					{
						if (secondOrLater)
							_buff.Append(",");

						Add(value);

						secondOrLater = true;
					}
					_buff.Append("]");
				}
				else if (src is string)
				{
					String str = (string)src;

					_buff.Append("\"");

					foreach (char chr in str)
					{
						if (chr == '"')
						{
							_buff.Append("\\\"");
						}
						else if (chr == '\\')
						{
							_buff.Append("\\\\");
						}
						else if (chr == '/')
						{
							_buff.Append("\\/");
						}
						else if (chr == '\b')
						{
							_buff.Append("\\b");
						}
						else if (chr == '\f')
						{
							_buff.Append("\\f");
						}
						else if (chr == '\n')
						{
							_buff.Append("\\n");
						}
						else if (chr == '\r')
						{
							_buff.Append("\\r");
						}
						else if (chr == '\t')
						{
							_buff.Append("\\t");
						}
						else
						{
							_buff.Append(chr);
						}
					}
					_buff.Append("\"");
				}
				else
				{
					_buff.Append(src);
				}
			}

			public string Get()
			{
				return _buff.ToString();
			}
		}

		public static object Decode(byte[] src)
		{
			return Decode(GetEncoding(src).GetString(src));
		}

		private static Encoding GetEncoding(byte[] src)
		{
			if (4 <= src.Length)
			{
				string x4 = StringTools.ToHex(ArrayTools.GetPart(src, 0, 4));

				if (x4 == "0000feff" || x4 == "fffe0000")
					return Encoding.UTF32;
			}
			if (2 <= src.Length)
			{
				string x2 = StringTools.ToHex(ArrayTools.GetPart(src, 0, 2));

				if (x2 == "feff" || x2 == "fffe")
					return Encoding.Unicode;
			}

			// TODO BOM が無い場合..

			return Encoding.UTF8;
		}

		public static object Decode(string src)
		{
			return new Decoder(src).Get();
		}

		private class Decoder
		{
			private string _src;
			private int _rPos;

			public Decoder(string src)
			{
				_src = src;
			}

			private char Next()
			{
				return _src[_rPos++];
			}

			private char NextNS()
			{
				char chr;

				do
				{
					chr = Next();
				}
				while (chr <= ' ');

				return chr;
			}

			public object Get()
			{
				char chr = NextNS();

				if (chr == '{')
				{
					ObjectMap om = ObjectMap.CreateIgnoreCase();

					if (NextNS() != '}')
					{
						_rPos--;

						do
						{
							Object key = Get();
							NextNS(); // ':'
							Object value = Get();
							om.Add(key, value);
						}
						while (NextNS() != '}');
					}
					return om;
				}
				if (chr == '[')
				{
					ObjectList ol = new ObjectList();

					if (NextNS() != ']')
					{
						_rPos--;

						do
						{
							ol.Add(Get());
						}
						while (NextNS() != ']');
					}
					return ol;
				}
				if (chr == '"')
				{
					StringBuilder buff = new StringBuilder();

					for (; ; )
					{
						chr = Next();

						if (chr == '"')
						{
							break;
						}
						if (chr == '\\')
						{
							chr = Next();

							if (chr == 'b')
							{
								chr = '\b';
							}
							else if (chr == 'f')
							{
								chr = '\f';
							}
							else if (chr == 'n')
							{
								chr = '\n';
							}
							else if (chr == 'r')
							{
								chr = '\r';
							}
							else if (chr == 't')
							{
								chr = '\t';
							}
							else if (chr == 'u')
							{
								char c1 = Next();
								char c2 = Next();
								char c3 = Next();
								char c4 = Next();

								chr = (char)IntTools.Parse(new string(new char[] { c1, c2, c3, c4 }), 16);
							}
						}
						buff.Append(chr);
					}
					return buff.ToString();
				}

				{
					StringBuilder buff = new StringBuilder();

					buff.Append(chr);

					while (_rPos < _src.Length)
					{
						chr = Next();

						if (
							chr == '}' ||
								chr == ']' ||
								chr == ','
							)
						{
							_rPos--;
							break;
						}
						buff.Append(chr);
					}
					String str = buff.ToString();
					str = str.Trim();
					return new JsonValue(str);
				}
			}
		}
	}
}
