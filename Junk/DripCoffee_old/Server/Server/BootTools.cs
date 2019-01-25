﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Server
{
	// sync > @ BootTools

	public class BootTools
	{
		public static string SelfFile;
		public static string SelfDir;

		public static void OnBoot()
		{
			SelfFile = System.Reflection.Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);

			Directory.SetCurrentDirectory(SelfDir);
		}
	}

	// < sync
}
