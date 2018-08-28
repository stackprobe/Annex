using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class GISTxtReader : IDisposable
	{
		private FileStream Reader = null;

		public GISTxtReader(string file)
		{
			this.Reader = new FileStream(file, FileMode.Open, FileAccess.Read);
		}

		public byte[] Word = null;

		public bool Read_EOF(int width)
		{
			this.Word = new byte[width];
			int ret = this.Reader.Read(this.Word, 0, width);

			if (ret == 0)
				return false;

			if (ret != width)
				throw new Exception("I/O エラー");

			return true;
		}

		public void Read(int width)
		{
			if (this.Read_EOF(width) == false)
				throw new Exception("ファイルの途中で EOF に到達した。");
		}

		public void ReadNewLine()
		{
			this.Read(2);

			if (
				this.Word[0] != 0x0d || // CR
				this.Word[1] != 0x0a // LF
				)
				throw new Exception("改行読み込みエラー");
		}

		public string GetString()
		{
			return StringTools.ENCODING_SJIS.GetString(this.Word).Trim();
		}

		public int GetInt()
		{
			return int.Parse(this.GetString());
		}

		public int GetInt_Empty()
		{
			string tmp = this.GetString();

			if (tmp == "")
				return -1;

			return int.Parse(tmp);
		}

		public void Dispose()
		{
			if (this.Reader != null)
			{
				this.Reader.Dispose();
				this.Reader = null;
			}
		}
	}
}
