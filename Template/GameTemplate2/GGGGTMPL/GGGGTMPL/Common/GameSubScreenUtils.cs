﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public static class GameSubScreenUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static List<GameSubScreen> SubScreens = new List<GameSubScreen>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Add(GameSubScreen subScreen)
		{
			SubScreens.Add(subScreen);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Remove(GameSubScreen subScreen) // ret: ? ! Already removed
		{
			return GameUtils.FastDesertElement(SubScreens, i => i == subScreen) != null;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void UnloadAll()
		{
			foreach (GameSubScreen subScreen in SubScreens)
			{
				subScreen.Unload();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int CurrDrawScreenHandle = DX.DX_SCREEN_BACK;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ChangeDrawScreen(int handle)
		{
			if (DX.SetDrawScreen(handle) != 0) // ? 失敗
				throw new Exception("Failed to DX.SetDrawScreen()");

			CurrDrawScreenHandle = handle;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ChangeDrawScreen(GameSubScreen subScreen)
		{
			ChangeDrawScreen(subScreen.GetHandle());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void RestoreDrawScreen()
		{
			ChangeDrawScreen(GameGround.MainScreen != null ? GameGround.MainScreen.GetHandle() : DX.DX_SCREEN_BACK);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Size GetDrawScreenSize() // ret: 描画領域のサイズ？
		{
			int w;
			int h;
			int cbd;

			if (DX.GetScreenState(out w, out h, out cbd) != 0)
				throw new Exception("Failed to DX.GetScreenState()");

			if (w < 1 || IntTools.IMAX < w)
				throw new Exception("Bad w: " + w);

			if (h < 1 || IntTools.IMAX < h)
				throw new Exception("Bad h: " + h);

			return new Size(w, h);
		}
	}
}
