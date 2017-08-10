using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools.Types;
using Charlotte.Tools;

namespace Charlotte.Test.Tools.Types
{
	public class Base_tTest
	{
		private readonly Int_t Int0 = new Int_t();
		private readonly Int_t Int0_100 = new Int_t(0, 100);
		private readonly JString_t JS_AT = JString_t.CreateAsciiToken();
		private readonly JString_t JS_D1_10 = JString_t.CreateDoc(1, 10);
		private readonly JString_t HissuToken = JString_t.CreateToken(1);
		private readonly JString_t JS_Inited = JString_t.CreateDoc().Init("ABC");

		public void Test01()
		{
			Int0.Value = 100;
			DebugTools.WriteLog("Int0.Value: " + Int0.Value);

			for (int count = -100; count <= 300; count++)
			{
				Int0_100.Value = count;
				//DebugTools.WriteLog("Int0_100.Value: " + Int0_100.Value);
			}

			JS_AT.Value = "ABCあいうDEF";
			DebugTools.WriteLog("JS_AT.Value: " + JS_AT.Value);

			JS_AT.Value = "ABC_☃_";
			DebugTools.WriteLog("JS_AT.Value: " + JS_AT.Value);

			JS_AT.Value = null;
			DebugTools.WriteLog("JS_AT.Value: " + JS_AT.Value);

			JS_D1_10.Value = null;
			DebugTools.WriteLog("JS_D1_10.Value: " + JS_D1_10.Value);

			HissuToken.Value = "";
			DebugTools.WriteLog("HissuToken.Value: " + HissuToken.Value);

			DebugTools.WriteLog("JS_Inited.Value: " + JS_Inited.Value);
		}
	}
}
