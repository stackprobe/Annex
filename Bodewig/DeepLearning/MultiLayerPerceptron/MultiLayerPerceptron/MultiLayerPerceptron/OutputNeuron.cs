using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class OutputNeuron : Neuron
	{
		public override void Fire()
		{
			double value = 0.0;

			foreach (Axon prev in this.Prevs)
			{
				value += prev.GetOutput();
			}
			this.Input = value;
			this.Output = value;
		}

		public override double GetDifferentialCoefficient()
		{
			return 1.0;
		}
	}
}
