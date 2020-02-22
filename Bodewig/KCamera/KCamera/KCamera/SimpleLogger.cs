using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class SimpleLogger
	{
		public string LogFile;

		// <---- prm

		public void WriteLog(string line)
		{
			using (StreamWriter writer = new StreamWriter(this.LogFile, true, Encoding.UTF8))
			{
				writer.WriteLine("[" + DateTime.Now + "] " + line);
			}
		}
	}
}
