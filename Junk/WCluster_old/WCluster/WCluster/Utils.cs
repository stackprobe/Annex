using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte
{
	public static class Utils
	{
		public delegate void operation_d();

		public static string getHash(byte[] src)
		{
			return StringTools.toHex(BinaryTools.getSubBytes(SecurityTools.getSHA512(src), 0, 16));
		}

		public static void adjustColumnsWidth(DataGridView sheet)
		{
			for (int colidx = 0; colidx < sheet.ColumnCount; colidx++)
			{
				if (sheet.RowCount == 0)
				{
					sheet.Columns[colidx].Width = 200;
				}
				else
				{
					sheet.Columns[colidx].Width = 10000; // 一旦思いっきり広げてからでないとダメな時がある。
					sheet.AutoResizeColumn(colidx, DataGridViewAutoSizeColumnMode.AllCells);

					if (sheet.Columns[colidx].Width < 100)
						sheet.Columns[colidx].Width = 100;
				}
			}
		}

		public static Consts.Combination_e toCombination(string src)
		{
			switch (src)
			{
				case "AND":
					return Consts.Combination_e.AND;

				case "OR":
					return Consts.Combination_e.OR;
			}
			throw new FormatException("指定されたコンビネーションは不明です。");
		}

		public static string toString(Consts.Combination_e src)
		{
			switch (src)
			{
				case Consts.Combination_e.AND:
					return "AND";

				case Consts.Combination_e.OR:
					return "OR";
			}
			throw null;
		}

		public static string toEllipsis(string src, int lenmax = 100, int eMargin = 10, string eTrailer = " ...")
		{
			if (lenmax < src.Length)
				return src.Substring(lenmax - eMargin) + eTrailer;

			return src;
		}

		private static long _lastPosix = -1L;

		public static long getUniquePosix()
		{
			long ret = DateTimeToSec.Now.getPosix();
			ret = Math.Max(ret, _lastPosix + 1L);
			_lastPosix = ret;
			return ret;
		}
	}
}
