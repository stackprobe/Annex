using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class BiasNeuron : Neuron
	{
		public override void Fire()
		{
			this.Input = 0.0;
			this.Output = 1.0;
		}

		public override double GetDifferentialCoefficient()
		{
			throw null; // never
		}
	}
}
