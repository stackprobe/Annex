using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class EscapeString
	{
		public static EscapeString I = new EscapeString();

		public EscapeString()
			: this("\t\r\n ", '$', "trns")
		{ }

		private string _decChrs;
		private char _escapeChr;
		private string _encChrs;

		public EscapeString(string decChrs, char escapeChr, string encChrs)
		{
			if (
				decChrs == null ||
				encChrs == null ||
				decChrs.Length != encChrs.Length ||
				HasSameChar(decChrs + escapeChr + encChrs)
				)
				throw new Exception("args");

			_decChrs = decChrs + escapeChr;
			_escapeChr = escapeChr;
			_encChrs = encChrs + escapeChr;
		}

		private static bool HasSameChar(string chrs)
		{
			for (int r = 1; r < chrs.Length; r++)
				for (int l = 0; l < r; l++)
					if (chrs[l] == chrs[r])
						return true;

			return false;
		}

		public string Encode(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach(char chr in str)
			{
				int chrPos = _decChrs.IndexOf(chr);

				if (chrPos == -1)
				{
					buff.Append(chr);
				}
				else
				{
					buff.Append(_escapeChr);
					buff.Append(_encChrs[chrPos]);
				}
			}
			return buff.ToString();
		}

		public string Decode(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == _escapeChr && index + 1 < str.Length)
				{
					index++;
					chr = str[index];
					int chrPos = _encChrs.IndexOf(chr);

					if (chrPos != -1)
						chr = _decChrs[chrPos];
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
