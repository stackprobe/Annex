using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	public class Axon
	{
		public Neuron Prev;
		public Neuron Next;

		// <---- prm

		public double Weight = 0.1;

		// <---- prop

		public double GetOutput()
		{
			return this.Prev.Output.Value * this.Weight;
		}

		public double DifferentialCoefficient = double.NaN;

		public double GetDifferentialCoefficient_Weight_Output(Neuron output)
		{
			this.Next.DifferentialCoefficient_Input = this.Prev.Output.Value;

			Neuron[] neurons = new Neuron[] { this.Next };

			for (; ; )
			{
				foreach (Neuron n in neurons)
				{
					n.DifferentialCoefficient_Output =
						n.DifferentialCoefficient_Input *
						n.GetDifferentialCoefficient();

					foreach (Axon a in n.Nexts)
						a.DifferentialCoefficient =
							n.DifferentialCoefficient_Output *
							a.Weight;
				}
				Neuron[] nextNeurons = neurons[0].Nexts.Select(a => a.Next).ToArray();

				if (nextNeurons.Length == 0)
					break;

				foreach (Neuron nn in nextNeurons)
				{
					double input = 0.0;

					foreach (Axon a in nn.Prevs)
						if (ArrayTools.Contains(neurons, n => n == a.Prev))
							input += a.DifferentialCoefficient;

					nn.DifferentialCoefficient_Input = input;
				}
				neurons = nextNeurons;
			}
			return output.DifferentialCoefficient_Output;
		}

		public double WeightNew = double.NaN;
	}
}
