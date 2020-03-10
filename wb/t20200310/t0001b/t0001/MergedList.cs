using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Charlotte.Tools;
using System.Data;

namespace Charlotte
{
	public class MergedList
	{
		//private const string MERGED_HTML_FILE = @"C:\wb2\20200306_Merged\Merged.html";
		private const string MERGED_HTML_FILE = @"C:\wb2\20200310_Merged_mini\Merged.html"; // test

		public class RowInfo
		{
			public bool Selected;
			public string ImageLFile; // null == 存在しない。
			public string ImageRFile; // null == 存在しない。
			public Bitmap ThumbL;
			public Bitmap ThumbR;
		}

		public List<RowInfo> Rows = new List<RowInfo>();

		public MergedList()
		{
			RowInfo row = null;
			int lr = -1;

			foreach (string line in File.ReadAllLines(MERGED_HTML_FILE))
			{
				if (line == "<tr>")
				{
					row = new RowInfo();
					lr = -1;
				}
				else if (line == "<td>")
				{
					lr++;
				}
				else if (line == "</tr>")
				{
					row.ThumbL = GetDefThumb_IfNull(row.ThumbL);
					row.ThumbR = GetDefThumb_IfNull(row.ThumbR);

					this.Rows.Add(row);
					row = null;
				}
				else if (line.StartsWith("<a href=\""))
				{
					string imageFile = StringTools.GetEnclosed(line, "<a href=\"", "\">").Inner;
					string thumbFile = StringTools.GetEnclosed(line, "<img src=\"", "\">").Inner;

					Bitmap thumb = new Bitmap(Path.Combine(Path.GetDirectoryName(MERGED_HTML_FILE), thumbFile));

					if (lr == 0)
					{
						row.ImageLFile = imageFile;
						row.ThumbL = thumb;
					}
					else if (lr == 1)
					{
						row.ImageRFile = imageFile;
						row.ThumbR = thumb;
					}
					else
					{
						throw null; // never
					}
				}
			}
		}

		private static Bitmap GetDefThumb_IfNull(Bitmap thumb)
		{
			if (thumb == null)
				thumb = GetDefThumb();

			return thumb;
		}

		private static Bitmap _thumb = null;

		private static Bitmap GetDefThumb()
		{
			if (_thumb == null)
			{
				_thumb = new Bitmap(100, 100);

				using (Graphics g = Graphics.FromImage(_thumb))
				{
					g.FillRectangle(Brushes.Yellow, 0, 0, 100, 100);
				}
			}
			return _thumb;
		}

		public DataTable GetDataSource()
		{
			DataTable table = new DataTable();

			table.Columns.Add("MSC-Selected");
			table.Columns.Add("MSC-ImageL", typeof(Bitmap));
			table.Columns.Add("MSC-ImageR", typeof(Bitmap));

			foreach (RowInfo row in this.Rows)
			{
				DataRow dRow = table.NewRow();

				dRow.ItemArray = new object[]
				{
					row.Selected ? MainWin.MS_S_SELECTED : MainWin.MS_S_NOT_SELECTED,
					row.ThumbL,
					row.ThumbR,
				};

				table.Rows.Add(dRow);
			}
			return table;
		}
	}
}
