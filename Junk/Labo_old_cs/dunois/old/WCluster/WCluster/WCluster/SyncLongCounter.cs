using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCluster
{
	public class SyncLongCounter
	{
		private object SYNCROOT = new object();
		private long Count;

		public void Increment()
		{
			lock (SYNCROOT)
			{
				Count++;
			}
		}

		public long GetCount()
		{
			lock (SYNCROOT)
			{
				return Count;
			}
		}
	}
}
