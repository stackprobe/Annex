using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;
using System.Drawing.Imaging;
using System.IO;

namespace Charlotte.MkMvs
{
	public class MakeMovie0001
	{
		public static void Main01()
		{
			FileTools.Delete(@"C:\temp\mm");
			FileTools.CreateDir(@"C:\temp\mm\bmp");
			FileTools.CreateDir(@"C:\temp\mm\img");

			DDSubScreen screen = new DDSubScreen(1920, 1080);
			const int frameNum = 200;

			DDPicture mainPic = DDPictureLoaders.Standard(@"C:\wb2\20191204_ジャケット的な\バンドリ_raise_a_suilen.jpg");

			double zx = screen.GetSize().W * 1.0 / mainPic.Get_W();
			double zy = screen.GetSize().H * 1.0 / mainPic.Get_H();

			double z1 = Math.Max(zx, zy);
			double z2 = Math.Min(zx, zy);

			for (int frame = 0; frame < frameNum; frame++)
			{
				double rate = frame * 1.0 / (frameNum - 1);
				double invRate = 1.0 - rate;

				DDSubScreenUtils.ChangeDrawScreen(screen);

				DDCurtain.DrawCurtain();

				DDDraw.DrawBegin(mainPic, screen.GetSize().W / 2, screen.GetSize().H / 2);
				DDDraw.DrawZoom(z1 * (1.0 + rate * 0.2));
				DDDraw.DrawEnd();

#if true
				DDDraw.SetAlpha(0.5);
				DDDraw.SetBright(0, 0, 0);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, screen.GetSize().W, screen.GetSize().H);
				DDDraw.Reset();
#else
				DDCurtain.DrawCurtain(-0.5);
#endif

				DDDraw.DrawBegin(mainPic, screen.GetSize().W / 2, screen.GetSize().H / 2);
				DDDraw.DrawZoom(z2 * (1.0 + invRate * 0.1));
				DDDraw.DrawEnd();

				DX.SaveDrawScreen(0, 0, screen.GetSize().W, screen.GetSize().H, string.Format(@"C:\temp\mm\bmp\{0}.bmp", frame));

				DDSubScreenUtils.RestoreDrawScreen();

				DDEngine.EachFrame();
			}

			mainPic.Unload();

			for (int frame = 0; frame < frameNum; frame++)
			{
				new Canvas2(string.Format(@"C:\temp\mm\bmp\{0}.bmp", frame)).Save(string.Format(@"C:\temp\mm\img\{0}.jpg", frame), ImageFormat.Jpeg, 90);
			}

			ProcessTools.Batch(new string[]
			{
				@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r 20 -i %%d.jpg ..\out.mp4",
			},
			@"C:\temp\mm\img"
			);

			File.Copy(@"C:\temp\mm\out.mp4", @"C:\temp\mm.mp4", true);

			DDEngine.EachFrame();
		}
	}
}
