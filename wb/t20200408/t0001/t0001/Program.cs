using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{5fd91ecf-0c7e-4974-94b5-74bb55bd0776}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			using (CsvFileReader reader = new CsvFileReader(@"C:\wb2\20200306_Merged\Merged.csv"))
			{
				//string onlyLDir = @"C:\temp\OnlyL";
				string onlyRDir = @"C:\temp\OnlyR";

				//FileTools.Delete(onlyLDir);
				FileTools.Delete(onlyRDir);
				//FileTools.CreateDir(onlyLDir);
				FileTools.CreateDir(onlyRDir);

				List<string> dest = new List<string>();

				dest.Add("<table>");

				for (; ; )
				{
					string[] row = reader.ReadRow();

					if (row == null)
						break;

					string lFile = row[0];
					string rFile = row[1];

					dest.Add("<tr>");

					if (lFile == "")
					{
						if (rFile == "")
							throw null; // souteigai !!!

						dest.Add(
							"<td>N</td>" +
							"<td><a href=\"" + rFile + "\"><img src=\"" + rFile + "\" /></a></td>"
							);

						File.Copy(rFile, Path.Combine(onlyRDir, Path.GetFileName(rFile)));
					}
					else if (rFile == "")
					{
						dest.Add(
							"<td><a href=\"" + lFile + "\"><img src=\"" + lFile + "\" /></a></td>" +
							"<td>N</td>"
							);

						//File.Copy(lFile, Path.Combine(onlyLDir, Path.GetFileName(lFile)));
					}
					else
					{
						dest.Add("<td>N</td><td>N</td>");
					}
					dest.Add("</tr>");
				}
				dest.Add("</table>");

				File.WriteAllLines(@"C:\temp\OnlyLR.html", dest, Encoding.UTF8);
			}
		}
	}
}
