using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCluster
{
	public class SyncFlag
	{
		private object SYNCROOT = new object();
		private bool Flag;

		public void SetFlag(bool flag)
		{
			lock (SYNCROOT)
			{
				Flag = flag;
			}
		}

		public bool GetFlag()
		{
			lock (SYNCROOT)
			{
				return Flag;
			}
		}
	}
}
