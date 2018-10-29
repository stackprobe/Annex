using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class InputNeuron : Neuron
	{
		public override void Fire()
		{
			this.Output = this.Input.Value;
		}
	}
}
