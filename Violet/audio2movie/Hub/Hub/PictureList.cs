using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Charlotte.Tools;

namespace Charlotte
{
	public class PictureList
	{
		public string OutputDir = @"out\PictureList";
		public string ImageFile = @"res\Singing.jpg";
		public int TotalTimeCentisecond = 500;
		public string Title1 = "２０１１年１２月３日公開";
		public string Title2 = "映画『けいおん』";
		public string Title3 = "エンディングテーマ";
		public string Title4 = "Singing!";
		public int Title2Size = 128;
		public int Title4Size = 256;
		public int HalfCurtainWPct = 70;
		public string Singer1 = "放課後ティータイム";
		public string Singer2 = "日笠陽子・豊崎愛生・佐藤聡美・寿美菜子・竹達彩奈";
		public int Singer2Size = 64;
		public int SingerCurtainHPct = 30;
		public int SingerCurtainAPct = 50;

		// <---- prm

		public void Init()
		{
			FileTools.Delete(this.OutputDir);
			FileTools.CreateDir(this.OutputDir);
		}

		private class TitleBox
		{
			public int Frame = 0;
			public double X;
			public double Y;
			public double Alpha = 0.0;
			public double DestX;
			public double DestY;
			public double DestAlpha = 1.0;
			public string Text;
			public int FontSize;

			public TitleBox(double x, double y, string text, int fontSize)
			{
				this.X = x + 200.0;
				this.Y = y + 100.0;
				this.DestX = x;
				this.DestY = y;
				this.Text = text;
				this.FontSize = fontSize;
			}

			public bool EachFrame() // ret: ? dead
			{
				this.Frame++;

				if (this.Frame == 80)
					return true;

				if (this.Frame == 50)
				{
					this.DestAlpha = 0.0;
				}

				this.DestX -= 2.0;

				CommonUtils.Approach(ref this.X, this.DestX, 0.8);
				CommonUtils.Approach(ref this.Y, this.DestY, 0.8);
				CommonUtils.Approach(ref this.Alpha, this.DestAlpha, 0.9);

				return false;
			}

			public void DrawText(Image picture)
			{
				using (Graphics g = Graphics.FromImage(picture))
				{
					g.DrawString(
						this.Text,
						new Font("游ゴシック", (float)this.FontSize, FontStyle.Bold),
						new SolidBrush(Color.FromArgb((int)(this.Alpha * 255.0), 255, 255, 255)),
						(float)this.X,
						(float)this.Y
						);
				}
			}
		}

		private Image Picture = null;
		private int Frame = 0;
		private List<TitleBox> TitleBoxes = new List<TitleBox>();

		public void Perform()
		{
			// ---- フィールド初期化

			this.SingerCurtainA = this.SingerCurtainAPct / 100.0;
			this.SingerCurtainA_Real = this.SingerCurtainA;

			// ----

			int frameCount = (int)((this.TotalTimeCentisecond / 100.0) * Consts.FPS + 1);

			for (int c = 0; c < 210; c++)
			{
				this.CreatePicture();
				this.DrawImage();

				if (c < 10)
				{
					this.DrawCurtain(1.0);
				}
				else if (c < 30)
				{
					this.DrawCurtain((30 - c) / 20.0);
				}
				else
				{
					// noop
				}

				if (c < 80)
				{
					this.DrawHalfCurtain(1.0);
				}
				else if (c < 100)
				{
					this.DrawHalfCurtain((100 - c) / 20.0);
				}

				if (c == 120)
				{
					this.SingerCurtainH = (Consts.PICTURE_H * this.SingerCurtainHPct) / 100.0;
				}
				if (c == 180)
				{
					//this.SingerCurtainH = 0.0;
					this.SingerCurtainA = 0.0;
				}
				this.DrawSingerCurtain();

				if (c == 10)
				{
					this.TitleBoxes.Add(new TitleBox(100.0, 200.0, this.Title1, 32));
				}
				if (c == 15)
				{
					this.TitleBoxes.Add(new TitleBox(100.0, 300.0, this.Title2, this.Title2Size));
				}
				if (c == 20)
				{
					this.TitleBoxes.Add(new TitleBox(100.0, 700.0, this.Title3, 48));
				}
				if (c == 25)
				{
					this.TitleBoxes.Add(new TitleBox(100.0, 800.0, this.Title4, this.Title4Size));
				}

				if (c == 120)
				{
					this.TitleBoxes.Add(new TitleBox(100.0, 20.0, this.Singer1, 128));
				}
				if (c == 125)
				{
					this.TitleBoxes.Add(new TitleBox(150.0, 250.0, this.Singer2, this.Singer2Size));
				}

				for (int index = 0; index < this.TitleBoxes.Count; index++)
				{
					if (this.TitleBoxes[index].EachFrame())
					{
						this.TitleBoxes.RemoveAt(index);
						index--;
					}
				}

				foreach (TitleBox tb in this.TitleBoxes)
				{
					tb.DrawText(this.Picture);
				}

				this.WritePicture();
			}

			{
				this.CreatePicture();
				this.DrawImage();
				this.WritePicture();
			}

			while (this.Frame < frameCount - 30)
			{
				this.CopyLastPicture();
			}
			while (this.Frame < frameCount)
			{
				this.CreatePicture();
				this.DrawImage();

				if (this.Frame < frameCount - 10)
				{
					this.DrawCurtain((this.Frame - (frameCount - 30)) / 20.0);
				}
				else
				{
					this.DrawCurtain(1.0);
				}

				this.WritePicture();
			}
		}

