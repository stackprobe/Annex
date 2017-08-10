using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Test.Tools
{
	public class MutectorTest
	{
		private static bool[] _death = new bool[1];

		public static void Test01()
		{
			using (Mutector.Sender sender = new Mutector.Sender("キュア☆マジカル"))
			using (Mutector.Recver recver = new Mutector.Recver("キュア☆マジカル"))
			{
				_death[0] = false;

				recver.SetRecver(new Test01Recver());

				Thread th = new Thread((ThreadStart)delegate
				{
					try
					{
						recver.Perform();
					}
					catch (Exception e)
					{
						DebugTools.WriteLog("recver_e: " + e);
					}
				});

				th.Start();
				try
				{
					// recver の受信開始前に送信すると例外を投げる！

					Thread.Sleep(100); // recver の開始待ち。待ち時間は適当！

					sender.Send(Encoding.UTF8.GetBytes("#"));
					sender.Send(Encoding.UTF8.GetBytes("123"));
					sender.Send(Encoding.UTF8.GetBytes("ABCDEF"));
					sender.Send(Encoding.UTF8.GetBytes("リズ先生の秘密が茂る宝島(ワンダーランド)を、僕のリンクルステッキで探検&冒険したい！"));

					// この時点で recver の受信は完了している。-- Test01Recver.Interlude が false を返しても良い！
				}
				finally
				{
					_death[0] = true;
					th.Join();
				}
			}
		}

		private class Test01Recver : Mutector.IRecver
		{
			public bool Interlude()
			{
				return _death[0] == false;
			}

			public void Recved(byte[] message)
			{
				DebugTools.WriteLog(Encoding.UTF8.GetString(message));
			}
		}

		/// <summary>
		/// 対 Factory
		/// </summary>
		public static void Test02()
		{
			using (Mutector.Recver recver = new Mutector.Recver("Mutector_Test"))
			{
				recver.SetRecver(new Test02Recver());
				recver.Perform();
			}
		}

		private class Test02Recver : Mutector.IRecver
		{
			public bool Interlude()
			{
				return Console.KeyAvailable == false;
			}

			public void Recved(byte[] message)
			{
				Console.WriteLine("[" + StringTools.ENCODING_SJIS.GetString(message) + "]");
				//DebugTools.WriteLog(StringTools.ENCODING_SJIS.GetString(message));
			}
		}
	}
}
