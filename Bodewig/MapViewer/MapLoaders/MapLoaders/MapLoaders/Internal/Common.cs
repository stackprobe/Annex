using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.MapLoaders.Internal
{
	public static class Common
	{
		private static byte[] Buff_8 = new byte[8];

		public static int ReadInt(FileStream reader)
		{
			reader.Read(Buff_8, 0, 4);
			return BitConverter.ToInt32(Buff_8, 0);
		}

		public static void WriteInt(FileStream writer, int value)
		{
			writer.Write(BitConverter.GetBytes(value), 0, 4);
		}

		public static string ReadString(FileStream reader)
		{
			reader.Read(Buff_8, 0, 4);
			int size = BitConverter.ToInt32(Buff_8, 0);
			byte[] buff = new byte[size];
			reader.Read(buff, 0, size);
			return Encoding.UTF8.GetString(buff);
		}

		public static void WriteString(FileStream writer, string str)
		{
			byte[] buff = Encoding.UTF8.GetBytes(str);
			writer.Write(BitConverter.GetBytes(buff.Length), 0, 4);
			writer.Write(buff, 0, buff.Length);
		}

		public static double ReadDouble(FileStream reader)
		{
			reader.Read(Buff_8, 0, 8);
			return BitConverter.ToDouble(Buff_8, 0);
		}

		public static void WriteDouble(FileStream writer, double value)
		{
			writer.Write(BitConverter.GetBytes(value), 0, 8);
		}
	}
}
