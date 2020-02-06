using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class PictureListTest
	{
		public void Test01()
		{
			PictureList pl = new PictureList();

			pl.Init();
			pl.Perform();
		}
	}
}
