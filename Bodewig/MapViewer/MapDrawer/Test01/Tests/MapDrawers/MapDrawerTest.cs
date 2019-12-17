using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MapDrawers;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Tests.MapDrawers
{
	public class MapDrawerTest
	{
		public void Test01()
		{
			MapDrawer md = new MapDrawer();

			Test01_B(md.Draw(35.0, 135.0, 10.0));
			Test01_B(md.Draw(35.0, 135.0, 30.0));
			Test01_B(md.Draw(35.0, 135.0, 100.0));
			Test01_B(md.Draw(35.0, 135.0, 300.0));
		}

		private int ImageFileCount = 0;

		private void Test01_B(Image img)
		{
			new Canvas2(img).Save(string.Format(@"C:\temp\{0}.png", ImageFileCount++));
		}
	}
}
