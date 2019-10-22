using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class AxonLayer
	{
		public double[][] WeightTable;

		public int PrevCount
		{
			get
			{
				return this.WeightTable.Length - 1;
			}
		}

		public int NextCount
		{
			get
			{
				return this.WeightTable[0].Length;
			}
		}

		public AxonLayer(int prevCount, int nextCount)
		{
			prevCount++; // Bias

			this.WeightTable = new double[prevCount][];

			for (int index = 0; index < prevCount; index++)
				this.WeightTable[index] = new double[nextCount];
		}
	}
}
