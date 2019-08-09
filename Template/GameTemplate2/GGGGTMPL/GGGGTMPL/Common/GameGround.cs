using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace Charlotte.Common
{
	public static class GameGround
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameTaskList EL = new GameTaskList();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int PrimaryPadId = -1; // -1 == 未設定
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameSubScreen MainScreen = null; // null == 不使用

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreen_W = GameConsts.Screen_W;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreen_H = GameConsts.Screen_H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_L;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_T;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_W = -1; // -1 == RealScreenDraw_LTWH 不使用
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double MusicVolume = GameConsts.DefaultVolume;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double SEVolume = GameConsts.DefaultVolume;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void INIT()
		{
			GameInput.DIR_2.PadBtnId = 0;
			GameInput.DIR_4.PadBtnId = 1;
			GameInput.DIR_6.PadBtnId = 2;
			GameInput.DIR_8.PadBtnId = 3;
			GameInput.A.PadBtnId = 4;
			GameInput.B.PadBtnId = 7;
			GameInput.C.PadBtnId = 5;
			GameInput.D.PadBtnId = 8;
			GameInput.E.PadBtnId = 6;
			GameInput.F.PadBtnId = 9;
			GameInput.L.PadBtnId = 10;
			GameInput.R.PadBtnId = 11;
			GameInput.PAUSE.PadBtnId = 13;
			GameInput.START.PadBtnId = 12;

			GameInput.DIR_2.KbdKeyId = DX.KEY_INPUT_DOWN;
			GameInput.DIR_4.KbdKeyId = DX.KEY_INPUT_LEFT;
			GameInput.DIR_6.KbdKeyId = DX.KEY_INPUT_RIGHT;
			GameInput.DIR_8.KbdKeyId = DX.KEY_INPUT_UP;
			GameInput.A.KbdKeyId = DX.KEY_INPUT_Z;
			GameInput.B.KbdKeyId = DX.KEY_INPUT_X;
			GameInput.C.KbdKeyId = DX.KEY_INPUT_C;
			GameInput.D.KbdKeyId = DX.KEY_INPUT_V;
			GameInput.E.KbdKeyId = DX.KEY_INPUT_A;
			GameInput.F.KbdKeyId = DX.KEY_INPUT_S;
			GameInput.L.KbdKeyId = DX.KEY_INPUT_D;
			GameInput.R.KbdKeyId = DX.KEY_INPUT_F;
			GameInput.PAUSE.KbdKeyId = DX.KEY_INPUT_SPACE;
			GameInput.START.KbdKeyId = DX.KEY_INPUT_RETURN;

			// app > @ INIT

			// < app
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void FNLZ()
		{
			// app > @ FNLZ

			// < app
		}
	}
}
