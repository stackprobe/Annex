using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		private const string W_FILE = @"C:\temp\t20200622_Test0001_01.csv";

		public void Test01()
		{
			int rowcnt = SecurityTools.CRandom.GetRange(2000, 3000);
			//int rowcnt = SecurityTools.CRandom.GetRange(20000, 30000);
			//int rowcnt = SecurityTools.CRandom.GetRange(200000, 300000);
			//int rowcnt = SecurityTools.CRandom.GetRange(2000000, 3000000);

			using (CsvFileWriter writer = new CsvFileWriter(W_FILE))
			{
				for (int rowidx = 0; rowidx < rowcnt; rowidx++)
				{
					writer.WriteCell(SecurityTools.CRandom.GetRange(1000, 1019).ToString());
					writer.WriteCell(SecurityTools.MakePassword());
					writer.WriteCell(SecurityTools.MakePassword_9());
					writer.WriteCell(SecurityTools.MakePassword_9A());
					writer.EndRow();
				}
			}
		}

		public void Test02()
		{
			using (CsvFileWriter writer = new CsvFileWriter(W_FILE))
			{
				// none
			}
		}
	}
}
