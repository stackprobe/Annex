using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class InputNeuron : Neuron
	{
		public double Value;

		public override double GetOutput()
		{
			return this.Value;
		}
	}
}
