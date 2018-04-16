using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace lockRecursiveTest
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}

		private void Main2()
		{
			this.Test01(3);
		}

		private object SYNCROOT = new object();

		private void Test01(int c)
		{
			Console.WriteLine("Test01.1");
			lock (this.SYNCROOT)
			{
				Console.WriteLine("Test01.2");
				this.Test01_b(c);
				Console.WriteLine("Test01.3");
			}
			Console.WriteLine("Test01.4");
		}

		private void Test01_b(int c)
		{
			if (--c <= 0)
				return;

#if false // ok
			this.Test01(c);
#else
			// 次の Test01.1 で止まる。
			{
				Thread th = new Thread(() => this.Test01(c));
				th.Start();
				th.Join();
			}
#endif
		}
	}
}
