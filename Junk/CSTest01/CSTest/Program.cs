using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Program p = new Program();

				p.Main2();
				p.Main3();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("STROKE ENTER");
			Console.ReadLine();
		}

		private void Main2()
		{
			string[] a = new string[] { "A", "B", "C" };

			List<string> l = a.ToList();

			l[1] = "xxx";

			Console.WriteLine("a[0]: " + a[0]);
			Console.WriteLine("a[1]: " + a[1]); // B -> ToList()は複製ぽい。
			Console.WriteLine("a[2]: " + a[2]);

			{
				string str1;
				string str2;

				str1 = null;
				str2 = null;

				Console.WriteLine("null == null: " + (str1 == str2));

				str1 = "";
				str2 = null;

				Console.WriteLine("[] == null: " + (str1 == str2));

				str1 = null;
				str2 = "";

				Console.WriteLine("null == []: " + (str1 == str2));

				str1 = "";
				str2 = "";

				Console.WriteLine("[] == []: " + (str1 == str2));
			}
		}

		private void Main3()
		{
			Console.WriteLine("" + Test02_1());

			try
			{
				Test02_2();
			}
			catch (Exception e)
			{
				Console.WriteLine("" + e);
			}
		}

		private static string Test02_1()
		{
			try
			{
				return "AAA";
			}
			finally
			{
				//return "BBB"; // ng
			}
		}

		private static void Test02_2()
		{
			try
			{
				throw new Exception("aaa");
			}
			finally
			{
				throw new Exception("bbb");
			}
		}
	}
}
