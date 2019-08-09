using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Charlotte.Tools
{
	public static class SecurityTools
	{
		private static RandomNumberGenerator cRandom = RNGCryptoServiceProvider.Create();

		public static byte[] getCRand(int size)
		{
			byte[] dest = new byte[size];
			cRandom.GetBytes(dest);
			return dest;
		}

		public static byte[] getSHA512(byte[] src)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return sha512.ComputeHash(src);
			}
		}

		public static byte[] getSHA512File(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return sha512.ComputeHash(fs);
			}
		}
	}
}
