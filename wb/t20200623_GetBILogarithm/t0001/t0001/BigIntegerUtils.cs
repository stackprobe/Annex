using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Charlotte
{
	public static class BigIntegerUtils
	{
		// sync > @ GetByteArrayLength_BigInteger

		public static int GetByteArrayLength(BigInteger v)
		{
			byte[] bytes = v.ToByteArray();
			int index;

			for (index = bytes.Length - 1; 0 <= index; index--)
				if (bytes[index] != 0)
					break;

			return index + 1;
		}

		// < sync
	}
}
