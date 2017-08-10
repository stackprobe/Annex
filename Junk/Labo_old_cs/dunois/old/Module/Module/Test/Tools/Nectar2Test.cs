using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Test.Tools
{
	public class Nectar2Test
	{
		public static void Test01()
		{
			using (Nectar2.Sender sender = new Nectar2.Sender("CURE-MIRACLE"))
			using (Nectar2.Recver recver = new Nectar2.Recver("CURE-MIRACLE", Encoding.ASCII.GetBytes("\n")[0]))
			{
				sender.Send(Encoding.ASCII.GetBytes("[CURE UP^]\n[RA PA PA!]\n"));

				DebugTools.WriteLog("1: " + Encoding.ASCII.GetString(RecvWait(recver)));
				DebugTools.WriteLog("2: " + Encoding.ASCII.GetString(RecvWait(recver)));
			}
		}

		private static byte[] RecvWait(Nectar2.Recver recver)
		{
			for (int c = 0; c < 100; c++)
			{
				byte[] message = recver.Recv();

				if (message != null)
					return message;

				DebugTools.WriteLog("待つンゴ: " + c);
				Thread.Sleep(c);
			}
			throw new Exception("時間掛かり過ぎンゴ");
		}

		/// <summary>
		/// Kirara test 2016.12.15
		/// </summary>
		public static void Test02()
		{
			using (Nectar2.Sender sender = new Nectar2.Sender("{4d7beb3b-fbd9-4d29-8ad3-6c403ed6a281}")) // shared_uuid@ign -- テストなので
			using (Nectar2.Recver recver = new Nectar2.Recver("{54d2a688-3ffc-4c77-85d2-3e24ba4cba85}")) // shared_uuid@ign -- テストなので
			{
				DebugTools.WriteLog("SEND-BEFORE");

				sender.Send(Encoding.ASCII.GetBytes("Kirara Milky-way !"));
				sender.Send(new byte[] { 0x00 });

				DebugTools.WriteLog("SEND-DONE");

				DebugTools.WriteLog("Recv: " + Encoding.ASCII.GetString(RecvWait(recver)));

				DebugTools.WriteLog("RECV-DONE");
			}
		}

		/// <summary>
		/// Factory test 2016.12.16
		/// </summary>
		public static void Test03()
		{
			using (Nectar2.Sender sender = new Nectar2.Sender("Kira_Kira_PRE-CURE_a_la_mode"))
			{
				sender.Send(Encoding.ASCII.GetBytes("123456789"));
				sender.Send(new byte[] { 0x00 });
				sender.Send(Encoding.ASCII.GetBytes("abc"));
				sender.Send(new byte[] { 0x00 });
				sender.Send(Encoding.ASCII.GetBytes("def-ghi"));
				sender.Send(new byte[] { 0x00 });
				sender.Send(StringTools.ENCODING_SJIS.GetBytes("ああああいいいいうううう"));
				sender.Send(StringTools.ENCODING_SJIS.GetBytes("ええええおおおお"));
				sender.Send(StringTools.ENCODING_SJIS.GetBytes("かかかか"));
				sender.Send(new byte[] { 0x00 });

				{
					int c = 0;

					while (sender.IsBusy())
					{
						DebugTools.WriteLog("待つンゴ: " + c);
						Thread.Sleep(c);
						c++;
					}
				}
			}
		}
	}
}
