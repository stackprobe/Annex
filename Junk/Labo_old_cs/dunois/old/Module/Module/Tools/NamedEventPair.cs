using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class NamedEventPair
	{
		private object SYNCROOT_forSet = new object();
		private object SYNCROOT_forWait = new object();
		private NamedEventData _forSet;
		private NamedEventData _forWait;

		public NamedEventPair()
			: this(SecurityTools.GetCRandIdent())
		{ }

		public NamedEventPair(string name)
		{
			_forSet = new NamedEventData(name);
			_forWait = new NamedEventData(name);
		}

		/// <summary>
		/// thread safe
		/// </summary>
		public void Set()
		{
			lock (SYNCROOT_forSet)
			{
				_forSet.Set();
			}
		}

		/// <summary>
		/// thread safe
		/// </summary>
		public void WaitForever()
		{
			lock (SYNCROOT_forWait)
			{
				_forWait.WaitForever();
			}
		}

		/// <summary>
		/// thread safe
		/// </summary>
		/// <param name="millis"></param>
		public void WaitForMillis(int millis)
		{
			lock (SYNCROOT_forWait)
			{
				_forWait.WaitForMillis(millis);
			}
		}

		public void Dispose()
		{
			if (_forSet != null)
			{
				_forSet.Dispose();
				_forSet = null;
				_forWait.Dispose();
				_forWait = null;
			}
		}
	}
}
