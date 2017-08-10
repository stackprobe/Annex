using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Charlotte.Tools
{
	public class SecurityTools
	{
		public static string GetSHA512_128String(byte[] block)
		{
			return GetSHA512String(block).Substring(0, 32);
		}

		public static string GetSHA512String(byte[] block)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return StringTools.ToHex(sha512.ComputeHash(block));
			}
		}

		public static string GetSHA512_128StringByFile(string file)
		{
			return GetSHA512StringByFile(file).Substring(0, 32);
		}

		public static string GetSHA512StringByFile(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return StringTools.ToHex(sha512.ComputeHash(fs));
			}
		}

		private static RandomNumberGenerator Rand = RNGCryptoServiceProvider.Create();

		public static byte[] GetCRandBytes(int size)
		{
			byte[] dest = new byte[size];
			Rand.GetBytes(dest);
			return dest;
		}

		public static UInt64 GetCRand64()
		{
			byte[] b = GetCRandBytes(8);

			UInt64 ret =
				((UInt64)b[0] << 56) |
				((UInt64)b[0] << 48) |
				((UInt64)b[0] << 40) |
				((UInt64)b[0] << 32) |
				((UInt64)b[0] << 24) |
				((UInt64)b[0] << 16) |
				((UInt64)b[0] << 8) |
				((UInt64)b[0] << 0);

			return ret;
		}

		public static uint GetCRand()
		{
			byte[] b = GetCRandBytes(8);

			uint ret =
				((uint)b[0] << 24) |
				((uint)b[0] << 16) |
				((uint)b[0] << 8) |
				((uint)b[0] << 0);

			return ret;
		}

		public static uint GetCRand(uint modulo)
		{
			return (uint)(GetCRand64() % (UInt64)modulo); // XXX
		}

		public static char GetCRandChar(string chrs)
		{
			return chrs[(int)GetCRand((uint)chrs.Length)];
		}

		public static string GetCRandString(string chrs, int len)
		{
			StringBuilder buff = new StringBuilder();

			for (int count = 0; count < len; count++)
			{
				buff.Append(GetCRandChar(chrs));
			}
			return buff.ToString();
		}

		public static string GetCRandIdent()
		{
			return GetCRandString(StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha, 22); // mkpw -> 62 P 22 L 2 = 130.*
		}

		public static string GetCRandUpperIdent()
		{
			return GetCRandString(StringTools.DIGIT + StringTools.ALPHA, 25); // mkpu -> 36 P 25 L 2 = 129.*
		}

		public static string GetCRandLowerIdent()
		{
			return GetCRandUpperIdent().ToLower();
		}

		public static string GetCRandDigitIdent()
		{
			return GetCRandString(StringTools.DIGIT, 40); // mkpd -> 10 P 40 L 2 = 132.*
		}
	}
}
