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
			this.Output = value; // 最後は恒等関数(P.107)
			//this.Output = ActivationFunction.GetOutput(value);
		}

		public override double GetDifferentialCoefficient()
		{
			return 1.0; // 最後は恒等関数(P.107)
			//return ActivationFunction.GetDifferentialCoefficient(this.Input.Value);
		}
	}
}
