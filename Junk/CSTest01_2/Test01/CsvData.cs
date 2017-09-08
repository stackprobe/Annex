using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class CsvData
	{
		private static readonly Encoding DEFAULT_ENCODING = Encoding.GetEncoding(932);
		private char Delimiter;
		private List<List<string>> Rows = new List<List<string>>();

		public CsvData(char delimiter = ',')
		{
			this.Delimiter = delimiter;
		}

		public void Clear()
		{
			this.Rows.Clear();
		}

		public void ReadFile(string csvFile)
		{
			this.ReadFile(csvFile, DEFAULT_ENCODING);
		}

		public void ReadFile(string csvFile, Encoding encoding)
		{
			this.Clear();

			using (StreamReader sr = new StreamReader(csvFile, encoding))
			{
				List<string> row = new List<string>();

				for (; ; )
				{
					int chr = ReadChar(sr);

					if (chr == -1)
						break;

					StringBuilder buff = new StringBuilder();

					if (chr == '"')
					{
						for (; ; )
						{
							chr = ReadChar(sr);

							if (chr == -1)
								break;

							if (chr == '"')
							{
								chr = ReadChar(sr);

								if (chr != '"')
									break;
							}
							buff.Append((char)chr);
						}
					}
					else
					{
						for (; ; )
						{
							if (chr == this.Delimiter || chr == '\n')
								break;

							buff.Append((char)chr);
							chr = ReadChar(sr);

							if (chr == -1)
								break;
						}
					}
					row.Add(buff.ToString());

					if (chr == '\n')
					{
						this.Rows.Add(row);
						row = new List<string>();
					}
				}
				if (1 <= row.Count)
					this.Rows.Add(row);
			}
		}

		private static int ReadChar(StreamReader sr)
		{
			int chr;

			do
			{
				chr = sr.Read();
			}
			while (chr == '\r');

			return chr;
		}

		public void WriteFile(string csvFile)
		{
			this.WriteFile(csvFile, DEFAULT_ENCODING);
		}

		public void WriteFile(string csvFile, Encoding encoding)
		{
			using (StreamWriter sw = new StreamWriter(csvFile, false, encoding))
			{
				foreach (List<string> row in this.Rows)
				{
					for (int colidx = 0; colidx < row.Count; colidx++)
					{
						if (1 <= colidx)
							sw.Write(this.Delimiter);

						string cell = row[colidx];

						if (cell.Contains(this.Delimiter) ||
							cell.Contains('"') ||
							cell.Contains('\r') ||
							cell.Contains('\n'))
						{
							string dq2dqdqCell = cell.Replace("\"", "\"\"");

							sw.Write('"');
							sw.Write(dq2dqdqCell);
							sw.Write('"');
						}
						else
							sw.Write(cell);
					}
					sw.Write('\n');
				}
			}
		}

		public void TTR()
		{
			this.TrimAllCell();
			this.Trim();
			this.ToRect();
		}

		public void TrimAllCell()
		{
			foreach (List<string> row in this.Rows)
				for (int colidx = 0; colidx < row.Count; colidx++)
					row[colidx] = row[colidx].Trim();
		}

		public void Trim()
		{
			foreach (List<string> row in this.Rows)
				while (1 <= row.Count && row[row.Count - 1] == "")
					row.RemoveAt(row.Count - 1);

			while (1 <= this.Rows.Count && this.Rows[this.Rows.Count - 1].Count == 0)
				this.Rows.RemoveAt(this.Rows.Count - 1);
		}

		public void ToRect()
		{
			int w = 0;

			foreach (List<string> row in this.Rows)
				w = Math.Max(w, row.Count);

			foreach (List<string> row in this.Rows)
				while (row.Count < w)
					row.Add("");
		}
	}
}
