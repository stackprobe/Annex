using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameEngine
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long FrameStartTime;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long LangolierTime;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int ProcFrame;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int FreezeInputFrame;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool WindowIsActive;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void CheckHz()
		{
			long currTime = GameSystem.GetCurrTime();

			LangolierTime += 16L;
			LangolierTime = LongTools.Range(LangolierTime, currTime - 100L, currTime + 100L);

			while (currTime < LangolierTime)
			{
				Thread.Sleep(1);

				// DxLib >

				DX.ScreenFlip();

				if (DX.ProcessMessage() == -1)
				{
					throw new Exception("End");
				}

				// < DxLib

				currTime = GameSystem.GetCurrTime();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void EachFrame()
		{
			if (GameSE.EachFrame() == false)
			{
				GameMusic.EachFrame();
			}
			GameGround.EL.ExecuteAllTask();
			GameCurtain.EachFrame();

			// TODO
		}
	}
}
