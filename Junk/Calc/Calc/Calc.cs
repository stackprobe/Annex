using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	public class CalcOperand
	{
		private Calc Calc;

		public CalcOperand(Calc calc)
		{
			this.Calc = calc;
		}

		public List<int> F = new List<int>();
		public int E = 0;
		public int Sign = 1;

		public void Trim()
		{
			while (1 <= F.Count && F[F.Count - 1] == 0)
			{
				F.RemoveAt(F.Count - 1);
			}
			while (1 <= F.Count && F[0] == 0)
			{
				F.RemoveAt(0);
				E--;
			}
			if (F.Count == 0)
			{
				E = 0;
				Sign = 1;
			}
		}

		public int Get(int index)
		{
			if (F.Count <= index)
			{
				return 0;
			}
			return F[index];
		}

		public void Set(int index, int value)
		{
			while (F.Count <= index)
			{
				F.Add(0);
			}
			F[index] = value;
		}

		public void Expand()
		{
			F.Insert(0, 0);
			E++;
		}

		public void Add(int index, int value)
		{
			while (1 <= value)
			{
				value += Get(index);
				Set(index, value % Calc.RADIX);
				value /= Calc.RADIX;
				index++;
			}
		}

		public CalcOperand GetClone()
		{
			CalcOperand co = new CalcOperand(Calc);

			co.F = ArrayTools.ShallowCopy(F);
			co.E = E;
			co.Sign = Sign;

			return co;
		}

		public bool IsZero()
		{
			Trim();
			return F.Count == 0;
		}
	}

	public class Calc
	{
		public const string DEF_DIGITS = "0123456789";
		public const int DEF_BASEMENT = 30;
		public const int DEF_MOD_BASEMENT = 0;

		public string DIGITS;
		public int RADIX
		{
			get
			{
				return this.DIGITS.Length;
			}
		}
		private bool NoEe;

		public Calc(string digits)
		{
			if (digits == null)
				digits = DEF_DIGITS;

			this.DIGITS = digits;

			if (this.RADIX < 2)
				throw new ArgumentException();

			if (StringTools.HasSameChar(this.DIGITS + "-."))
				throw new ArgumentException();

			this.NoEe = this.DIGITS.IndexOf('E') != -1 && this.DIGITS.IndexOf('e') != -1;
		}

		public Calc()
			: this(null)
		{ }

		public CalcOperand FromSimpleString(string str)
		{
			CalcOperand co = new CalcOperand(this);
			bool fndPrd = false;

			foreach(char chr in str)
			{
				if (chr == '-')
				{
					co.Sign = -1;
					continue;
				}
				if (chr == '.')
				{
					fndPrd = true;
					continue;
				}
				int value = this.DIGITS.IndexOf(chr);

				if (value == -1)
					value = this.DIGITS.IndexOf(char.ToLower(chr));

				if (value == -1)
					value = this.DIGITS.IndexOf(char.ToUpper(chr));

				if (value != -1)
				{
					co.F.Add(value);

					if (fndPrd)
						co.E++;
				}
			}
			co.F.Reverse();
			co.Trim();
			return co;
		}

		public CalcOperand FromString(string str)
		{
			int ePos;

			if (this.NoEe)
			{
				ePos = str.IndexOf("E");

				if (ePos == -1)
					ePos = str.IndexOf("e");
			}
			else
			{
				ePos = str.IndexOf("E+");

				if (ePos == -1)
					ePos = str.IndexOf("e+");

				if (ePos == -1)
					ePos = str.IndexOf("E-");

				if (ePos == -1)
					ePos = str.IndexOf("e-");
			}

			if (ePos != -1)
			{
				CalcOperand co = this.FromSimpleString(str.Substring(0, ePos));
				co.E -= int.Parse(str.Substring(ePos + 1));
				co.Trim();
				return co;
			}
			return this.FromSimpleString(str);
		}

		public CalcOperand FromInt(int value)
		{
			CalcOperand co = new CalcOperand(this);

			if (value < 0)
			{
				co.Add(0, -value);
				co.Sign = -1;
			}
			else
				co.Add(0, value);

			return co;
		}

		public string GetString(CalcOperand co, int effectMin = 0)
		{
			StringBuilder buff = new StringBuilder();

			co.Trim();

			if (1 <= effectMin)
			{
				while (co.F.Count < co.E + 1)
				{
					co.F.Add(0);
				}
				while (co.F.Count < effectMin)
				{
					co.Expand();
				}
			}
			for (int index = co.E; index < 0; index++)
			{
				buff.Append(this.DIGITS[0]);
			}
			for (int index = 0; index < Math.Max(co.E + 1, co.F.Count); index++)
			{
				if (1 <= index && index == co.E)
				{
					buff.Append('.');
				}
				buff.Append(this.DIGITS[co.Get(index)]);
			}
			if (co.Sign == -1)
			{
				buff.Append('-');
			}
			return StringTools.Reverse(buff.ToString());
		}

		public string GetString(CalcOperand co, bool scient, int effect = 0, bool rndOff = false)
		{
			co = co.GetClone();
			co.Trim();

			if (1 <= effect)
			{
				int endPos = co.F.Count - effect;

				for (int index = 0; index < endPos; index++)
				{
					if (rndOff && index == endPos - 1 && this.RADIX / 2 <= co.F[index])
					{
						co.Add(endPos, 1); // 四捨五入
					}
					co.F[index] = 0;
				}
				co.Trim();
			}
			if (scient)
			{
				int me = Math.Max(0, co.F.Count - 1);
				int ee = me - co.E;

				co.E = me;

				return this.GetString(co, effect) + 'E' + (ee < 0 ? '-' : '+') + StringTools.RPad("" + Math.Abs(ee), 2, this.DIGITS[0]);
			}
			return this.GetString(co);
		}

		private void SyncE(CalcOperand co1, CalcOperand co2)
		{
			while (co1.E < co2.E)
			{
				co1.Expand();
			}
			while (co2.E < co1.E)
			{
				co2.Expand();
			}
		}

		public CalcOperand Add(CalcOperand co1, CalcOperand co2)
		{
			if (co1 == co2)
				co2 = co2.GetClone();

			CalcOperand ans;

			if (co1.Sign == -1)
			{
				co1.Sign = 1;
				ans = this.Sub(co2, co1);
				co1.Sign = -1;
				return ans;
			}
			if (co2.Sign == -1)
			{
				co2.Sign = 1;
				ans = this.Sub(co1, co2);
				co2.Sign = -1;
				return ans;
			}
			co1.Trim();
			co2.Trim();

			this.SyncE(co1, co2);

			ans = new CalcOperand(this);

			for (int index = 0; index < Math.Max(co1.F.Count, co2.F.Count); index++)
			{
				ans.Add(index, co1.Get(index) + co2.Get(index));
			}
			ans.E = co1.E;
			ans.Trim();
			return ans;
		}

		public CalcOperand Sub(CalcOperand co1, CalcOperand co2)
		{
			if (co1 == co2)
				return new CalcOperand(this); // x - x == 0

			CalcOperand ans;

			if (co1.Sign == -1)
			{
				co1.Sign = 1;
				ans = this.Add(co1, co2);
				ans.Sign *= -1;
				co1.Sign = -1;
				return ans;
			}
			if (co2.Sign == -1)
			{
				co2.Sign = 1;
				ans = this.Add(co1, co2);
				co2.Sign = -1;
				return ans;
			}
			co1.Trim();
			co2.Trim();

			this.SyncE(co1, co2);

			int endPos = Math.Max(co1.F.Count, co2.F.Count);
			endPos = Math.Max(1, endPos);

			ans = new CalcOperand(this);

			for (int index = 0; index < endPos; index++)
			{
				ans.Add(index, co1.Get(index) + this.RADIX - co2.Get(index) - (index == 0 ? 0 : 1));
			}
			if (ans.Get(endPos) == 0)
			{
				ans = this.Sub(co2, co1);
				ans.Sign = -1;
				return ans;
			}
			ans.F[endPos] = 0;
			ans.E = co1.E;
			ans.Trim();
			return ans;
		}

		public CalcOperand Mul(CalcOperand co1, CalcOperand co2)
		{
			CalcOperand ans = new CalcOperand(this);

			co1.Trim();
			co2.Trim();

			for (int index1 = 0; index1 < co1.F.Count; index1++)
				for (int index2 = 0; index2 < co2.F.Count; index2++)
					ans.Add(index1 + index2, co1.F[index1] * co2.F[index2]);

			ans.E = co1.E + co2.E;
			ans.Sign = co1.Sign * co2.Sign;
			ans.Trim();
			return ans;
		}

		public CalcOperand Div(CalcOperand co1, CalcOperand co2, int basement = DEF_BASEMENT)
		{
			co1.Trim();
			co2.Trim();

			if (co2.F.Count == 0)
				throw new DivideByZeroException();

			co1 = co1.GetClone();
			co2 = co2.GetClone();

			CalcOperand ans = new CalcOperand(this);

			ans.Sign = co1.Sign * co2.Sign;
			co1.Sign = 1;
			co2.Sign = 1;
			co2.E += basement;
			co2.Trim();

			this.SyncE(co1, co2);

			int shiftCnt = Math.Max(0, co1.F.Count - co2.F.Count);

			co2.E -= shiftCnt;

			while(0 <= shiftCnt)
			{
				CalcOperand tmp = this.Sub(co1, co2);

				if (tmp.Sign == 1)
				{
					ans.Add(shiftCnt, 1);
					co1 = tmp;
				}
				else
				{
					co2.E++;
					shiftCnt--;
				}
			}
			ans.E = basement;
			ans.Trim();
			return ans;
		}

		public CalcOperand Mod(CalcOperand co1, CalcOperand co2, int basement = DEF_MOD_BASEMENT)
		{
			return this.Sub(
				co1,
				this.Mul(
					this.Div(co1, co2, basement),
					co2
					)
				);
		}

		public string DoCalc(string str1, char op, string str2, int basement = DEF_BASEMENT)
		{
			CalcOperand co1 = this.FromString(str1);
			CalcOperand co2 = this.FromString(str2);
			CalcOperand ans;

			if (op == '+')
			{
				ans = this.Add(co1, co2);
			}
			else if (op == '-')
			{
				ans = this.Sub(co1, co2);
			}
			else if (op == '*')
			{
				ans = this.Mul(co1, co2);
			}
			else if (op == '/')
			{
				ans = this.Div(co1, co2, basement);
			}
			else if (op == '%')
			{
				ans = this.Mod(co1, co2, basement);
			}
			else
			{
				throw new ArgumentException();
			}
			return this.GetString(ans);
		}

		public int Compare(CalcOperand co1, CalcOperand co2)
		{
			CalcOperand ans = this.Sub(co1, co2);

			if (ans.Sign == -1)
				return -1;

			if (ans.IsZero())
				return 0;

			return 1;
		}

		public bool LT(CalcOperand co1, CalcOperand co2) // ? co1 < co2
		{
			return this.Compare(co1, co2) == -1;
		}

		public bool LE(CalcOperand co1, CalcOperand co2) // ? co1 <= co2
		{
			return this.Compare(co1, co2) != 1;
		}

		public bool GT(CalcOperand co1, CalcOperand co2) // ? co1 > co2
		{
			return this.Compare(co1, co2) == 1;
		}

		public bool GE(CalcOperand co1, CalcOperand co2) // ? co1 >= co2
		{
			return this.Compare(co1, co2) != -1;
		}

		public bool IsSame(CalcOperand co1, CalcOperand co2) // ? co1 == co2
		{
			return this.Compare(co1, co2) == 0;
		}

		public CalcOperand Power(CalcOperand co, int exponent)
		{
			if (exponent < 0)
				throw new Exception("bad_exponent");

			if (exponent == 0)
			{
				co = new CalcOperand(this);
				co.Add(0, 1);
				return co;
			}
			if (exponent == 1)
				return co.GetClone();

			CalcOperand ans = this.Power(this.Mul(co, co), exponent / 2);

			if (exponent % 2 == 1)
				ans = this.Mul(ans, co);

			return ans;
		}
	}
}
