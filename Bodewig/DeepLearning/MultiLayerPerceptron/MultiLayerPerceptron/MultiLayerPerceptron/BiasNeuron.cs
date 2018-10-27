using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class BiasNeuron : Neuron
	{
		public static BiasNeuron I = new BiasNeuron();

		public override double GetOutput()
		{
			return 1.0;
		}
	}
}
