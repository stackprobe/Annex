using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.MkMvs
{
	public class MkMv
	{
		public static void Main01()
		{
			DDSubScreen screen = new DDSubScreen(1920, 1080);

			DDPicture wall = DDPictureLoaders.Standard(@"C:\wb2\20191204_ジャケット的な\スタァライト_星見純那.jpg");

			for (int c = 0; c < 100; c++)
			{
				DDSubScreenUtils.ChangeDrawScreen(screen);

				DDCurtain.DrawCurtain();

				DDDraw.DrawBegin(wall, screen.GetSize().W / 2, screen.GetSize().H / 2);
				DDDraw.DrawZoom(1.0 + c / 100.0);
				DDDraw.DrawEnd();

				DX.SaveDrawScreen(0, 0, screen.GetSize().W, screen.GetSize().H, @"C:\temp\" + c + ".bmp");

				DDSubScreenUtils.RestoreDrawScreen();

				DDEngine.EachFrame();
			}

			wall.Unload();
		}
	}
}
