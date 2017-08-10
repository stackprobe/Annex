using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WinFrmApp1
{
	public class Tools
	{
		public static void WriteLog(string message)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(@"C:\appdata\WinShutdownTest.log", true, Encoding.GetEncoding(932)))
				{
					sw.WriteLine(message);
				}
			}
			catch
			{ }
		}
	}
}
