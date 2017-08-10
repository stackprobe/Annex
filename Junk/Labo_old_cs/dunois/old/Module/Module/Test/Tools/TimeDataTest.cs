using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class TimeDataTest
	{
		public static void Test01()
		{
			DebugTools.WriteLog(TimeData.Now.ToString());
			DebugTools.WriteLog(new TimeData(1, 1, 1).ToString());
			DebugTools.WriteLog(new TimeData(9999, 12, 31, 23, 59, 59).ToString());
			DebugTools.WriteLog(new TimeData(0L).ToString());
			DebugTools.WriteLog(new TimeData(long.MaxValue).ToString());

			DebugTools.WriteLog(TimeData.Now.GetDateTime().ToString());
			DebugTools.WriteLog(new TimeData(1, 1, 1).GetDateTime().ToString());
			DebugTools.WriteLog(new TimeData(9999, 12, 31, 23, 59, 59).GetDateTime().ToString());
			DebugTools.WriteLog(new TimeData(0L).GetDateTime().ToString());
			//DebugTools.WriteLog(new TimeData(long.MaxValue).GetDateTime().ToString()); // 例外

			DebugTools.WriteLog("DateTime max = " + GetMaxTimeDataOfDateTime().GetDateTime().ToString());
		}

		private static TimeData GetMaxTimeDataOfDateTime()
		{
			TimeData b = new TimeData(0L);
			TimeData e = new TimeData(long.MaxValue / 2L);

			while (b.GetTime() + 1 < e.GetTime())
			{
				TimeData m = new TimeData((b.GetTime() + e.GetTime()) / 2L);

				try
				{
					m.GetDateTime();
					b = m;
				}
				catch
				{
					e = m;
				}
			}
			return b;
		}
	}
}
