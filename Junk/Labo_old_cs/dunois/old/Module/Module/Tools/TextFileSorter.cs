using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class TextFileSorter : FileSorter<StreamReader, StreamWriter, string>
	{
		private Encoding _encoding;

		public TextFileSorter(Encoding encoding)
		{
			_encoding = encoding;
		}

		protected override StreamReader ReadOpen(string file)
		{
			return new StreamReader(file, _encoding);
		}

		protected override string ReadRecord(StreamReader reader)
		{
			return reader.ReadLine();
		}

		protected override void ReadClose(StreamReader reader)
		{
			reader.Close();
		}

		protected override StreamWriter WriteOpen(string file)
		{
			return new StreamWriter(file, false, _encoding);
		}

		protected override void WriteRecord(StreamWriter writer, string record)
		{
			writer.WriteLine(record);
		}

		protected override void WriteClose(StreamWriter writer)
		{
			writer.Close();
		}

		protected override long GetWeight(string record)
		{
			return 100 + record.Length * 2;
		}

		protected override long GetWeightMax()
		{
			return 50000000; // 50 MB
		}

		protected override int Comp(string str1, string str2)
		{
			return StringTools.Comp(str1, str2);
		}
	}
}
