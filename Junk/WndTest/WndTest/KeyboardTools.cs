using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public static class KeyboardTools
	{
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		private static extern short GetKeyState(int key);

		public static short[][] GetState()
		{
			short[][] ret = new short[2][];

			ret[0] = new short[256];
			ret[1] = new short[256];

			for (int index = 0; index < 256; index++)
			{
				ret[0][index] = GetAsyncKeyState(index);
				ret[1][index] = GetKeyState(index);
			}
			return ret;
		}
	}
}
