using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class CsvDataTest
	{
		public static void Test01()
		{
			CsvData csv = new CsvData();

			csv.Table.AddRow();
			csv.Table.Add("abc");
			csv.Table.Add("def");
			csv.Table.Add("ghi");

			csv.Table.AddRow();
			csv.Table.Add("");
			csv.Table.Add("\n");
			csv.Table.Add("\"");

			csv.Table.AddRow();
			csv.Table.Add("1");
			csv.Table.Add("2222");
			csv.Table.Add("333333333");

			csv.WriteFile(@"C:\temp\1.csv");

			csv = new CsvData();
			csv.ReadFile(@"C:\temp\1.csv");
			csv.WriteFile(@"C:\temp\2.csv");
		}
	}
}
