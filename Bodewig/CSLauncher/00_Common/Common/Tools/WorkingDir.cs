using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	public class WorkingDir
	{
		private string Dir;

		public WorkingDir(string appIdent)
		{
			this.Dir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), appIdent + "_" + Process.GetCurrentProcess().Id);
		}
	}
}
