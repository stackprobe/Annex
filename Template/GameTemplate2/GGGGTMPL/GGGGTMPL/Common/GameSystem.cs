﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Charlotte.Common
{
	public static class GameSystem
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void PinOn<T>(T data, Action<IntPtr> routine)
		{
			GCHandle pinnedData = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				routine(pinnedData.AddrOfPinnedObject());
			}
			finally
			{
				pinnedData.Free();
			}
		}
	}
}
