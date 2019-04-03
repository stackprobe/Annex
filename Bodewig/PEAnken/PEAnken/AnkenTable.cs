using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using Charlotte.Utils;

namespace Charlotte
{
	public class AnkenTable
	{
		public void AddText(string text)
		{
			StringTools.Island[] islands = StringTools.GetAllIsland(text, "https://");

			foreach (StringTools.Island island in islands)
			{
				string url = island.Inner + island.Right;
				int urlEndIndex = StringTools.IndexOf(url, chr => chr <= ' ');

				if (urlEndIndex != -1)
					url = url.Substring(0, urlEndIndex);

				AddUrl(url);
			}
		}

		public void AddUrl(string url)
		{
			string html;

			{
				HTTPClient hc = new HTTPClient(url);
				hc.Get();
				html = Encoding.UTF8.GetString(hc.ResBody);
			}

			string csv;

			{
				StringTools.Enclosed[] encls = StringTools.GetAllEnclosed(html, "'./csv/", "'");

				if (encls.Length != 2)
					throw new Exception("encl_num: " + encls.Length);

				string csvUrl = url = url.Substring(0, url.LastIndexOf('/')) + "/csv/" + encls[1].Inner;

				{
					HTTPClient hc = new HTTPClient(csvUrl);
					hc.Get();
					csv = Encoding.UTF8.GetString(hc.ResBody); // .csv だけど UTF-8
				}
			}

			using (WorkingDir wd = new WorkingDir())
			{
				string csvFile = wd.MakePath();

				File.WriteAllText(csvFile, csv, Encoding.UTF8);

				using (CsvFileReader reader = new CsvFileReader(csvFile, Encoding.UTF8))
				{
					// ヘッダ
					{
						string[] row = reader.ReadRow();

						if (row.Length != 4) throw null;
						if (row[0] != "案件番号") throw null;
						if (row[1] != "担当営業") throw null;
						if (row[2] != "タイトル") throw null;
						//if (row[3] != "案件内容") throw null; // 何かタグとか付いてる。
					}

					for (; ; )
					{
						string[] row = reader.ReadRow();

						if (row == null)
							break;

#if false // シンプル
						StartRow();
						AddCell("案件番号", row[0]);
						AddCell("営業担当", row[1]);
						AddCell("タイトル", row[2]);
						AddCell("案件内容", row[3]);
						EndRow();
#else
						StartRow();
						AddCell("案件番号", StringTools.GetEnclosed(row[0], ">", "<").Inner);
						AddCell("営業担当", row[1]);
						AddCell("タイトル", row[2]);

						{
							StringTools.Enclosed encl = StringTools.GetEnclosed(row[2], "【", "】");

							if (encl != null)
								AddCell("言語", encl.Inner);
						}

						{
							string rawText = row[3];
							int rawTextStartIndex = rawText.IndexOf("【勤務地】");

							if (rawTextStartIndex != -1)
								rawText = rawText.Substring(rawTextStartIndex);

							StringTools.Enclosed[] encls = StringTools.GetAllEnclosed(rawText, "【", "】");

							foreach (StringTools.Enclosed encl in encls)
							{
								string title = encl.Inner;
								string cell = encl.EndPtn.Right;
								int cellEndIndex = cell.IndexOf("【");

								if (cellEndIndex != -1)
									cell = cell.Substring(0, cellEndIndex);

								cell = cell.Replace("\t", "");
								cell = cell.Replace("\r", "");
								cell = cell.Replace("\n", "");
								cell = cell.Replace(" ", "");
								cell = cell.Replace("　", "");
								cell = cell.Replace("<br/>", " ");
								cell = cell.Trim();

								if (20 < title.Length)
								{
									cell = title + cell;
									title = title.Substring(0, 10) + " ...";
								}
								AddCell(title, cell);
							}
						}

						EndRow();
#endif
					}
				}
			}
		}

		private List<string> CellTitles = null;
		private List<string> Cells = null;

		private void StartRow()
		{
			CellTitles = new List<string>();
			Cells = new List<string>();
		}

		private void AddCell(string title, string cell)
		{
			CellTitles.Add(title);
			Cells.Add(cell);
		}

		private void EndRow()
		{
			List<string> row = new List<string>();

			for (int index = 0; index < CellTitles.Count; index++)
			{
				string title = CellTitles[index];
				string cell = Cells[index];

				int colidx = ArrayTools.IndexOf(Header.ToArray(), t => t == title);

				if (colidx == -1)
				{
					colidx = Header.Count;
					Header.Add(title);
				}
				while (row.Count <= colidx)
					row.Add("");

				row[colidx] += cell;
			}
			Rows.Add(row.ToArray());
		}

		private List<string> Header = new List<string>();
		private List<string[]> Rows = new List<string[]>();

		public HeaderTable GetTable()
		{
			Normalize();

			return new HeaderTable()
			{
				Header = Header.ToArray(),
				Rows = Rows.ToArray(),
			};
		}

		private void Normalize()
		{
			for (int rowidx = 0; rowidx < Rows.Count; rowidx++)
			{
				string[] row = Rows[rowidx];

				if (row.Length < Header.Count)
				{
					List<string> cells = new List<string>(row);

					while (cells.Count < Header.Count)
						cells.Add("");

					Rows[rowidx] = cells.ToArray();
				}
			}
		}
	}
}
