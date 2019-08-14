using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				GameMain2.Perform(Main3);
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			for (; ; )
			{
				GameEngine.EachFrame();
			}
		}
	}
}
