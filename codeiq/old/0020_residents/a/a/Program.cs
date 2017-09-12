using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();

				new Program().Main2();

				sw.Stop();
				//Console.WriteLine("" + sw.Elapsed); // test
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			//Console.ReadLine(); // test
		}

		private class Resident
		{
			public double X;
			public double Y;

			public bool Covered;
		}

		private List<Resident> _residents = new List<Resident>();

		private class Speaker
		{
			public int MeterX;
			public int MeterY;

			public double X
			{
				get { return this.MeterX * 0.001; }
			}

			public double Y
			{
				get { return this.MeterY * 0.001; }
			}

			public List<Resident> CoverResidents = new List<Resident>();
		}

		private List<Speaker> _speakers = new List<Speaker>();

		private void Main2()
		{
			using (CsvFileReader cfr = new CsvFileReader(Console.OpenStandardInput()))
			{
				for (; ; )
				{
					string[] row = cfr.NextRow();

					if (row == null)
						break;

					if (row.Length == 2)
					{
						Resident r = new Resident();

						r.X = double.Parse(row[0]);
						r.Y = double.Parse(row[1]);

						_residents.Add(r);
					}
				}
			}

			while (this.ExistNotCovered())
			{
				//Console.WriteLine("uncovered-residents: " + this.GetNotCoveredCount()); // test
				this.TryCover();
			}

			foreach (Speaker s in _speakers)
			{
				Console.WriteLine(s.X.ToString("f3") + ", " + s.Y.ToString("f3"));
			}
		}

		private int GetNotCoveredCount()
		{
			int count = 0;

			foreach (Resident r in _residents)
			{
				if (r.Covered == false)
				{
					count++;
				}
			}
			return count;
		}

		private bool ExistNotCovered()
		{
			foreach (Resident r in _residents)
			{
				if (r.Covered == false)
				{
					return true;
				}
			}
			return false;
		}

		private const int METER_X_MIN = 0;
		private const int METER_Y_MIN = 0;
		private const int METER_X_MAX = 10000;
		private const int METER_Y_MAX = 10000;

		private void TryCover()
		{
			Speaker s = this.GetBestPos(
				METER_X_MIN,
				METER_Y_MIN,
				METER_X_MAX,
				METER_Y_MAX,
				100,
				100
				);

			s = this.GetBestPos(s.MeterX - 50, s.MeterY - 50, s.MeterX + 50, s.MeterY + 50, 10, 10);
			s = this.GetBestPos(s.MeterX - 5, s.MeterY - 5, s.MeterX + 5, s.MeterY + 5, 1, 1);

			this.Covered(s);
			_speakers.Add(s);
		}
		
		private Speaker GetBestPos(int mXBgn, int mYBgn, int mXEnd, int mYEnd, int mXStep, int mYStep)
		{
			Speaker best = null;

			mXBgn = Math.Max(METER_X_MIN, mXBgn);
			mYBgn = Math.Max(METER_Y_MIN, mYBgn);
			mXEnd = Math.Min(mXEnd, METER_X_MAX);
			mYEnd = Math.Min(mYEnd, METER_Y_MAX);

			for (int mx = mXBgn; mx <= mXEnd; mx += mXStep)
			{
				for (int my = mYBgn; my <= mYEnd; my += mYStep)
				{
					Speaker s = new Speaker();

					s.MeterX = mx;
					s.MeterY = my;

					this.Cover(s);

					if (best == null || best.CoverResidents.Count < s.CoverResidents.Count)
					{
						best = s;
						//Console.WriteLine("new_best: " + best.CoverResidents.Count); // test
					}
				}
			}
			return best;
		}

		private void Cover(Speaker s)
		{
			foreach (Resident r in _residents)
			{
				if (r.Covered == false && Tools.GetDistance(s.X, s.Y, r.X, r.Y) <= 1.0)
				{
					s.CoverResidents.Add(r);
				}
			}
		}

		private void Covered(Speaker s)
		{
			foreach (Resident r in s.CoverResidents)
			{
				r.Covered = true;
			}
		}
	}

	public class Tools
	{
		public static double GetDistance(double x1, double y1, double x2, double y2)
		{
			double x = x1 - x2;
			double y = y1 - y2;

			x *= x;
			y *= y;

			return Math.Sqrt(x + y);
		}
	}

	public class CsvFileReader : IDisposable
	{
		private Stream _sr;

		public CsvFileReader(Stream sr)
		{
			_sr = sr;
		}

		public int ReadChar()
		{
			for (; ; )
			{
				int chr = _sr.ReadByte();

				if (chr == '\r')
					continue;

				return chr;
			}
		}

		public bool _endOfRow;
		public bool _endOfFile;

		public string NextCell()
		{
			StringBuilder buff = new StringBuilder();
			int chr = ReadChar();

			if (chr == '"')
			{
				for (; ; )
				{
					chr = ReadChar();

					if (chr == -1)
						break;

					if (chr == '"')
					{
						chr = ReadChar();

						if (chr != '"')
							break;
					}
					buff.Append((char)chr);
				}
			}
			else
			{
				for (; ; )
				{
					if (chr == -1 || chr == '\n' || chr == ',')
						break;

					buff.Append((char)chr);
					chr = ReadChar();
				}
			}
			_endOfRow = chr == -1 || chr == '\n';
			_endOfFile = chr == -1;

			return buff.ToString();
		}

		public string[] NextRow()
		{
			List<string> row = new List<string>();

			do
			{
				row.Add(NextCell());
			}
			while (_endOfRow == false);

			if (_endOfFile && row.Count == 1 && row[0] == "")
			{
				return null;
			}
			return row.ToArray();
		}

		public void Dispose()
		{
			this.Close();
		}

		public void Close()
		{
			try
			{
				//_sr.Close();
			}
			catch
			{ }
		}
	}
}
