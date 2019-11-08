using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.BranchMap01
{
	public class Test0001
	{
		public Map Map;

		// <---- prms

		public void Test01()
		{
			BranchMap bm = new BranchMap() { Map = Map };

			bm.Load();
			bm.Output();
		}
	}
}
