using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class AttachString
	{
		public static AttachString I = new AttachString();

		public AttachString()
			: this("\t\r\n ", "trns")
		{ }

		public AttachString(string decChrs, string encChrs)
			: this(':', '$', '.', decChrs, encChrs)
		{ }

		public AttachString(char delimiter, char escapeChr, char escapeDelimiter)
			: this(delimiter, escapeChr, escapeDelimiter, "", "")
		{ }

		private char _delimiter;
		private EscapeString _es;

		public AttachString(char delimiter, char escapeChr, char escapeDelimiter, string decChrs, string encChrs)
			: this(delimiter, new EscapeString(
				decChrs + delimiter,
				escapeChr,
				encChrs + escapeDelimiter
				))
		{ }

		public AttachString(char delimiter, EscapeString es)
		{
			_delimiter = delimiter;
			_es = es;
		}

		public string Untokenize(List<string> tokens)
		{
			List<string> tmp = new List<string>();

			foreach (string token in tokens)
				tmp.Add(_es.Encode(token));

			tmp.Add("");
			return string.Join("" + _delimiter, tmp);
		}

		public List<string> Tokenize(string str)
		{
			List<string> ret = new List<string>();

			foreach (string token in str.Split(_delimiter))
				ret.Add(_es.Decode(token));

			ret.RemoveAt(ret.Count - 1);
			return ret;
		}
	}
}
