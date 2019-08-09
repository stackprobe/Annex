using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class Consts
	{
		public const int FORM_INITED_TIMER_COUNT = 3;

		public const int KEY_NAME_LEN_MIN = 1;
		public const int KEY_NAME_LEN_MAX = 100;

		public const int KEY_BUNDLE_NAME_LEN_MIN = 1;
		public const int KEY_BUNDLE_NAME_LEN_MAX = 100;

		public const int PASSPHRASE_LEN_MIN = 1;
		public const int PASSPHRASE_LEN_MAX = 100;

		public const string DUMMY_KEY_NAME = "dummy-key";
		public static readonly string DUMMY_IDENT = StringTools.repeat("0", 32);
		public static readonly string DUMMY_RAW_KEY = StringTools.repeat("0", 128);

		public enum DropImageSize_e
		{
			_128 = 128,
			_256 = 256,
			_512 = 512,
		}

		public enum Combination_e
		{
			AND = 1,
			OR,
		}
	}
}
