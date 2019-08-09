using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] SplitableJoin(string[] lines)
		{
			return BinTools.Join(lines.Select(line => Encoding.UTF8.GetBytes(line)).ToArray());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string[] Split(byte[] data)
		{
			return BinTools.Split(data).Select(bLine => Encoding.UTF8.GetString(bLine)).ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Noop(params object[] dummyPrms)
		{
			// noop
		}
	}
}
