using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0002
	{
		private const string R_FILE = @"C:\temp\t20200622_Test0001_01.csv";
		private const string W_FILE = @"C:\temp\t20200622_Test0001_02.csv";

		private CacheMap<string, HugeQueue> QueueMap;

		public void Test01()
		{
			QueueMap = DictionaryTools.CreateCache(ident => new HugeQueue());
			try
			{
				using (CsvFileReader reader = new CsvFileReader(R_FILE))
				{
					for (; ; )
					{
						string[] row = reader.ReadRow();

						if (row == null)
							break;

						string ident = row[0];
						string[] trailer = row.Skip(1).ToArray();

						HugeQueue q = QueueMap[ident];

						q.Enqueue(BinTools.SplittableJoin(trailer.Select(v => Encoding.UTF8.GetBytes(v)).ToArray()));
					}
				}

				string[] idents = QueueMap.Keys.ToArray();

				Array.Sort(idents, StringTools.Comp);

				using (CsvFileWriter writer = new CsvFileWriter(W_FILE))
				{
					foreach (string ident in idents)
					{
						HugeQueue q = QueueMap[ident];

						while (q.HasElements())
						{
							string[] trailer = BinTools.Split(q.Dequeue()).Select(v => Encoding.UTF8.GetString(v)).ToArray();

							writer.WriteCell(ident);
							writer.WriteCells(trailer);
							writer.EndRow();
						}
					}
				}
			}
			finally
			{
				foreach (HugeQueue q in QueueMap.Values)
				{
					q.Dispose();
				}
				QueueMap = null;
			}
		}
	}
}
