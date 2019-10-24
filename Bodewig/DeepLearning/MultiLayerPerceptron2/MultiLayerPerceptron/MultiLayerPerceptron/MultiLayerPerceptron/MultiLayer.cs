using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 入力層・隠れ層・出力層 と 各層の間にある軸索層 の集まり
	/// </summary>
	public class MultiLayer
	{
		public Layer[] Layers;

		public MultiLayer(int[] neuronCounts)
		{
			this.Layers = new Layer[neuronCounts.Length];

			for (int index = 0; index + 1 < this.Layers.Length; index++)
			{
				this.Layers[index] = new Layer()
				{
					Neurons = new Neuron[neuronCounts[index]],
					Axons = new Axon[neuronCounts[index] + 1, neuronCounts[index + 1]],
				};

				for (int c = 0; c < neuronCounts[index]; c++)
					this.Layers[index].Neurons[c] = new Neuron();

				for (int c = 0; c < neuronCounts[index] + 1; c++)
					for (int n = 0; n < neuronCounts[index + 1]; n++)
						this.Layers[index].Axons[c, n] = new Axon();
			}

			{
				int index = neuronCounts.Length - 1;

				this.Layers[index] = new Layer()
				{
					Neurons = new Neuron[neuronCounts[index]],
					Axons = null,
				};

				for (int c = 0; c < neuronCounts[index]; c++)
					this.Layers[index].Neurons[c] = new Neuron();
			}
		}

		public void Randomize()
		{
			for (int index = 1; index < this.Layers.Length; index++)
			{
				Layer cl = this.Layers[index - 1];
				Layer nl = this.Layers[index];

				for (int n = 0; n < nl.Neurons.Length; n++)
				{
					double wt = 0.0;

					for (int c = 0; c < cl.Neurons.Length; c++)
					{
						wt += cl.Axons[c, n].Weight = SecurityTools.CRandom.GetReal() * 2.0 - 1.0;
					}
					cl.Axons[cl.Neurons.Length, n].Weight = -wt;
				}
			}
		}
	}
}
