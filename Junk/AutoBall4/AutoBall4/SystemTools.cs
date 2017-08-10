using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AutoBall4
{
	public class SystemTools
	{
		public static void Execute(string file, string args)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = file;
			psi.Arguments = args;
			psi.WindowStyle = ProcessWindowStyle.Minimized;

			Process.Start(psi).WaitForExit();
		}
	}
}
