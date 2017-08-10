using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class HttpRequestTest
	{
		public static void Test01()
		{
			GetTest("http://www.4chan.org/");
			GetTest("http://www.nicovideo.jp/");
			GetTest("http://homepage2.nifty.com/natupaji/DxLib/dxfunc.html");
		}

		private static void GetTest(string url)
		{
			HttpRequest hr = new HttpRequest(url);
			hr.SetHeaderField("X-Header", "Value 123 ABC");
			hr.SetIEProxy();
			HttpResponse res = hr.Get();

			DebugTools.WriteLog(url);

			foreach (string name in res.GetHeaderFields().Keys)
				DebugTools.WriteLog(name + ": " + res.GetHeaderFields()[name]);

			DebugTools.WriteLog(StringTools.ENCODING_SJIS.GetString(res.GetBody()));
		}
	}
}
