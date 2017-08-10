using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCluster
{
	public class SyncString
	{
		private object SYNCROOT = new object();
		private string Value;

		public SyncString(string value)
		{
			Value = value;
		}

		public void SetString(string value)
		{
			lock (SYNCROOT)
			{
				Value = value;
			}
		}

		public string GetString()
		{
			lock (SYNCROOT)
			{
				return Value;
			}
		}
	}
}
