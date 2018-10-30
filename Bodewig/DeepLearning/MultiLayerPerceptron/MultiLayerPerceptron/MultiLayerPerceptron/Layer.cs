using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	public class Layer
	{
		public MultiLayer Owner;
		public int SelfIndex;
		public List<Neuron> Neurons = new List<Neuron>();

		public Layer(MultiLayer owner, int selfIndex, int count, Func<Neuron> createNeuron)
		{
			this.Owner = owner;
			this.SelfIndex = selfIndex;

			for (int index = 0; index < count; index++)
			{
				this.Neurons.Add(createNeuron());
			}
		}

		public void Connect(Layer next)
		{
			this.Neurons.Add(new BiasNeuron());

			foreach (Neuron n in next.Neurons)
			{
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

		public void RandomizePrevs()
		{
			foreach (Neuron n in this.Neurons)
			{
				foreach (Axon prev in n.Prevs)
				{
					prev.Weight = SecurityTools.CRandom.GetReal() * 0.2 - 0.1; // -0.1 ～ 0.1
					//prev.Weight = SecurityTools.CRandom.GetUInt() * 2.0 / uint.MaxValue - 1.0; // -1.0 ～ 1.0
				}
			}
		}

		public void Clear()
		{
			foreach (Neuron n in this.Neurons)
			{
				n.Clear();
			}
		}

		public void Fire()
		{
			foreach (Neuron n in this.Neurons)
			{
				n.Fire();
			}
		}

		public IEnumerable<string> ToStrings()
		{
			foreach (Neuron n in this.Neurons)
			{
				yield return n.ToString();
			}
		}
	}
}
