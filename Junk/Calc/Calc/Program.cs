using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
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
			if (args.Length == 1 || args.Length == 2)
			{
				Console.WriteLine(
					new Calc().GetString(
						new Calc().FromString(args[0])
						)
					);
			}
			if (args.Length == 1)
			{
				Console.WriteLine(
					new Calc().GetString(
						new Calc().FromString(args[0]),
						true
						)
					);
			}
			else if (args.Length == 2)
			{
				Console.WriteLine(
					new Calc().GetString(
						new Calc().FromString(args[0]),
						true,
						int.Parse(args[1]),
						true
						)
					);
			}
			else if (args.Length == 3)
			{
				Console.WriteLine(new Calc().DoCalc(args[0], args[1][0], args[2]));
			}
			else if (args.Length == 4)
			{
				Console.WriteLine(new Calc().DoCalc(args[0], args[1][0], args[2], int.Parse(args[3])));
			}
			else
			{
				try
				{
					this.TestMain();
					//this.TestDateData();
					this.TestDateData2();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				Console.ReadLine();
			}
		}

		private void TestMain()
		{
			Console.WriteLine(new Calc().DoCalc("1", '+', "1"));
			Console.WriteLine(new Calc().DoCalc("1", '-', "1"));
			Console.WriteLine(new Calc().DoCalc("1", '*', "1"));
			Console.WriteLine(new Calc().DoCalc("1", '/', "1"));
		}

		private void TestDateData()
		{
			Calc calc = new Calc();

			int y = 1;
			int m = 1;
			int d = 1;
			long day = 0;

			while (y < 3000)
			{
				// check
				{
					DateData dd = new DateData(calc, calc.FromInt(y), calc.FromInt(m), calc.FromInt(d));

					if (day != long.Parse(calc.GetString(dd.GetDay())))
						throw new Exception();

					CalcOperand[] date = dd.GetDate();

					if (calc.IsSame(calc.FromInt(y), date[0]) == false) throw new Exception();
					if (calc.IsSame(calc.FromInt(m), date[1]) == false) throw new Exception();
					if (calc.IsSame(calc.FromInt(d), date[2]) == false) throw new Exception();
				}

				// next day
				{
					d++;

					if (this.GetEndOfMonth(y, m) < d)
					{
						m++;
						d = 1;

						if (12 < m)
						{
							y++;
							m = 1;

							Console.WriteLine("[TestDateDate]_y: " + y);
						}
					}
					day++;
				}
			}
		}

		private int GetEndOfMonth(int year, int month)
		{
			if (
				year < 1 ||
				month < 1 || 12 < month
				)
				throw new ArgumentException();

			switch (month)
			{
				case 1: return 31;
				case 2: return this.IsUruuYear(year) ? 29 : 28;
				case 3: return 31;
				case 4: return 30;
				case 5: return 31;
				case 6: return 30;
				case 7: return 31;
				case 8: return 31;
				case 9: return 30;
				case 10: return 31;
				case 11: return 30;
				case 12: return 31;
			}
			return -1; // dummy
		}

		private bool IsUruuYear(int year)
		{
			return (year % 4) == 0 && (year % 100) != 0 || (year % 400) == 0; // from wiki
		}

		private void TestDateData2()
		{
			this.TestDateData("1", "1", "1");
			this.TestDateData("400", "12", "31");
			this.TestDateData("401", "1", "1");
			this.TestDateData("800", "12", "31");
			this.TestDateData("801", "1", "1");

			this.TestDateData("1313", "13", "13", "1314", "1", "13");
			this.TestDateData("1313", "26", "13", "1315", "2", "13");

			this.TestDateData("1234567890", "6", "6");
			this.TestDateData("123456789012345678901234567890", "12", "31");
			this.TestDateData("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", "9", "9");
			this.TestDateData(
				"123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
				"123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
				"123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
				"1",
				"1"
				);
		}

		private void TestDateData(string y, string m, string d)
		{
			this.TestDateData(y, m, d, y, m, d);
		}

		private void TestDateData(string ty, string tm, string td, string ey, string em, string ed)
		{
			Calc calc = new Calc();

			Console.WriteLine("tYmd: " + ty + "/" + tm + "/" + td);
			Console.WriteLine("eYmd: " + ey + "/" + em + "/" + ed);

			CalcOperand day = new DateData(calc, calc.FromString(ty), calc.FromString(tm), calc.FromString(td)).GetDay();

			Console.WriteLine("day: " + calc.GetString(day));

			CalcOperand[] date = new DateData(calc, day).GetDate();

			Console.WriteLine("rYmd: " + calc.GetString(date[0]) + "/" + calc.GetString(date[1]) + "/" + calc.GetString(date[2]));

			if (
				calc.IsSame(calc.FromString(ey), date[0]) == false ||
				calc.IsSame(calc.FromString(em), date[1]) == false ||
				calc.IsSame(calc.FromString(ed), date[2]) == false
				)
				throw new Exception("test_ng");
		}
	}
}
