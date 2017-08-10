using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class DebugTools
	{
#if DEBUG
		public static void WriteLog_Console(string line)
		{
			Console.WriteLine(line);
			WriteLog(line);
		}

		private static object WriteLog_SYNCROOT = new object();
		private static bool WriteLog_Wrote = false;

		public static void WriteLog(string line)
		{
			lock (WriteLog_SYNCROOT)
			{
				FileMode mode = WriteLog_Wrote ? FileMode.Append : FileMode.Create;

				using (FileStream writer = new FileStream(@"C:\temp\Module.log", mode, FileAccess.Write))
				{
					FileTools.Write(
						writer,
						StringTools.ENCODING_SJIS.GetBytes("[" + DateTimeTools.GetCommonString_Millis(DateTime.Now) + "] " + line + "\r\n")
						);

					WriteLog_Wrote = true;
				}
			}
		}
#else
		public static void WriteLog_Console(string line)
		{ }

		public static void WriteLog(string line)
		{ }
#endif

		public static void MakeRandTextFile(string file, Encoding encoding, string chrs, string lineNew, int linecnt, int chrcntMin, int chrcntMax)
		{
			byte[] bLineNew = encoding.GetBytes(lineNew);

			using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				for (int index = 0; index < linecnt; index++)
				{
					FileTools.Write(fs, encoding.GetBytes(MakeRandString(chrs, MathTools.Random(chrcntMin, chrcntMax))));
					FileTools.Write(fs, bLineNew);
				}
			}
		}

		public static string MakeRandString(string chrs, int chrcnt)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < chrcnt; index++)
			{
				buff.Append(chrs[MathTools.Random(chrs.Length)]);
			}
			return buff.ToString();
		}

		public static string[] MakeRandLines(string chrs, int chrcntMin, int chrcntMax, int linecnt)
		{
			string[] dest = new string[linecnt];

			for (int index = 0; index < linecnt; index++)
			{
				dest[index] = MakeRandString(chrs, MathTools.Random(chrcntMin, chrcntMax));
			}
			return dest;
		}

		public static byte[] MakeRandBytes(int size)
		{
			byte[] buff = new byte[size];

			for (int index = 0; index < size; index++)
			{
				buff[index] = (byte)MathTools.Random(256);
			}
			return buff;
		}
	}
}
