using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MSection : IDisposable
	{
		private Mutex Mtx;
		private bool Binding;

		public MSection(string ident)
			: this(new Mutex(false, ident), true)
		{ }

		public MSection(Mutex mtx, bool binding = false)
		{
			this.Mtx = mtx;
			this.Mtx.WaitOne();
			this.Binding = binding;
		}

		public void Dispose()
		{
			if (this.Mtx != null)
			{
				this.Mtx.ReleaseMutex();

				if (this.Binding)
					this.Mtx.Dispose();

				this.Mtx = null;
			}
		}
	}
}
