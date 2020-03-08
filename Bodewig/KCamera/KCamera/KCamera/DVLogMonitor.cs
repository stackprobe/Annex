using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class DVLogMonitor
	{
		public DVLogMonitorPrm Prm;

		// <---- prm

		private List<double> ValueMaxHistory = new List<double>();
		private double? ValueMax_Max = null;

		public void AddValueMax(double valueMax)
		{
			if (this.Prm.MonitorCount <= this.ValueMaxHistory.Count)
				this.ValueMaxHistory.RemoveAt(0);

			this.ValueMaxHistory.Add(valueMax);
			this.ValueMax_Max = Math.Max(this.Prm.ValueMax_Max_Min, ArrayTools.Heaviest(this.ValueMaxHistory, v => v));

			Ground.SLog.WriteLog(string.Format("ValueMax_Max: {0:F20}", this.ValueMax_Max.Value));
		}

		public bool LastDifferent = false;

		public void CheckDifferent(double value)
		{
			if (this.ValueMax_Max == null)
				return;

			this.LastDifferent = this.ValueMax_Max.Value * this.Prm.DiffMagnifBorder <= value;

#if false // test
			if (this.LastDifferent)
			{
				Ground.SLog.WriteLog(string.Format("CheckDifferent_LastDifferent_True: {0:F20} * {1} == {2:F20} <= {3:F20}",
					this.ValueMax_Max.Value,
					this.Prm.DiffMagnifBorder,
					this.ValueMax_Max.Value * this.Prm.DiffMagnifBorder,
					value
					));
			}
#endif
		}
	}
}
