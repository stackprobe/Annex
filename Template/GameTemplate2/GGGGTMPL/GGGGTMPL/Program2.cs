﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			GameAdditionalEvents.Ground_INIT = () =>
			{
				ProcMain.WriteLog("Ground_INIT");

				//GameGround.RO_MouseDispMode = true;
			};

			GameAdditionalEvents.Ground_FNLZ = () =>
			{
				ProcMain.WriteLog("Ground_FNLZ");
			};

			GameAdditionalEvents.PostGameStart = () =>
			{
				ProcMain.WriteLog("PostGameStart");

				// Font >

				//GameFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");

				// < Font

				Ground.I = new Ground();
			};

			GameAdditionalEvents.Save = lines =>
			{
				lines.Add(DateTime.Now.ToString()); // Dummy
			};

			GameAdditionalEvents.Load = lines =>
			{
				int c = 0;

				GameUtils.Noop(lines[c++]); // Dummy
			};

			GameMain2.Perform(Main4);
		}

		private void Main4()
		{
			new Test0001().Test01();
		}
	}
}
