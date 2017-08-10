using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace AutoBall4
{
	public class WinTools
	{
		public static void PrintScreen(string outPngFile)
		{
			Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
			}
			bmp.Save(outPngFile, ImageFormat.Png);
		}
	}
}
