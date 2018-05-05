using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.LTools;
using Charlotte.Tests.LTools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{29203080-fb34-423d-93b2-9333bd86c903}";

		static void Main(string[] args)
		{
			LCommon.MainProc(new Program().Main2, APP_IDENT);
		}

		private void Main2(ArgsReader ar)
		{
			new DummyTest().Test01();
		}
	}
}
