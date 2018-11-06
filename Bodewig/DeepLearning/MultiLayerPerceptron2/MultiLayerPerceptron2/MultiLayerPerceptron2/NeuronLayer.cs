using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron2
{
	public class NeuronLayer
	{
		public double[] Inputs;
		public double[] Outputs;

		public int Count
		{
			get
			{
				return this.Inputs.Length;
			}
		}

		public NeuronLayer(int count)
		{
			this.Inputs = new double[count];
			this.Outputs = new double[count];
		}
	}
}
