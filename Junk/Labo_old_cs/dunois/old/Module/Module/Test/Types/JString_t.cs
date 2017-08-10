using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools.Types
{
	public class JString_t : Base_t<string>
	{
		public const int COMMON_MAXLEN_TOKEN = 300; // 何かの名前、識別子、ファイル名、ドメイン名 etc.
		public const int COMMON_MAXLEN_LINE = 2000; // テキストファイルの１行 etc.
		public const int COMMON_MAXLEN_DOC = 50000; // 説明文 etc.

		private bool _okJpn;
		private bool _okRet;
		private bool _okTab;
		private bool _okSpc;
		private bool _noTrim;
		private int _minlen;
		private int _maxlen;

		public JString_t(
			bool okJpn = true,
			bool okRet = true,
			bool okTab = true,
			bool okSpc = true,
			bool noTrim = true,
			int minlen = 0,
			int maxlen = COMMON_MAXLEN_DOC
			)
		{
			_okJpn = okJpn;
			_okRet = okRet;
			_okTab = okTab;
			_okSpc = okSpc;
			_noTrim = noTrim;
			_minlen = minlen;
			_maxlen = maxlen;

			this.Set("");
		}

		public override void Set(string value)
		{
			if (value == null)
				value = "";

			value = JString.ToJString(value, _okJpn, _okRet, _okTab, _okSpc, _noTrim, _minlen, _maxlen);
			_value = value;
		}

		public override string Get()
		{
			return _value;
		}

		public JString_t Init(string initval)
		{
			this.Set(initval);
			return this;
		}

		public static JString_t CreateAsciiToken(int minlen = 0, int maxlen = COMMON_MAXLEN_TOKEN)
		{
			return new JString_t(false, false, false, false, false, minlen, maxlen);
		}

		public static JString_t CreateAsciiLine(int minlen = 0, int maxlen = COMMON_MAXLEN_LINE)
		{
			return new JString_t(false, false, false, true, false, minlen, maxlen);
		}

		public static JString_t CreateAsciiText(int minlen = 0, int maxlen = COMMON_MAXLEN_DOC)
		{
			return new JString_t(false, true, false, true, false, minlen, maxlen);
		}

		public static JString_t CreateToken(int minlen = 0, int maxlen = COMMON_MAXLEN_TOKEN)
		{
			return new JString_t(true, false, false, false, false, minlen, maxlen);
		}

		public static JString_t CreateLine(int minlen = 0, int maxlen = COMMON_MAXLEN_LINE)
		{
			return new JString_t(true, false, false, true, false, minlen, maxlen);
		}

		public static JString_t CreateText(int minlen = 0, int maxlen = COMMON_MAXLEN_DOC)
		{
			return new JString_t(true, true, false, true, false, minlen, maxlen);
		}

		public static JString_t CreateDoc(int minlen = 0, int maxlen = COMMON_MAXLEN_DOC)
		{
			return new JString_t(true, true, true, true, true, minlen, maxlen);
		}
	}
}
