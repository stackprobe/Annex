using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests.Tools
{
	public class MSectionTest
	{
		public void Test01()
		{
			using (new MSection("{b8bd3422-736b-4bd7-9db9-bffca7212629}"))
			{ }
		}

		public void Test02()
		{
			Thread[] ths = new Thread[10];

			for (int i = 0; i < ths.Length; i++)
				ths[i] = this.Test02_MakeTh(i);

			foreach (Thread th in ths)
				th.Start();

			foreach (Thread th in ths)
				th.Join();
		}

		private Thread Test02_MakeTh(int i)
		{
			return new Thread(() => this.Test02_Th(i));
		}

		private void Test02_Th(int i)
		{
			using (new MSection("{0a35977c-5ed6-42ef-a09c-b25454a7c6b0}"))
			{
				Console.WriteLine("A " + i);
				Thread.Sleep(100);
				Console.WriteLine("B " + i);
			}
		}
	}
}
