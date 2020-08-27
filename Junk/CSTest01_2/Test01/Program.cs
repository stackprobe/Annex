using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main6();
				//new Program().Main5();
				//new DirectoryTest().Test01();
				//new Program().Main4();
				//new Program().Main3();
				//new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("\\e");
			Console.ReadLine();
		}

		private void Main3()
		{
			{
				CsvData cd = new CsvData();

				cd.ReadFile(@"C:\tmp\test.csv");
				cd.TTR();
				cd.WriteFile(@"C:\temp\test.csv");
			}

			{
				string src = "AAA\tBBB\rCCC\na$a$a$a\n$$$\n\n\n";
				string enc = EscapeString.I.Encode(src);
				string dec = EscapeString.I.Decode(enc);

				if (src != dec)
					throw new Exception();
			}

			{
				List<string> src = new List<string>();

				src.Add("aaa");
				src.Add("bbb");
				src.Add("a$a$a");
				src.Add("$$$");
				src.Add("///");
				src.Add("___");
				src.Add("");
				src.Add("1/2/3");
				src.Add("");

				string enc = AttachString.I.Untokenize(src);
				List<string> dec = AttachString.I.Tokenize(enc);

				if (src.Count != dec.Count)
					throw new Exception();

				for (int index = 0; index < src.Count; index++)
					if (src[index] != dec[index])
						throw new Exception();
			}

			{
				XNode root = XNode.Load(@"C:\tmp\test.xml");

				root.PrintDebug();
			}

			{
				string url = "http://www.google.co.jp/";

				HttpClient hc = new HttpClient(url);
				hc.Perform();
				Console.WriteLine(Encoding.UTF8.GetString(hc.GetResBody()));
			}
		}

		private void Main2()
		{
			Console.WriteLine("[" + "\t\n\r aaa]".Trim());
			//Console.WriteLine("[" + "\a\t\n\r aaa]".Trim());

			//Console.WriteLine("\a\a\a\a\a");
			//Console.WriteLine("\a");

			Console.WriteLine("[" + "\t\n\r aaa\t_\n_\r_ aaa]\t\n\r ".Trim());

			Print(":::".Split(':'));
			Print("a:::".Split(':'));
			Print(":a::".Split(':'));
			Print("::a:".Split(':'));
			Print(":::a".Split(':'));
			Print(":\t:\n:".Split(':'));
			Print("  :  :  :  ".Split(':'));

			{
				List<string> lines = new List<string>();

				lines.Add("aaa");
				lines.Remove("aaa");

				Console.WriteLine("lines.Count: " + lines.Count); // 0

				lines.Remove("abcdef");

				Console.WriteLine("lines.Count: " + lines.Count); // 0

				lines.Add("aaa");
				lines.Remove("abcdef");

				Console.WriteLine("lines.Count: " + lines.Count); // 1
			}
		}

		private void Print(string[] strs)
		{
			Console.WriteLine("bgn");

			foreach (string str in strs)
			{
				Console.WriteLine("[" + str + "]");
			}
			Console.WriteLine("end");
		}

		private void Main4()
		{
			HttpServer hs = new HttpServer();

			Console.WriteLine("\\w");
			Console.ReadLine();

			hs.End();
		}

		private void Main5()
		{
			string[] a = new string[] { "1", "22", "333" };
			List<string> l = a.ToList();
			l.RemoveAt(1);
			l.Add("4444");
			string[] b = l.ToArray();

			Console.WriteLine(string.Join(", ", b));
		}

		private void Main6()
		{
#if true
			for (int i = int.MinValue; i <= int.MinValue + 1000; i++)
				Main6_a(i);

			for (int i = -1000; i <= 1000; i++)
				Main6_a(i);

			for (int i = int.MaxValue - 1000; i < int.MaxValue; i++)
				Main6_a(i);

			Main6_a(int.MaxValue);
#else
			for (int i = int.MinValue; i < int.MaxValue; i++)
				Main6_a(i);

			Main6_a(int.MaxValue);
#endif

			// ----

			for (long l = long.MinValue; l <= long.MinValue + 1000L; l++)
				Main6_b(l);

			for (long l = (long)int.MinValue - 1000L; l <= (long)int.MinValue + 1000L; l++)
				Main6_b(l);

			for (long l = -1000L; l <= 1000L; l++)
				Main6_b(l);

			for (long l = (long)int.MaxValue - 1000L; l <= (long)int.MaxValue + 1000L; l++)
				Main6_b(l);

			for (long l = long.MaxValue - 1000L; l < long.MaxValue; l++)
				Main6_b(l);

			Main6_b(long.MaxValue);
		}

		private void Main6_a(int i)
		{
			uint ui = (uint)i;
			int si = (int)ui;

			Console.WriteLine(string.Join(", ", i, ui, si)); // test

			if (i != si)
				throw null; // souteigai !!!
		}

		private void Main6_b(long l)
		{
			ulong ul = (ulong)l;
			long sl = (long)ul;

			Console.WriteLine(string.Join(", ", l, ul, sl)); // test

			if (l != sl)
				throw null; // souteigai !!!
		}
	}
}
