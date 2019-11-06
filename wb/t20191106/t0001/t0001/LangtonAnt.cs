using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class LangtonAnt
	{
		public class Ant
		{
			public Point Pt;
			public int Direction; // 0-3 == north, west, south, east
			public Color Color;
		}

		public int W = 100;
		public int H = 100;
		public Color BackColor = Color.Black;
		public List<Ant> Ants = new List<Ant>();
		public int ImageCount = 6000;
		public int Skip = 20;

		public void Perform()
		{
			Ant[,] map = new Ant[this.W, this.H];

			for (int count = 0; count < this.ImageCount; count++)
			{
				for (int c = 0; c < this.Skip; c++)
				{
					foreach (Ant ant in this.Ants)
					{
						if (map[ant.Pt.X, ant.Pt.Y] == null)
						{
							ant.Direction++;
							map[ant.Pt.X, ant.Pt.Y] = ant;
						}
						else
						{
							ant.Direction += 3;
							map[ant.Pt.X, ant.Pt.Y] = null;
						}
						ant.Direction %= 4;

						switch (ant.Direction)
						{
							case 0: ant.Pt.Y--; break;
							case 1: ant.Pt.X--; break;
							case 2: ant.Pt.Y++; break;
							case 3: ant.Pt.X++; break;
						}
						ant.Pt.X += this.W;
						ant.Pt.X %= this.W;
						ant.Pt.Y += this.H;
						ant.Pt.Y %= this.H;
					}
				}
				DrawImage(map, count);
			}
		}

		private void DrawImage(Ant[,] map, int count)
		{
			using (Bitmap bmp = new Bitmap(this.W, this.H))
			{
				for (int x = 0; x < this.W; x++)
				{
					for (int y = 0; y < this.H; y++)
					{
						bmp.SetPixel(x, y, map[x, y] == null ? this.BackColor : map[x, y].Color);
					}
				}
				bmp.Save(string.Format(@"C:\temp\{0}.bmp", count));
			}
		}
	}
}
