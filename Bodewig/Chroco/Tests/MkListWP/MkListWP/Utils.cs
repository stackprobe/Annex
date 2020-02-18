using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Charlotte
{
	public static class Utils
	{
		// sync > @ UISuspend

		public class UISuspend : IDisposable
		{
			[DllImport("user32.dll", EntryPoint = "SendMessageA")]
			private static extern uint SendMessage(IntPtr hWnd, uint wMsg, uint wParam, uint lParam);

			private const uint WM_SETREDRAW = 0x000B;

			private static void SetWindowRedraw(IWin32Window window, bool fRedraw)
			{
				if ((window != null) && (window.Handle != IntPtr.Zero))
				{
					SendMessage(window.Handle, WM_SETREDRAW, (fRedraw) ? 1u : 0u, 0u);
				}
			}

			private Control _ctrl;

			public UISuspend(Control ctrl)
			{
				SetWindowRedraw(ctrl, false);
				_ctrl = ctrl;
			}

			public void Dispose()
			{
				if (_ctrl != null)
				{
					SetWindowRedraw(_ctrl, true);
					_ctrl.Refresh();
					_ctrl = null;
				}
			}
		}

		// < sync
	}
}
