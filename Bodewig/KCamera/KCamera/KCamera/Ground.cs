using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public NamedEventUnit EvStop = new NamedEventUnit(Consts.EV_STOP);

		public string CameraNamePtn = "Venus";
		public string DestDir = @"C:\appdata\KCamera";
	}
}
