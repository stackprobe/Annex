using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public class MouseTools
	{
		private struct POINT
		{
			public int X;
			public int Y;
		}

		public class Point
		{
			public int X;
			public int Y;
		}

		[DllImport("user32.dll")]
		private extern static bool GetCursorPos(out POINT lpPoint);

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern void SetCursorPos(int x, int y);

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;

		public static Point GetCursor()
		{
			POINT pt;

			if (GetCursorPos(out pt) == false) // ? 失敗
				return null;

			Point ret = new Point();

			ret.X = pt.X;
			ret.Y = pt.Y;

			return ret;
		}

		public static void SetCursor(Point pt)
		{
			SetCursorPos(pt.X, pt.Y);
		}

		public static void LeftDown()
		{
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
		}

		public static void LeftUp()
		{
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}

		public static void RightDown()
		{
			mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
		}

		public static void RightUp()
		{
			mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
		}
	}
}