		private void CreatePicture()
		{
			this.Picture = new Bitmap(Consts.PICTURE_W, Consts.PICTURE_H);

			using (Graphics g = Graphics.FromImage(this.Picture))
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), 0, 0, Consts.PICTURE_W, Consts.PICTURE_H);
			}
		}

		private void DrawImage()
		{
			using (Image img = Bitmap.FromFile(this.ImageFile))
			{
				{
					int w = img.Width;
					int h = img.Height;

					{
						int ww = Consts.PICTURE_W;
						int hh = (h * ww) / w;

						if (hh < Consts.PICTURE_H)
						{
							hh = Consts.PICTURE_H;
							ww = (w * hh) / h;
						}

						w = ww;
						h = hh;
					}

					int l = (Consts.PICTURE_W - w) / 2;
					int t = (Consts.PICTURE_H - h) / 2;

					using (Graphics g = Graphics.FromImage(this.Picture))
					{
						g.DrawImage(img, l, t, w, h);
						g.FillRectangle(new SolidBrush(Color.FromArgb(127, 0, 0, 0)), 0, 0, Consts.PICTURE_W, Consts.PICTURE_H);
					}
				}

				{
					int w = img.Width;
					int h = img.Height;

					{
						int ww = Consts.PICTURE_W;
						int hh = (h * ww) / w;

						if (Consts.PICTURE_H < hh)
						{
							hh = Consts.PICTURE_H;
							ww = (w * hh) / h;
						}

						w = ww;
						h = hh;
					}

					int l = (Consts.PICTURE_W - w) / 2;
					int t = (Consts.PICTURE_H - h) / 2;

					using (Graphics g = Graphics.FromImage(this.Picture))
					{
						g.DrawImage(img, l, t, w, h);
					}
				}
			}
		}

		private void DrawCurtain(double alpha)
		{
			using (Graphics g = Graphics.FromImage(this.Picture))
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb((int)(alpha * 255.0), 0, 0, 0)), 0, 0, Consts.PICTURE_W, Consts.PICTURE_H);
			}
		}

		private void DrawHalfCurtain(double alpha)
		{
			int w = (Consts.PICTURE_W * HalfCurtainWPct) / 100 - this.Frame;

			alpha *= 0.8;

			using (Graphics g = Graphics.FromImage(this.Picture))
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb((int)(alpha * 255.0), 0, 0, 64)), 0, 0, w, Consts.PICTURE_H);
			}
		}

		private double SingerCurtainH = 0.0;
		private double SingerCurtainH_Real = 0.0;
		private double SingerCurtainA;
		private double SingerCurtainA_Real;

		private void DrawSingerCurtain()
		{
			CommonUtils.Approach(ref this.SingerCurtainH_Real, this.SingerCurtainH, 0.9);
			CommonUtils.Approach(ref this.SingerCurtainA_Real, this.SingerCurtainA, 0.9);

			if (this.SingerCurtainH_Real < 1.0)
				return;

			using (Graphics g = Graphics.FromImage(this.Picture))
			{
				g.FillRectangle(new SolidBrush(Color.FromArgb((int)(this.SingerCurtainA_Real * 255.0), 0, 0, 0)),
					0, 0, Consts.PICTURE_W, DoubleTools.ToInt(this.SingerCurtainH_Real));
			}
		}

		private string LastPictureFile = null;

		private void WritePicture()
		{
			string file = this.GetPictureFile(this.Frame);

			// 縮小
			{
				int w = Consts.PICTURE_W / Consts.V_倍;
				int h = Consts.PICTURE_H / Consts.V_倍;

				Image dest = new Bitmap(w, h);

				using (Graphics g = Graphics.FromImage(dest))
				{
					g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					g.DrawImage(this.Picture, 0, 0, w, h);
				}
				this.Picture.Dispose();
				this.Picture = dest;
			}

			using (EncoderParameters eps = new EncoderParameters(1))
			using (EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)90))
			{
				eps.Param[0] = ep;
				ImageCodecInfo ici = GetICI(ImageFormat.Jpeg);
				this.Picture.Save(file, ici, eps);
			}
			this.Picture.Dispose();
			this.Picture = null;
			this.Frame++;

#if false // old -- 遅い
			// 縮小
			{
				int w = Consts.PICTURE_W / Consts.V_倍;
				int h = Consts.PICTURE_H / Consts.V_倍;

				ProcessTools.Batch(new string[]
				{
					string.Format(@"C:\app\Kit\ImgTools\ImgTools.exe /rwf ""{0}"" /e {1} {2}", file, w, h),
				});
			}
#endif

			this.LastPictureFile = file;
		}

		private ImageCodecInfo GetICI(ImageFormat imgFmt)
		{
			return (from ici in ImageCodecInfo.GetImageEncoders() where ici.FormatID == imgFmt.Guid select ici).ToList()[0];
		}

		private void CopyLastPicture()
		{
			File.Copy(this.LastPictureFile, this.GetPictureFile(this.Frame));
			this.Frame++;
		}

		private string GetPictureFile(int frame)
		{
			return Path.Combine(this.OutputDir, string.Format("{0:D8}", frame) + ".jpg");
		}
	}
}
