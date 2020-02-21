using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Pulser
	{
		private Action<long> Pulse;

		public Pulser(Action<long> pulse)
		{
			this.Pulse = pulse;
		}

		private long Count = 0;
		private int Remaining = 0;
		private int RemainingStart = 0;
		private long LastTSec = 0;

		public void Invoke()
		{
			this.Count++;

			if (--this.Remaining < 0)
			{
				this.Pulse(this.Count);

				long currTSec = DateTimeToSec.Now.GetSec();

				if (this.LastTSec < currTSec)
				{
					if (1 < this.RemainingStart)
						this.RemainingStart--;
				}
				else
				{
					if (this.RemainingStart < 10)
						this.RemainingStart++;
					else
						this.RemainingStart += this.RemainingStart / 10;
				}
				this.Remaining = this.RemainingStart;
				this.LastTSec = currTSec;
			}
		}
	}
}
