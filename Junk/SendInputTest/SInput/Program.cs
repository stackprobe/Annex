using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}

		private static Queue<string> _argq;

		private static bool ArgIs(string spell)
		{
			if (1 <= _argq.Count && _argq.Peek().ToLower() == spell.ToLower())
			{
				_argq.Dequeue();
				return true;
			}
			return false;
		}

		private static string NextArg()
		{
			return _argq.Dequeue();
		}

		private static void Main2(string[] args)
		{
			_argq = new Queue<string>(args);

			for (; ; )
			{
				if (ArgIs("/MC"))
				{
					int x = IntTools.ToInt(NextArg());
					int y = IntTools.ToInt(NextArg());

					SndInput.MouseCursor(x, y);
					continue;
				}
				if (ArgIs("/LD"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.LeftDown);
					continue;
				}
				if (ArgIs("/LU"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.LeftUp);
					continue;
				}
				if (ArgIs("/RD"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.RightDown);
					continue;
				}
				if (ArgIs("/RU"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.RightUp);
					continue;
				}
				if (ArgIs("/MD"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.MiddleDown);
					continue;
				}
				if (ArgIs("/MU"))
				{
					SndInput.MouseButton(SndInput.MouseButton_e.MiddleUp);
					continue;
				}
				if (ArgIs("/MW"))
				{
					int level = IntTools.ToInt(NextArg(), Consts.WHEEL_LEVEL_MIN, Consts.WHEEL_LEVEL_MAX);

					SndInput.MouseWheel(level);
					continue;
				}
				if (ArgIs("/K"))
				{
					int vk = IntTools.ToInt(NextArg());
					bool downFlag = IntTools.ToInt(NextArg()) != 0;

					SndInput.Keyboard(vk, downFlag);
					continue;
				}
				if (ArgIs("/W"))
				{
					int millis = IntTools.ToInt(NextArg(), 0, Consts.WAIT_MILLIS_MAX);

					Thread.Sleep(millis);
					continue;
				}
				break;
			}
		}
	}
}
