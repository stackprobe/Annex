using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			int[,] map = ExternalUtils.MakeLikeADungeonMap(24);

			for (; ; )
			{
				ExternalUtils.WriteMap(map);

				{
					I2Point[] pts = AllXY().Where(p => map[p.X, p.Y] == 0).ToArray();

					if (pts.Length == 0)
						throw null; // そんなことある？

					I2Point pt = SecurityTools.CRandom.ChooseOne(pts);
					FillSame(map, pt.X, pt.Y, 2);

					if (AllXY().Where(p => map[p.X, p.Y] == 0).Count() == 0) // ? 閉路は全て繋がった。
					{
						FillSame(map, pt.X, pt.Y, 0);
						break;
					}
				}

				ExternalUtils.WriteMap(map);

				{
					I2Point[] pts = AllXY().Where(p =>
					{
						if (1 <= p.X && p.X < Consts.MAP_W - 1)
						{
							if (map[p.X - 1, p.Y] == 0 && map[p.X, p.Y] == 1 && map[p.X + 1, p.Y] == 2) return true;
							if (map[p.X - 1, p.Y] == 2 && map[p.X, p.Y] == 1 && map[p.X + 1, p.Y] == 0) return true;
						}
						if (1 <= p.Y && p.Y < Consts.MAP_H - 1)
						{
							if (map[p.X, p.Y - 1] == 0 && map[p.X, p.Y] == 1 && map[p.X, p.Y + 1] == 2) return true;
							if (map[p.X, p.Y - 1] == 2 && map[p.X, p.Y] == 1 && map[p.X, p.Y + 1] == 0) return true;
						}
						return false;
					})
					.ToArray();

					if (pts.Length == 0) // ? 他の閉路と壁１つで接していない。
					{
						//AllXY().Where(p => map[p.X, p.Y] == 2).ToList().ForEach(p => map[p.X, p.Y] = 1); // 壁で塗りつぶす。<-- 最大の閉路であった場合マズい。
						//continue;
						break;
					}
					I2Point pt = SecurityTools.CRandom.ChooseOne(pts);
					map[pt.X, pt.Y] = 0;
				}

				ExternalUtils.WriteMap(map);

				{
					I2Point pt = AllXY().Where(p => map[p.X, p.Y] == 2).First();

					FillSame(map, pt.X, pt.Y, 0);
					FillSame(map, pt.X, pt.Y, 2);

					ExternalUtils.WriteMap(map);

					FillSame(map, pt.X, pt.Y, 0);
				}
			}
			ExternalUtils.WriteMap(map);
		}

		private IEnumerable<I2Point> AllXY()
		{
			for (int x = 0; x < Consts.MAP_W; x++)
			{
				for (int y = 0; y < Consts.MAP_H; y++)
				{
					yield return new I2Point(x, y);
				}
			}
		}

		private void FillSame(int[,] map, int x, int y, int valDest)
		{
			int valTarg = map[x, y];

			if (valTarg == valDest)
				throw null;

			Queue<I2Point> seekers = new Queue<I2Point>();

			seekers.Enqueue(new I2Point(x, y));

			while (1 <= seekers.Count)
			{
				I2Point pt = seekers.Dequeue();

				if (
					pt.X < 0 || Consts.MAP_W <= pt.X ||
					pt.Y < 0 || Consts.MAP_H <= pt.Y
					)
					continue;

				if (map[pt.X, pt.Y] != valTarg)
					continue;

				map[pt.X, pt.Y] = valDest;

				seekers.Enqueue(new I2Point(pt.X - 1, pt.Y));
				seekers.Enqueue(new I2Point(pt.X + 1, pt.Y));
				seekers.Enqueue(new I2Point(pt.X, pt.Y - 1));
				seekers.Enqueue(new I2Point(pt.X, pt.Y + 1));
			}
		}
	}
}
