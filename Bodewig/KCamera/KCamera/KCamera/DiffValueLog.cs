using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class DiffValueLog
	{
		public string LogFile;
		public DVLogMonitor DVLogMonitor = null; // null == 無効

		// <---- prm

		private long[] CountList = new long[100];

		public void Add(double diffValue)
		{
			this.CountList[IntTools.Range((int)(diffValue * 1000000.0), 0, this.CountList.Length - 1)]++;

			this.AddToLog(diffValue);
		}

		public void WriteToFile(string file)
		{
			File.WriteAllLines(file, this.GetDistributionReport());
		}

		private string[] GetDistributionReport()
		{
			List<string> lines = new List<string>();

			for (int index = 0; index < this.CountList.Length; index++)
			{
				lines.Add("0.0000" + index.ToString("D2") + " ====> " + this.CountList[index]);
			}
			return lines.ToArray();
		}

		private DateTime LastLogWroteTime = DateTime.Now;
		private double DiffValueMin = double.MaxValue;
		private double DiffValueMax = 0.0;
		private double DiffValueAvgNumer = 0.0;
		private long DiffValueAvgDenom = 0L;

		private void AddToLog(double diffValue)
		{
			this.DiffValueMin = Math.Min(this.DiffValueMin, diffValue);
			this.DiffValueMax = Math.Max(this.DiffValueMax, diffValue);
			this.DiffValueAvgNumer += diffValue;
			this.DiffValueAvgDenom++;

			if (this.DVLogMonitor != null)
				this.DVLogMonitor.CheckDifferent(diffValue);

			DateTime currTime = DateTime.Now;

			if (60 <= (currTime - this.LastLogWroteTime).TotalSeconds)
			{
				if (this.DVLogMonitor != null)
					this.DVLogMonitor.AddValueMax(this.DiffValueMax);

				using (StreamWriter writer = new StreamWriter(this.LogFile, true, Encoding.ASCII))
				{
					writer.WriteLine(string.Format("[{0} To {1}] Min={2:F9} Max={3:F9} Avg={4:F9} ({5})",
						this.LastLogWroteTime,
						currTime,
						this.DiffValueMin,
						this.DiffValueMax,
						this.DiffValueAvgNumer / this.DiffValueAvgDenom,
						this.DiffValueAvgDenom
						));
				}

				this.LastLogWroteTime = currTime;

				this.DiffValueMin = double.MaxValue;
				this.DiffValueMax = 0.0;
				this.DiffValueAvgNumer = 0.0;
				this.DiffValueAvgDenom = 0L;
			}
		}

		public bool DVLM_GetLastDifferent()
		{
			return this.DVLogMonitor == null ? false : this.DVLogMonitor.LastDifferent;
		}
	}
}
