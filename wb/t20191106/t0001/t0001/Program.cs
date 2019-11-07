using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{ff19a1a5-f288-476b-805f-bca73f1a3c18}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			Test01();
		}

		private string OutputDir;

		private void Test01()
		{
			OutputDir = ExtraTools.MakeFreeDir();

			Test01_Main();

			ProcessTools.Batch(new string[] { "START " + OutputDir });
		}

		private void Test01_Main()
		{
			const int D = 20;

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2, la.H / 2),
					Direction = 0,
					Color = Color.White,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2),
					Direction = 0,
					Color = Color.Red,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2),
					Direction = 0,
					Color = Color.Blue,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2),
					Direction = 0,
					Color = Color.Red,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2),
					Direction = 2,
					Color = Color.DarkCyan,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 - D),
					Direction = 0,
					Color = Color.Red,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 + D),
					Direction = 0,
					Color = Color.Blue,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 - D),
					Direction = 0,
					Color = Color.Red,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 + D),
					Direction = 2,
					Color = Color.DarkCyan,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 - D),
					Direction = 0,
					Color = Color.Red,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 + D),
					Direction = 0,
					Color = Color.Green,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 + D),
					Direction = 0,
					Color = Color.Yellow,
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 - D),
					Direction = 0,
					Color = Color.Blue,
				});
			});

			Test01_a(la =>
			{
				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 - D),
					Direction = 0,
					Color = Color.FromArgb(105, 0, 45),
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 - D, la.H / 2 + D),
					Direction = 1,
					Color = Color.FromArgb(155, 0, 115),
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 + D),
					Direction = 2,
					Color = Color.FromArgb(255, 0, 255),
				});

				la.Ants.Add(new LangtonAnt.Ant()
				{
					Pt = new Point(la.W / 2 + D, la.H / 2 - D),
					Direction = 3,
					Color = Color.FromArgb(205, 0, 185),
				});
			});
		}

		private int OutputIndex = 0;

		private void Test01_a(Action<LangtonAnt> initLa)
		{
			ProcessTools.Batch(new string[] { @"C:\home\bat\onboot\clean_temp.bat" }); // Clear C:\temp

			Console.WriteLine("*1");

			{
				LangtonAnt la = new LangtonAnt();

				initLa(la);

				la.Perform();
			}

			Console.WriteLine("*2");

			using (WorkingDir wd = new WorkingDir())
			{
				string imgDir = wd.MakePath();

				FileTools.CreateDir(imgDir);

				foreach (string file in Directory.GetFiles(@"C:\temp"))
				{
					Canvas canvas = new Canvas(file);
					canvas = ExpandNearest(canvas, 3);
					canvas.Save(Path.Combine(imgDir, Path.GetFileNameWithoutExtension(file) + ".jpg"), ImageFormat.Jpeg, 100);
				}

				ProcessTools.Batch(new string[]
				{
					string.Format(@"C:\app\ffmpeg-4.1.3-win64-shared\bin\ffmpeg.exe -r 20 -i %%d.jpg {0}\LangtonAnt_{1}.mp4", OutputDir, OutputIndex.ToString("D3")),
				},
				imgDir
				);
			}
			OutputIndex++;

			Console.WriteLine("*3");
		}

		private Canvas ExpandNearest(Canvas src, int mul)
		{
			Canvas dest = new Canvas(src.GetWidth() * mul, src.GetHeight() * mul);

			for (int x = 0; x < dest.GetWidth(); x++)
			{
				for (int y = 0; y < dest.GetHeight(); y++)
				{
					dest.Set(x, y, src.Get(x / mul, y / mul));
				}
			}
			return dest;
		}
	}
}
