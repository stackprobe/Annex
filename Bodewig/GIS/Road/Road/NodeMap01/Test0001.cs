using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte.NodeMap01
{
	public class Test0001
	{
		public Map Map;

		// <---- prms

		public void Test01()
		{
			NodeMap nm = new NodeMap() { Map = Map };

			nm.Load();
			nm.Output();
		}

		public void Test02()
		{
			NodeMap nm = new NodeMap() { Map = Map };

			nm.Load();
			//nm.全部つながっているかどうか確認する(); // 繋がっていない！
			nm.島を数える();
		}

		public void Test03()
		{
			NodeMap nm = new NodeMap() { Map = Map };

			nm.Load();

			int minX = IntTools.IMAX;
			int minY = IntTools.IMAX;
			int maxX = -1;
			int maxY = -1;

			foreach (NodeMap.Node n in nm.Ns.Values)
			{
				minX = Math.Min(minX, n.X);
				minY = Math.Min(minY, n.Y);
				maxX = Math.Max(maxX, n.X);
				maxY = Math.Max(maxY, n.Y);
			}

			Console.WriteLine(minX + " - " + maxX + "  =>  " + (maxX - minX));
			Console.WriteLine(minY + " - " + maxY + "  =>  " + (maxY - minY));
		}

		// 4975948 - 5070208  =>  94260
		// 1256630 - 1338110  =>  81480

		public void Test04()
		{
			NodeMap nm = new NodeMap() { Map = Map };

			nm.Load();

			// 南が上になってね？

			int X_MIN = 4975000;
			int Y_MIN = 1256000;

			for (int xc = 0; xc < 19; xc++)
			{
				for (int yc = 0; yc < 17; yc++)
				{
					Console.WriteLine(xc + ", " + yc);

					int l = X_MIN + xc * 5000;
					int t = Y_MIN + yc * 5000;
					int r = l + 5000;
					int b = t + 5000;

					using (Bitmap bmp = new Bitmap(5000, 5000))
					{
						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.Clear(Color.White);

							foreach (NodeMap.Node n in nm.Ns.Values)
							{
								if (
									l <= n.X && n.X < r &&
									t <= n.Y && n.Y < b
									)
								{
									int x1 = n.X - l;
									int y1 = n.Y - t;

									foreach (NodeMap.Node link in n.Links.Values)
									{
										int x2 = link.X - l;
										int y2 = link.Y - t;

										g.DrawLine(new Pen(Color.Black, 3), new Point(x1, y1), new Point(x2, y2));
									}
								}
							}
						}
						bmp.Save(string.Format(@"C:\temp\{0:D2}_{1:D2}_{2}_{3}.png"
							, xc
							, yc
							, ((l + r) / 2) / 36000.0
							, ((t + b) / 2) / 36000.0
							));
					}
				}
			}
		}
	}
}
