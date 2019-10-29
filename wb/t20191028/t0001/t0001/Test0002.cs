using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0002
	{
		public void Test01()
		{
			int[,] map = ExternalUtils.MakeLikeADungeonMap(24);

			for (; ; )
			{
				ExternalUtils.WriteMap(map);

				// 最小の閉路を選択する。
				{
					List<I2Point> 閉路Pts = new List<I2Point>();

					foreach (I2Point pt in AllXY())
					{
						if (map[pt.X, pt.Y] == 0)
						{
							FillSame(map, pt.X, pt.Y, 2);
							閉路Pts.Add(pt);
						}
					}
					AllXY().Where(p => map[p.X, p.Y] == 2).ToList().ForEach(p => map[p.X, p.Y] = 0);

					I2Point smallestPt = new I2Point(-1, 0);
					int smallestSize = int.MaxValue;

					foreach (I2Point pt in 閉路Pts)
					{
						FillSame(map, pt.X, pt.Y, 2);

						int size = AllXY().Where(p => map[p.X, p.Y] == 2).Count();

						if (size < smallestSize)
						{
							smallestPt = pt;
							smallestSize = size;
						}
						FillSame(map, pt.X, pt.Y, 0);
					}
					if (smallestPt.X == -1)
						throw null; // そんなことある？

					FillSame(map, smallestPt.X, smallestPt.Y, 2);

					if (AllXY().Where(p => map[p.X, p.Y] == 0).Count() == 0) // ? 閉路は全て繋がった。
					{
						FillSame(map, smallestPt.X, smallestPt.Y, 0);
						break;
					}
				}

				ExternalUtils.WriteMap(map);

				{
					I2Point[] pts = AllXY().Where(p =>
					{
#if true
						if (map[p.X, p.Y] == 1)
						{
							bool f0 = false;
							bool f2 = false;

							foreach (I2Point q in new I2Point[]
							{
								new I2Point(p.X - 1, p.Y),
								new I2Point(p.X + 1, p.Y),
								new I2Point(p.X, p.Y - 1),
								new I2Point(p.X, p.Y + 1),
							})
							{
								if (
									0 <= q.X && q.X < Consts.MAP_W &&
									0 <= q.Y && q.Y < Consts.MAP_H
									)
								{
									if (map[q.X, q.Y] == 0) f0 = true;
									if (map[q.X, q.Y] == 2) f2 = true;
								}
							}
							return f0 && f2;
						}
#else // old
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
#endif
						return false;
					})
					.ToArray();

					if (pts.Length == 0) // ? 他の閉路と壁１つで接していない。
					{
						AllXY().Where(p => map[p.X, p.Y] == 2).ToList().ForEach(p => map[p.X, p.Y] = 1); // 壁で塗りつぶす。
						continue;
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

			Putスタートとゴール(map);

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

		private void Putスタートとゴール(int[,] map)
		{
			I2Point startPt = SecurityTools.CRandom.ChooseOne(AllXY().Where(p => map[p.X, p.Y] == 0).ToArray());
			I2Point goalPt = SecurityTools.CRandom.ChooseOne(Farthest(map, startPt));

			for (; ; )
			{
				I2Point[] nextPts = Farthest(map, goalPt);

				map[startPt.X, startPt.Y] = 2;
				map[goalPt.X, goalPt.Y] = 2;
				foreach (I2Point nextPt in nextPts) map[nextPt.X, nextPt.Y] = 2;

				ExternalUtils.WriteMap(map);

				map[startPt.X, startPt.Y] = 0;
				map[goalPt.X, goalPt.Y] = 0;
				foreach (I2Point nextPt in nextPts) map[nextPt.X, nextPt.Y] = 0;

				if (nextPts.Any(p => p.X == startPt.X && p.Y == startPt.Y))
					break;

				startPt = SecurityTools.CRandom.ChooseOne(nextPts);
			}
			map[startPt.X, startPt.Y] = 2;
			map[goalPt.X, goalPt.Y] = 2;
		}

		private class FarthestSeeker
		{
			public int Distance;
			public I2Point Pt;
		}

		private I2Point[] Farthest(int[,] map, I2Point pt)
		{
			Queue<FarthestSeeker> seekers = new Queue<FarthestSeeker>();

			seekers.Enqueue(new FarthestSeeker()
			{
				Distance = 0,
				Pt = pt,
			});

			int bestDistance = -1;
			List<I2Point> bestPts = new List<I2Point>();

			while (1 <= seekers.Count)
			{
				FarthestSeeker seeker = seekers.Dequeue();

				if (
					seeker.Pt.X < 0 || Consts.MAP_W <= seeker.Pt.X ||
					seeker.Pt.Y < 0 || Consts.MAP_H <= seeker.Pt.Y
					)
					continue;

				if (map[seeker.Pt.X, seeker.Pt.Y] != 0)
					continue;

				int d = seeker.Distance;

				if (bestDistance < d)
				{
					bestDistance = d;
					bestPts.Clear();
				}
				if (bestDistance == d)
					bestPts.Add(seeker.Pt);

				map[seeker.Pt.X, seeker.Pt.Y] = 2; // 探索済みをセット

				seekers.Enqueue(new FarthestSeeker() { Distance = d + 1, Pt = new I2Point(seeker.Pt.X - 1, seeker.Pt.Y) });
				seekers.Enqueue(new FarthestSeeker() { Distance = d + 1, Pt = new I2Point(seeker.Pt.X + 1, seeker.Pt.Y) });
				seekers.Enqueue(new FarthestSeeker() { Distance = d + 1, Pt = new I2Point(seeker.Pt.X, seeker.Pt.Y - 1) });
				seekers.Enqueue(new FarthestSeeker() { Distance = d + 1, Pt = new I2Point(seeker.Pt.X, seeker.Pt.Y + 1) });
			}
			FillSame(map, pt.X, pt.Y, 0); // 探索範囲をクリア

			return bestPts.ToArray();
		}
	}
}
