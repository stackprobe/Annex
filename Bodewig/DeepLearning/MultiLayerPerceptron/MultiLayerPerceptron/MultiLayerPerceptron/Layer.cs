using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class Layer
	{
		public MultiLayer Owner;
		public List<Neuron> Neurons = new List<Neuron>();

		public Layer(MultiLayer owner, int count, Func<Neuron> createNeuron)
		{
			this.Owner = owner;

			for (int index = 0; index < count; index++)
			{
				this.Neurons.Add(createNeuron());
			}
		}

		public void Connect(Layer next)
		{
			foreach (Neuron n in next.Neurons)
			{
				{
					Axon axon = new Axon();

					axon.Prev = BiasNeuron.I;
					axon.Next = n;

					n.Prevs.Add(axon);
				}

				foreach (Neuron c in this.Neurons)
				{
					Axon axon = new Axon();

					axon.Prev = c;
					axon.Next = n;

					c.Nexts.Add(axon);
					n.Prevs.Add(axon);
				}
			}
		}
	}
}
