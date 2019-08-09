using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
#if true
		public const int V_倍 = 2;
		public const int PICTURE_W = V_倍 * 1200;
		public const int PICTURE_H = V_倍 * 676;
		//public const int PICTURE_H = V_倍 * 675; // 奇数 ng
#else
		public const int V_倍 = 3;
		public const int PICTURE_W = V_倍 * 800;
		public const int PICTURE_H = V_倍 * 450;
#endif

		public const int FPS = 20;
	}
}
