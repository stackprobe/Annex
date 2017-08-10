using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XReader
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main_(args);
			}
			catch (Exception e)
			{
				Console.Write(e.StackTrace);
			}

			Console.WriteLine("\\e");
			Console.ReadLine();
		}

		private void Main_(string[] args)
		{
			//Main2(args);
			//Main3(args);
			//Main4(args);
			//Main5(args);
			Main6(args);
		}

		//public static string SENDXML_DIR = @"C:\etc\墨田\send_sample\backup_[sendxxx]";
		//public static string SENDXML_DIR = @"C:\etc\墨田\send_sample\VPWW53_[sendXml]";
		public static string SENDXML_DIR = @"C:\Projects\PJ08056301_SUMIDA\ws\ftp\log\send";

		public static string OUTPUT_FILE = @"C:\temp\XReaderOut.txt";

		private void Main2(string[] args)
		{
			string[] files = Directory.GetFiles(SENDXML_DIR);

			Array.Sort(files, new ComparerStringIgnoreCase());

			foreach (string file in files)
			{
				//string filePtn = "\\[sendXml]_VPWW50";
				//string filePtn = "\\[sendXml]_VPWW53";
				string filePtn = "\\[sendXml]_";

				if (Tools.ContainsIgnoreCase(file, filePtn))
				{
					Console.WriteLine(file);
					XNode root = XLoader.Load(file);

					//root.DebugPrint(0);

					//VPWW53_01(root);
					Parse_気象系(root);
				}
			}
			File.WriteAllLines(OUTPUT_FILE, OutLines, Encoding.GetEncoding(932));
		}

		private List<string> OutLines = new List<string>();

		private void VPWW53_01(XNode root)
		{
			foreach (XNode warning in root.GetChild("Body").GetChildren("Warning"))
			{
				foreach (XNode item in warning.GetChildren("Item"))
				{
					foreach (XNode kind in item.GetChildren("Kind"))
					{
						OutLines.Add(kind.RefChildText("Name", "(null)") + " = " + kind.RefChildText("Status", "(null)"));
					}
				}
			}
		}

		private void Parse_気象系(XNode root)
		{
			Console.WriteLine("Status: " + root.GetChild("Control").GetChild("Status").Text);
			Console.WriteLine("DateTime: " + root.GetChild("Control").GetChild("DateTime").Text);

			foreach (XNode warning in root.GetChild("Body").GetChildren("Warning"))
			{
				foreach (XNode item in warning.GetChildren("Item"))
				{
					foreach (XNode kind in item.GetChildren("Kind"))
					{
						OutLines.Add("Kind: " + kind.RefChildText("Name", "(null)") + " = " + kind.RefChildText("Status", "(null)"));
					}
					foreach (XNode kind in item.GetChildren("Area"))
					{
						OutLines.Add("Area: " + kind.RefChildText("Name", "(null)") + " = " + kind.RefChildText("Code", "(null)"));
					}
				}
			}
		}

		private void Main3(string[] args)
		{
			string[] files = Directory.GetFiles(@"C:\etc\墨田\send_sample\backup_[sendxxx]");

			foreach (string file in files)
			{
				if (Tools.EndsWithIgnoreCase(file, ".xml"))
				{
					XNode root = XLoader.Load(file);

					foreach (XNode node in root.GetAllNode())
					{
						if (
							Tools.stricmp(node.Name, "AREA") == 0 &&
							node.HasChild("CODE") &&
							node.HasChild("NAME")
							)
						{
							string line = node.RefChildText("CODE", null) + ":" + node.RefChildText("NAME", null);
							Console.WriteLine(line);
							this.OutLines.Add(line);
						}
					}
				}
			}
			File.WriteAllLines(@"C:\temp\AreaCodeName.txt", this.OutLines, Encoding.GetEncoding(932));
		}

		private void Main4(string[] args)
		{
			string[] files = Directory.GetFiles(@"C:\Projects\PJ08056301_SUMIDA\ws\ftp\log\send");
			List<String> outLines = new List<string>();

			foreach (string file in files)
			{
				if (Tools.EndsWithIgnoreCase(file, "_Body.xml"))
				{
					Console.WriteLine("file: " + file);

					XNode root = XLoader.Load(file);
					string reportTime = root.ReferChildren("Control/DateTime")[0].Text;

					foreach (XNode item in root.ReferChildren("Body/Warning/Item"))
					{
						string areaCode = item.ReferChildren("Area/Code")[0].Text;
						string areaName = item.ReferChildren("Area/Name")[0].Text;

						foreach (XNode kind in item.GetChildren("Kind"))
						{
							string code = kind.RefChildText("Code", "(null)");
							string name = kind.RefChildText("Name", "(null)");
							string status = kind.RefChildText("Status", "(null)");

							outLines.Add(Tools.Join(new string[]
							{
								reportTime,
								areaCode,
								areaName,
								status,
								code,
								name,
							},
							", "
							));
						}
					}
				}
			}
			File.WriteAllLines(@"C:\temp\AllAreaKind.txt", outLines, Encoding.GetEncoding(932));
		}

		private void Main5(string[] args)
		{
			string[] files = Directory.GetFiles(@"C:\Projects\PJ08056301_SUMIDA\ws\ftp\log\send");
			List<String> outLines = new List<string>();

			foreach (string file in files)
			{
				if (Tools.EndsWithIgnoreCase(file, "_Body.xml"))
				{
					Console.WriteLine("file: " + file);

					XNode root = XLoader.Load(file);
					string reportTime = root.ReferChildren("Control/DateTime")[0].Text;

					foreach (XNode node in root.GetAllNode())
					{
						if (
							Tools.stricmp(node.Name, "Code") == 0 &&
							node.Parent.HasChild("Name") &&
							(
								node.Parent.HasChild("Int") ||
								node.Parent.HasChild("MaxInt")
							)
							)
						{
							outLines.Add(node.Text + " = " + node.Parent.GetChild("Name").Text);
						}
					}
				}
			}
			File.WriteAllLines(OUTPUT_FILE, outLines, Encoding.GetEncoding(932));
		}

		private void Main6(string[] args)
		{
			// new!
		}
	}
}
