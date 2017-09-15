using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string[] args)
		{
			string rFile = args[0];

			Calc calc = new Calc();
			CalcOperand lSum = calc.FromSimpleString("0");
			CalcOperand rSum = calc.FromSimpleString("0");

			using (CsvFileReader cfr = new CsvFileReader(rFile, Encoding.ASCII))
			{
				for (; ; )
				{
					string[] row = cfr.NextRow();

					if (row == null)
						break;

					if (row.Length == 2)
					{
						lSum = calc.Add(lSum, calc.FromString(row[0]));
						rSum = calc.Add(rSum, calc.FromString(row[1]));
					}
				}
			}

			Console.WriteLine(
				calc.GetString(lSum) + " " +
				calc.Compare_Sym(lSum, rSum) + " " +
				calc.GetString(rSum)
				);
		}
	}

	public class CsvFileReader : IDisposable
	{
		private StreamReader _sr;

		public CsvFileReader(string file, Encoding encoding)
		{
			_sr = new StreamReader(file, encoding);
		}

		public int ReadChar()
		{
			for (; ; )
			{
				int chr = _sr.Read();

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
				_sr.Close();
			}
			catch
			{ }
		}
	}

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

			co.F = ArrayTools.GetClone(F);
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

			this.NoEe = this.DIGITS.IndexOf('E') == -1 && this.DIGITS.IndexOf('e') == -1;
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
				ePos = str.IndexOf('E');

				if (ePos == -1)
					ePos = str.IndexOf('e');
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

		public string GetString(CalcOperand co, int effectMin = 0)
		{
			StringBuilder buff = new StringBuilder();

			co.Trim();

			while (co.F.Count < co.E + 1)
			{
				co.F.Add(0);
			}
			while (co.F.Count < effectMin)
			{
				co.Expand();
			}
			for (int index = co.E; index < 0; index++)
			{
				buff.Append(this.DIGITS[0]);
			}
			for (int index = 0; index < co.F.Count; index++)
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

		public char Compare_Sym(CalcOperand co1, CalcOperand co2)
		{
			CalcOperand ans = this.Sub(co1, co2);

			if (ans.Sign == -1)
				return '<';

			if (ans.IsZero())
				return '=';

			return '>';
		}
	}

	public class ArrayTools
	{
		public static List<T> GetClone<T>(List<T> src)
		{
			List<T> nl = new List<T>();

			for (int index = 0; index < src.Count; index++)
			{
				nl.Add(src[index]);
			}
			return nl;
		}
	}

	public class StringTools
	{
		public static string Reverse(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = str.Length - 1; 0 <= index; index--)
			{
				buff.Append(str[index]);
			}
			return buff.ToString();
		}

		public static string RPad(string str, int minlen, char chrPad)
		{
			while (str.Length < minlen)
			{
				str = chrPad + str;
			}
			return str;
		}

		public static bool HasSameChar(string str)
		{
			for (int j = 1; j < str.Length; j++)
				for (int i = 0; i < j; i++)
					if (str[i] == str[j])
						return true;

			return false;
		}
	}
}
