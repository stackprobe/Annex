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
				this.Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			GameMain.GameStart();
			try
			{
				this.Main4();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
			finally
			{
				GameMain.GameEnd();
			}
		}

		private void Main4()
		{
			// TODO
		}
	}
}
