using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace BusyDlg
{
	public class Gnd
	{
		private Gnd()
		{ }

		public static Gnd I = new Gnd();

		public string SelfFile;
		public int ExecuteMode;
		public string Message;

		public static readonly int CREDIBLE_TIMEOUT_MILLIS = 60000;
		public static readonly int INCREDIBLE_TIMEOUT_MILLIS = 2000;

		public static readonly string DLG_OPENED_EVENT_NAME = "{3d0031db-12af-47e6-beee-01920674f181}";
		public EventWaitHandle DlgOpened;

		public static readonly string CLOSE_DLG_EVENT_NAME = "{a21364e0-f870-4882-8452-4f701ed98e77}";
		public EventWaitHandle CloseDlg;

		public static readonly string DLG_CLOSED_EVENT_NAME = "{c4c18ad3-c1f5-4c5d-8d9e-b11ff55b87e3}";
		public EventWaitHandle DlgClosed;

		public static readonly string DLG_OPEN_CLOSE_MUTEX_NAME = "{ca3635c8-ed1d-440a-84c8-0156205da0ff}";
		public Mutex DlgOpenCloseMutex;

		public bool IsDlgOpened()
		{
			bool ret = false;

			try
			{
				if (Tools.Contains(Tools.GetAllMainWindowTitle(), "　"))
					ret = true;
			}
			catch
			{ }

			return ret;
		}
	}
}
