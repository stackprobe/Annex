using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	public class DateData
	{
		private Calc _calc;
		private CalcOperand _day;

		public DateData(Calc calc, CalcOperand day)
		{
			_calc = calc;
			_day = day;
		}

		public DateData(Calc calc, CalcOperand y, CalcOperand m, CalcOperand d)
		{
			_calc = calc;
			_day = Date2Day(calc, y, m, d);
		}

		public CalcOperand GetDay()
		{
			return _day;
		}

		public CalcOperand[] GetDate()
		{
			return Day2Date(_calc, _day);
		}

		public string GetString(string format)
		{
			string ret = format;

			CalcOperand[] date = Day2Date(_calc, _day);

			ret = ret.Replace("[Y]", _calc.GetString(date[0]));
			ret = ret.Replace("[M]", _calc.GetString(date[1]));
			ret = ret.Replace("[D]", _calc.GetString(date[2]));

			return ret;
		}

		public static CalcOperand Date2Day(Calc calc, CalcOperand y, CalcOperand m, CalcOperand d)
		{
			if (
				calc.LT(y, calc.FromInt(1)) ||
				calc.LT(m, calc.FromInt(1)) ||
				calc.LT(d, calc.FromInt(1))
				)
				throw new Exception("日付に問題があります。");

			// 13月以上 -> 12月以下
			{
				m = calc.Sub(m, calc.FromInt(1));
				y = calc.Add(y, calc.Div(m, calc.FromInt(12), 0));
				m = calc.Mod(m, calc.FromInt(12));
				m = calc.Add(m, calc.FromInt(1));
			}

			if (calc.LE(m, calc.FromInt(2)))
				y = calc.Sub(y, calc.FromInt(1));

			CalcOperand day = calc.Div(y, calc.FromInt(400), 0);
			day = calc.Mul(day, calc.FromInt(365 * 400 + 97));

			y = calc.Mod(y, calc.FromInt(400));

			day = calc.Add(day, calc.Mul(y, calc.FromInt(365)));
			day = calc.Add(day, calc.Div(y, calc.FromInt(4), 0));
			day = calc.Sub(day, calc.Div(y, calc.FromInt(100), 0));

			if (calc.LT(calc.FromInt(2), m))
			{
				day = calc.Sub(day, calc.FromInt(31 * 10 - 4));
				m = calc.Sub(m, calc.FromInt(3));
				day = calc.Add(day, calc.Mul(calc.Div(m, calc.FromInt(5), 0), calc.FromInt(31 * 5 - 2)));
				m = calc.Mod(m, calc.FromInt(5));
				day = calc.Add(day, calc.Mul(calc.Div(m, calc.FromInt(2), 0), calc.FromInt(31 * 2 - 1)));
				m = calc.Mod(m, calc.FromInt(2));
				day = calc.Add(day, calc.Mul(m, calc.FromInt(31)));
			}
			else
				day = calc.Add(day, calc.Mul(calc.Sub(m, calc.FromInt(1)), calc.FromInt(31)));

			day = calc.Add(day, calc.Sub(d, calc.FromInt(1)));
			return day;
		}

		public static CalcOperand[] Day2Date(Calc calc, CalcOperand day)
		{
			CalcOperand yBit = calc.FromInt(256);
			CalcOperand y = calc.Add(
				calc.Mul(
					calc.Div(day, calc.FromInt(365 * 400 + 97), 0),
					calc.FromInt(400)
					),
				calc.FromInt(1)
				);

			for (; yBit.IsZero() == false; yBit = calc.Div(yBit, calc.FromInt(2), 0))
			{
				CalcOperand yTry = calc.Add(y, yBit);

				if (calc.LE(Date2Day(calc, yTry, calc.FromInt(1), calc.FromInt(1)), day))
					y = yTry;
			}
			CalcOperand m = calc.FromInt(0);

			for (CalcOperand mBit = calc.FromInt(8); mBit.IsZero() == false; mBit = calc.Div(mBit, calc.FromInt(2), 0))
			{
				CalcOperand mTry = calc.Add(m, mBit);

				if (calc.LE(mTry, calc.FromInt(12)) && calc.LE(Date2Day(calc, y, mTry, calc.FromInt(1)), day))
					m = mTry;
			}
			day = calc.Sub(day, Date2Day(calc, y, m, calc.FromInt(1)));
			day = calc.Add(day, calc.FromInt(1));

			return new CalcOperand[] { y, m, day };
		}
	}
}
