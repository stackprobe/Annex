using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{9d309a8b-691c-474f-860d-0ab3ffe3eaa3}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
#endif
		}

		private void Main2(ArgsReader ar)
		{
			Console.WriteLine("a1");

			{
				Mutex m = new Mutex(false, "TEST");

				m.WaitOne();
				m.ReleaseMutex();

				m.Dispose();
			}

			//
			// 入れ子テスト
			//

			{
				Mutex m = new Mutex(false, "TEST");

				m.WaitOne();
				m.WaitOne(); // 入れ子出来る！

				m.ReleaseMutex(); // 入れ子の分
				m.ReleaseMutex();
				//m.ReleaseMutex(); // -> 例外

				m.Dispose();
			}

			{
				Mutex m = new Mutex(false, "TEST");

				m.WaitOne();
				m.WaitOne(); // 入れ子 1
				m.WaitOne(); // 入れ子 2

				m.ReleaseMutex(); // 入れ子 2
				m.ReleaseMutex(); // 入れ子 1
				m.ReleaseMutex();
				//m.ReleaseMutex(); // -> 例外

				m.Dispose();
			}

			{
				Mutex m = new Mutex(false, "TEST");

				m.WaitOne();

				m.WaitOne();
				m.ReleaseMutex();

				m.WaitOne();
				m.ReleaseMutex();

				m.WaitOne();
				m.ReleaseMutex();

				m.WaitOne();
				m.WaitOne();
				m.WaitOne();
				m.ReleaseMutex();
				m.ReleaseMutex();
				m.ReleaseMutex();

				m.WaitOne();
				m.WaitOne();
				m.WaitOne();
				m.ReleaseMutex();
				m.ReleaseMutex();
				m.ReleaseMutex();

				m.WaitOne();
				m.WaitOne();
				m.WaitOne();
				m.ReleaseMutex();
				m.ReleaseMutex();
				m.ReleaseMutex();

				m.ReleaseMutex();
				//m.ReleaseMutex(); // -> 例外

				m.Dispose();
			}

			Console.WriteLine("a2");

			//
			// 同じ Mutex オブジェクトでも別スレッドではちゃんと待つ模様
			//

			{
				Mutex m = new Mutex(false, "TEST");

				new Thread(() =>
				{
					Thread.Sleep(100); // catnap

					m.WaitOne(); // メインスレッド側の ReleaseMutex() まで、待たされる！
					Console.WriteLine("a2.1");
					m.ReleaseMutex();
				})
				.Start();

				m.WaitOne();
				Thread.Sleep(2000); // 2 sec
				Console.WriteLine("a2.0.1");
				m.ReleaseMutex();
				Console.WriteLine("a2.0.2");

				Thread.Sleep(100); // catnap

				m.Dispose();
			}

			Console.WriteLine("a3");

			{
				Mutex m = new Mutex(false, "TEST");

				new Thread(() =>
				{
					m.WaitOne();
					Thread.Sleep(2000); // 2 sec
					Console.WriteLine("a3.0.1");
					m.ReleaseMutex();
					Console.WriteLine("a3.0.1");
				})
				.Start();

				Thread.Sleep(100); // catnap

				m.WaitOne(); // ワーカースレッド側の ReleaseMutex() まで、待たされる！
				Console.WriteLine("a3.1");
				m.ReleaseMutex();

				m.Dispose();
			}

			Console.WriteLine("a4");

			//
			// 別オブジェクト
			//

			{
				Mutex m = new Mutex(false, "TEST");

				new Thread(() =>
				{
					Mutex m2 = new Mutex(false, "TEST");

					Thread.Sleep(100); // catnap

					m2.WaitOne(); // メインスレッド側の ReleaseMutex() まで、待たされる！
					Console.WriteLine("a4.1");
					m2.ReleaseMutex();

					m2.Dispose();
				})
				.Start();

				m.WaitOne();
				Thread.Sleep(2000); // 2 sec
				Console.WriteLine("a4.0.1");
				m.ReleaseMutex();
				Console.WriteLine("a4.0.2");

				Thread.Sleep(100); // catnap

				m.Dispose();
			}

			Console.WriteLine("a5");

			{
				Mutex m = new Mutex(false, "TEST");

				new Thread(() =>
				{
					Mutex m2 = new Mutex(false, "TEST");

					m2.WaitOne();
					Thread.Sleep(2000); // 2 sec
					Console.WriteLine("a5.0.1");
					m2.ReleaseMutex();
					Console.WriteLine("a5.0.2");

					m2.Dispose();
				})
				.Start();

				Thread.Sleep(100); // catnap

				m.WaitOne(); // ワーカースレッド側の ReleaseMutex() まで、待たされる！
				Console.WriteLine("a5.1");
				m.ReleaseMutex();

				Thread.Sleep(100); // catnap

				m.Dispose();
			}

			Console.WriteLine("a6");

			// ----
		}
	}
}
