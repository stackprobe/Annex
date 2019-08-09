using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace AutoBall4
{
	public static class WinTools
	{
		public static void PrintScreen(string outPngFile)
		{
			const int SCREEN_L = 0;
			const int SCREEN_T = 0;
			const int SCREEN_W = 1920;
			const int SCREEN_H = 1080;

			Bitmap bmp = new Bitmap(SCREEN_W, SCREEN_H);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(new Point(SCREEN_L, SCREEN_T), new Point(0, 0), bmp.Size);
			}
			bmp.Save(outPngFile, ImageFormat.Png);
		}
	}
}
