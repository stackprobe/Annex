using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public static class WindowTools
	{
		private delegate bool EnumWindowsCallBack_d(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private extern static bool EnumWindows(EnumWindowsCallBack_d d_enumWindowsCallBack, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private extern static bool EnumChildWindows(IntPtr hWnd, EnumWindowsCallBack_d d_enumChildWindowsCallBack, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lPString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lPClassName, int nMaxCount);

		private static List<Info> Infos;
		private static Info Parent;

		public static List<Info> FindWindows()
		{
			Infos = new List<Info>();
			Parent = null;

			EnumWindows(EnumWindowsCallBack, IntPtr.Zero);

			List<Info> ret = Infos;
			Infos = null;
			Parent = null;
			return ret;
		}

		private static bool EnumWindowsCallBack(IntPtr hWnd, IntPtr lParam)
		{
			Info info = new Info();

			{
				int len = GetWindowTextLength(hWnd);

				if (1 <= len)
				{
					StringBuilder buff = new StringBuilder(len + 1);
					GetWindowText(hWnd, buff, buff.Capacity);
					info.Title = buff.ToString();
				}
			}

			{
				StringBuilder buff = new StringBuilder(1024);
				GetClassName(hWnd, buff, buff.Capacity);
				info.ClassName = buff.ToString();
			}

			info.Text = GetTextTools.Perform(hWnd);
			info.Rect = GetRect(hWnd);
			info.HWnd = hWnd;
			info.Parent = Parent;

			Infos.Add(info);

			Info bkParent = Parent;
			Parent = info;
			EnumChildWindows(hWnd, EnumWindowsCallBack, IntPtr.Zero);
			Parent = bkParent;

			return true;
		}

		public class Info
		{
			public string Title;
			public string ClassName;
			public string Text;
			public Rect Rect;
			public IntPtr HWnd;
			public Info Parent;
		}

		public class Rect
		{
			public int L;
			public int T;
			public int W;
			public int H;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int L;
			public int T;
			public int R;
			public int B;
		}

		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

		public static Rect GetRect(IntPtr hWnd)
		{
			RECT rect;

			if (GetWindowRect(hWnd, out rect) == false) // ? 失敗
				return null;

			Rect ret = new Rect();

			ret.L = rect.L;
			ret.T = rect.T;
			ret.W = rect.R - rect.L;
			ret.H = rect.B - rect.T;

			return ret;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		public static void ToTop(IntPtr hWnd)
		{
			SetForegroundWindow(hWnd);
		}
	}
}
