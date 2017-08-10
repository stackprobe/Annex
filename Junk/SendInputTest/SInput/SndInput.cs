using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public class SndInput
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct MOUSEINPUT
		{
			public int dx;
			public int dy;
			public int mouseData;
			public int dwFlags;
			public int time;
			public int dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		private struct KEYBDINPUT
		{
			public short wVk;
			public short wScan;
			public int dwFlags;
			public int time;
			public int dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		private struct HARDWAREINPUT
		{
			public int uMsg;
			public short wParamL;
			public short wParamH;
		};

		[StructLayout(LayoutKind.Explicit)]
		private struct INPUT
		{
			[FieldOffset(0)]
			public int type;
			[FieldOffset(4)]
			public MOUSEINPUT mi;
			[FieldOffset(4)]
			public KEYBDINPUT ki;
			[FieldOffset(4)]
			public HARDWAREINPUT hi;
		};

		[DllImport("user32.dll")]
		private extern static void SendInput(int nInputs, ref INPUT pInputs, int cbsize);

		[DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
		private extern static int MapVirtualKey(int wCode, int wMapType);

		private const int INPUT_MOUSE = 0;
		private const int INPUT_KEYBOARD = 1;
		private const int INPUT_HARDWARE = 2;

		private const int MOUSEEVENTF_MOVE = 0x01;
		private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;
		private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
		private const int MOUSEEVENTF_MIDDLEUP = 0x40;
		private const int MOUSEEVENTF_WHEEL = 0x800;
		private const int WHEEL_DELTA = 120;

		private const int KEYEVENTF_KEYDOWN = 0x00;
		private const int KEYEVENTF_KEYUP = 0x02;
		private const int KEYEVENTF_EXTENDEDKEY = 0x01;

		public const int VK_BACK = 0x08;
		public const int VK_TAB = 0x09;
		public const int VK_CLEAR = 0x0c;
		public const int VK_RETURN = 0x0d;
		public const int VK_SHIFT = 0x10;
		public const int VK_CONTROL = 0x11;
		public const int VK_MENU = 0x12;
		public const int VK_PAUSE = 0x13;
		public const int VK_CAPITAL = 0x14;
		public const int VK_KANA = 0x15;
		public const int VK_KANJI = 0x19;
		public const int VK_ESCAPE = 0x1b;
		public const int VK_CONVERT = 0x1c;
		public const int VK_NCONVERT = 0x1d;
		public const int VK_SPACE = 0x20;
		public const int VK_PRIOR = 0x21;
		public const int VK_NEXT = 0x22;
		public const int VK_END = 0x23;
		public const int VK_HOME = 0x24;
		public const int VK_LEFT = 0x25;
		public const int VK_UP = 0x26;
		public const int VK_RIGHT = 0x27;
		public const int VK_DOWN = 0x28;
		public const int VK_SELECT = 0x29;
		public const int VK_PRINT = 0x2a;
		public const int VK_EXECUTE = 0x2b;
		public const int VK_SNAPSHOT = 0x2c;
		public const int VK_INSERT = 0x2d;
		public const int VK_DELETE = 0x2e;
		public const int VK_HELP = 0x2f;
		public const int VK_0 = 0x30;
		// ...
		public const int VK_9 = 0x39;
		public const int VK_A = 0x40;
		// ...
		public const int VK_Z = 0x5a;
		public const int VK_LWIN = 0x5b;
		public const int VK_RWIN = 0x5c;
		public const int VK_F1 = 0x70;
		public const int VK_F2 = 0x71;
		public const int VK_F3 = 0x72;
		public const int VK_F4 = 0x73;
		public const int VK_F5 = 0x74;
		public const int VK_F6 = 0x75;
		public const int VK_F7 = 0x76;
		public const int VK_F8 = 0x77;
		public const int VK_F9 = 0x78;
		public const int VK_F10 = 0x79;
		public const int VK_F11 = 0x7a;
		public const int VK_F12 = 0x7b;
		public const int VK_F13 = 0x7c;
		public const int VK_F14 = 0x7d;
		public const int VK_F15 = 0x7e;
		public const int VK_F16 = 0x7f;
		public const int VK_F17 = 0x80;
		public const int VK_F18 = 0x81;
		public const int VK_F19 = 0x82;
		public const int VK_F20 = 0x83;
		public const int VK_F21 = 0x84;
		public const int VK_F22 = 0x85;
		public const int VK_F23 = 0x86;
		public const int VK_F24 = 0x87;
		public const int VK_NUMLOCK = 0x90;
		public const int VK_SCROLL = 0x91;
		public const int VK_LSHIFT = 0xa0;
		public const int VK_RSHIFT = 0xa1;
		public const int VK_LCONTROL = 0xa2;
		public const int VK_RCONTROL = 0xa3;
		public const int VK_LMENU = 0xa4;
		public const int VK_RMENU = 0xa5;
#if false
		public const int VK_SEMICOLON = 0xba;
		public const int VK_EQUAL = 0xbb;
		public const int VK_APOSTROPHE = 0xde;
#else
		public const int VK_OEM_1 = 0xba;
		public const int VK_OEM_PLUS = 0xbb;
		public const int VK_OEM_MINUS = 0xbd;
		public const int VK_OEM_7 = 0xde;
		public const int VK_SEMICOLON = 0xbb;
		public const int VK_EQUAL = 0xde;
		public const int VK_OEM_APOSTROPHE = 0xba;
#endif
		public const int VK_HYPHEN = 0xbd;
		public const int VK_COMMA = 0xbc;
		public const int VK_PERIOD = 0xbe;
		public const int VK_SLASH = 0xbf;
		public const int VK_BACKQUOTE = 0xc0;
		public const int VK_APP1 = 0xc1;
		public const int VK_APP2 = 0xc2;
		public const int VK_APP3 = 0xc3;
		public const int VK_APP4 = 0xc4;
		public const int VK_APP5 = 0xc5;
		public const int VK_APP6 = 0xc6;
		public const int VK_LBRACKET = 0xdb;
		public const int VK_BACKSLASH = 0xdc;
		public const int VK_RBRACKET = 0xdd;
		public const int VK_APOSTROPHE = 0xde;
		public const int VK_OFF = 0xdf;
		public const int VK_OEM_102 = 0xe2;
		public const int VK_DBE_ALPHANUMERIC = 0xf0;
		public const int VK_DBE_KATAKANA = 0xf1;
		public const int VK_DBE_HIRAGANA = 0xf2;
		public const int VK_DBE_SBCSCHAR = 0xf3;
		public const int VK_DBE_DBCSCHAR = 0xf4;
		public const int VK_DBE_ROMAN = 0xf5;
		public const int VK_DBE_NOROMAN = 0xf6;

		public static void MouseCursor(int x, int y)
		{
			INPUT[] i = new INPUT[1];

			i[0].type = INPUT_MOUSE;
			i[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
			i[0].mi.dx = IntTools.ToInt(x * (65535.0 / Screen.PrimaryScreen.Bounds.Width));
			i[0].mi.dy = IntTools.ToInt(y * (65535.0 / Screen.PrimaryScreen.Bounds.Height));
			i[0].mi.mouseData = 0;
			i[0].mi.dwExtraInfo = 0;
			i[0].mi.time = 0;

			SendInput(1, ref i[0], Marshal.SizeOf(i[0]));
		}

		public enum MouseButton_e
		{
			LeftDown = MOUSEEVENTF_LEFTDOWN,
			LeftUp = MOUSEEVENTF_LEFTUP,
			RightDown = MOUSEEVENTF_RIGHTDOWN,
			RightUp = MOUSEEVENTF_RIGHTUP,
			MiddleDown = MOUSEEVENTF_MIDDLEDOWN,
			MiddleUp = MOUSEEVENTF_MIDDLEUP,
		};

		public static void MouseButton(MouseButton_e action)
		{
			INPUT[] i = new INPUT[1];

			i[0].type = INPUT_MOUSE;
			i[0].mi.dwFlags = (int)action;
			i[0].mi.dx = 0;
			i[0].mi.dy = 0;
			i[0].mi.mouseData = 0;
			i[0].mi.dwExtraInfo = 0;
			i[0].mi.time = 0;

			SendInput(1, ref i[0], Marshal.SizeOf(i[0]));
		}

		public static void MouseWheel(int level) // level: -1 == 手前に1コロ, 1 == 奥へ1コロ
		{
			INPUT[] i = new INPUT[1];

			i[0].type = INPUT_MOUSE;
			i[0].mi.dwFlags = MOUSEEVENTF_WHEEL;
			i[0].mi.dx = 0;
			i[0].mi.dy = 0;
			i[0].mi.mouseData = level * WHEEL_DELTA;
			i[0].mi.dwExtraInfo = 0;
			i[0].mi.time = 0;

			SendInput(1, ref i[0], Marshal.SizeOf(i[0]));
		}

		public static void Keyboard(int vk, bool downFlag)
		{
			if (vk < 0x00 || 0xff < vk)
				throw new ArgumentOutOfRangeException();

			INPUT[] i = new INPUT[1];

			i[0].type = INPUT_KEYBOARD;
			i[0].ki.wVk = (short)vk;
			i[0].ki.wScan = (short)MapVirtualKey(i[0].ki.wVk, 0);
			i[0].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | (downFlag ? KEYEVENTF_KEYDOWN : KEYEVENTF_KEYUP);
			i[0].ki.dwExtraInfo = 0;
			i[0].ki.time = 0;

			SendInput(1, ref i[0], Marshal.SizeOf(i[0]));
		}
	}
}
