using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			ProcMain.GUIMain(() => new MainWin(), APP_IDENT, APP_TITLE);
		}

		public const string APP_IDENT = "{d79b4fc0-1ed4-413d-ab52-ebd36d9c8af8}";
		public const string APP_TITLE = "MapViewTest2";
	}
}
