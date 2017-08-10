﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public struct TimeData
	{
		public static TimeData FromString(string src)
		{
			List<string> tokens = StringTools.MeaningTokenize(src, StringTools.DIGIT);

			if (tokens.Count == 1)
			{
				src = tokens[0];

				if (src.Length == 8)
					return FromDateStamp(int.Parse(src));

				if (11 <= src.Length && src.Length <= 14)
					return FromTimeStamp(long.Parse(src));
			}
			else if (tokens.Count == 3)
			{
				return new TimeData(
					int.Parse(tokens[0]),
					int.Parse(tokens[1]),
					int.Parse(tokens[2])
					);
			}
			else if (tokens.Count == 6)
			{
				return new TimeData(
					int.Parse(tokens[0]),
					int.Parse(tokens[1]),
					int.Parse(tokens[2]),
					int.Parse(tokens[3]),
					int.Parse(tokens[4]),
					int.Parse(tokens[5])
					);
			}
			throw new ArgumentException();
		}

		public static TimeData FromTimeStamp(long timeStamp)
		{
			return FromDateStamp(
				(int)(timeStamp / 1000000L),
				(int)(timeStamp % 1000000L)
				);
		}

		public static TimeData FromDateStamp(int dateStamp, int dayTimeStamp = 0)
		{
			int d = dateStamp % 100;
			dateStamp /= 100;
			int m = dateStamp % 100;
			int y = dateStamp / 100;

			int s = dayTimeStamp % 100;
			dayTimeStamp /= 100;
			int i = dayTimeStamp % 100;
			int h = dayTimeStamp / 100;

			return new TimeData(y, m, d, h, i, s);
		}

		public static TimeData FromTimeStamp(int[] timeStamp)
		{
			return new TimeData(
				timeStamp[0],
				timeStamp[1],
				timeStamp[2],
				timeStamp[3],
				timeStamp[4],
				timeStamp[5]
				);
		}

		public static TimeData FromDateStamp(int[] dateStamp)
		{
			return new TimeData(
				dateStamp[0],
				dateStamp[1],
				dateStamp[2]
				);
		}

		private long _t;

		public TimeData(long t)
		{
			_t = t;
		}

		public TimeData(int y, int m, int d)
		{
			_t = GetTime(y, m, d);
		}

		public TimeData(int y, int m, int d, int h, int i, int s)
		{
			_t = GetTime(y, m, d, h, i, s);
		}

		public TimeData(DateTime dt)
		{
			_t = GetTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
		}

		public static long GetTime(int y, int m, int d)
		{
			return GetTime(y, m, d, 0, 0, 0);
		}

		public static long GetTime(int y, int m, int d, int h, int i, int s)
		{
			if (
				y < 1 ||
				m < 1 ||
				d < 1 ||
				h < 0 ||
				i < 0 ||
				s < 0
				)
				return 0L;

			m--;
			long ly = (long)y + m / 12;
			m %= 12;
			m++;

			if (m <= 2)
				ly--;

			long t = ly / 400;

			t *= 365 * 400 + 97;
			ly %= 400;
			t += ly * 365;
			t += ly / 4;
			t -= ly / 100;

			if (2 < m)
			{
				t -= 31 * 10 - 4;
				m -= 3;
				t += (m / 5) * (31 * 5 - 2);
				m %= 5;
				t += (m / 2) * (31 * 2 - 1);
				m %= 2;
				t += m * 31;
			}
			else
				t += (m - 1) * 31;

			t += d - 1;
			t *= 24;
			t += h;
			t *= 60;
			t += i;
			t *= 60;
			t += s;

			return t;
		}

		public static int[] GetTimeStamp(long t)
		{
			if (t < 0)
				return new int[] { 1, 1, 1, 0, 0, 0 };

			int s = (int)(t % 60);
			t /= 60;
			int i = (int)(t % 60);
			t /= 60;
			int h = (int)(t % 24);
			t /= 24;
			long ly = (t / 146097) * 400 + 1;

			t %= 146097;

			t += Math.Min((t + 306) / 36524, 3);
			ly += (t / 1461) * 4;
			t %= 1461;

			t += Math.Min((t + 306) / 365, 3);
			ly += t / 366;
			t %= 366;

			int m = 1;

			if (60 <= t)
			{
				m += 2;
				t -= 60;
				m += ((int)t / 153) * 5;
				t %= 153;
				m += ((int)t / 61) * 2;
				t %= 61;
			}
			m += (int)t / 31;
			t %= 31;

			if ((long)int.MaxValue < ly)
				return new int[] { int.MaxValue, 12, 31, 23, 59, 59 };

			int y = (int)ly;
			int d = (int)t + 1;

			return new int[] { y, m, d, h, i, s };
		}

		public int GetWeekdayIndex()
		{
			return (int)(_t / 86400L) % 7;
		}

		public string GetWeekday()
		{
			return GetWeekday(this.GetWeekdayIndex());
		}

		public static string GetWeekday(int index)
		{
			return "月火水木金土日".Substring(index, 1);
		}

		public override string ToString()
		{
			return this.GetString("Y/M/D (W) h:m:s");
		}

		public string GetString()
		{
			return this.GetString("YMDhms");
		}

		public string GetString(string format)
		{
			int[] timeStamp = GetTimeStamp(_t);

			if (9999 < timeStamp[0])
			{
				timeStamp[0] = 9999;
				timeStamp[1] = 12;
				timeStamp[2] = 31;
				timeStamp[3] = 23;
				timeStamp[4] = 59;
				timeStamp[5] = 59;
			}
			string weekday = FromTimeStamp(timeStamp).GetWeekday();
			string ret = format;

			ret = ret.Replace("Y", StringTools.ZPad(timeStamp[0], 4));
			ret = ret.Replace("M", StringTools.ZPad(timeStamp[1], 2));
			ret = ret.Replace("D", StringTools.ZPad(timeStamp[2], 2));
			ret = ret.Replace("h", StringTools.ZPad(timeStamp[3], 2));
			ret = ret.Replace("m", StringTools.ZPad(timeStamp[4], 2));
			ret = ret.Replace("s", StringTools.ZPad(timeStamp[5], 2));
			ret = ret.Replace("W", weekday);

			return ret;
		}

		public long GetTime()
		{
			return _t;
		}

		public int[] GetTimeStamp()
		{
			return GetTimeStamp(_t);
		}

		public DateTime GetDateTime()
		{
			int[] timeStamp = GetTimeStamp(_t);

			return new DateTime(
				timeStamp[0],
				timeStamp[1],
				timeStamp[2],
				timeStamp[3],
				timeStamp[4],
				timeStamp[5]
				);
		}

		public static TimeData Now
		{
			get
			{
				return new TimeData(DateTime.Now);
			}
		}

		public static readonly TimeData EPOCH_TIME_ZERO = new TimeData(1970, 1, 1);

		public long GetEpochTime()
		{
			return _t - EPOCH_TIME_ZERO._t;
		}

		public static TimeData FromEpochTime(long t)
		{
			return new TimeData(EPOCH_TIME_ZERO._t + t);
		}
	}
}
