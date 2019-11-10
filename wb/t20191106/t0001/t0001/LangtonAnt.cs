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

			// <---- prm

			public Point LastPt;
			public Color ColorNew;
		}

		public int W = 100;
		public int H = 100;
		public Color BackColor = Color.Black;
		public List<Ant> Ants = new List<Ant>();
		public int ImageCount = 1200;
		public int StepPerImage = 100;

		// <---- prm

		public void Perform()
		{
			Color[,] map = new Color[this.W, this.H];

			for (int x = 0; x < this.W; x++)
				for (int y = 0; y < this.H; y++)
					map[x, y] = this.BackColor;

			for (int count = 0; count < this.ImageCount; count++)
			{
				for (int c = 0; c < this.StepPerImage; c++)
				{
					foreach (Ant ant in this.Ants)
					{
						ant.LastPt = ant.Pt;

						if (map[ant.Pt.X, ant.Pt.Y] == this.BackColor)
						{
							ant.Direction++;
							ant.ColorNew = ant.Color;
						}
						else
						{
							ant.Direction += 3;
							ant.ColorNew = this.BackColor;
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
					foreach (Ant ant in this.Ants)
						map[ant.LastPt.X, ant.LastPt.Y] = ant.ColorNew;
				}
				DrawImage(map, count);
			}
		}

		private void DrawImage(Color[,] map, int count)
		{
			using (Bitmap bmp = new Bitmap(this.W, this.H))
			{
				for (int x = 0; x < this.W; x++)
					for (int y = 0; y < this.H; y++)
						bmp.SetPixel(x, y, map[x, y]);

				bmp.Save(string.Format(@"C:\temp\{0}.bmp", count));
			}
		}
	}
}
