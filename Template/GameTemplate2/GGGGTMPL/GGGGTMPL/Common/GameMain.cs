using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	public static class GameMain
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void GameStart()
		{
			// *.INIT
			{
				GameGround.INIT();
				GameResource.INIT();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void GameEnd()
		{
			// *.FNLZ
			{
				GameResource.FNLZ();
				GameGround.FNLZ();
			}
		}
	}
}
