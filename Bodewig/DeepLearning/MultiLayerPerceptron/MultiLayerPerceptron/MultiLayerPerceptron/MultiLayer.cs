using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	public class MultiLayer
	{
		public Layer InputLayer;
		public List<Layer> Layers = new List<Layer>();
		public Layer OutputLayer;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputCount">1以上</param>
		/// <param name="outputCount">1以上</param>
		/// <param name="counts">1つ以上、各要素は1以上</param>
		public MultiLayer(int inputCount, int outputCount, int[] counts)
		{
			this.InputLayer = new Layer(this, -1, inputCount, () => new InputNeuron());

			for (int index = 0; index < counts.Length; index++)
			{
				this.Layers.Add(new Layer(this, index, counts[index], () => new HiddenNeuron()));
			}
			this.OutputLayer = new Layer(this, counts.Length, outputCount, () => new OutputNeuron());

			// ----

			this.InputLayer.Connect(this.Layers[0]);

			for (int index = 1; index < counts.Length; index++)
			{
				this.Layers[index - 1].Connect(this.Layers[index]);
			}
			this.Layers[counts.Length - 1].Connect(this.OutputLayer);

			// ----

			for (int index = 0; index < counts.Length; index++)
			{
				this.Layers[index].RandomizePrevs();
			}
			this.OutputLayer.RandomizePrevs();
		}

		public void Clear()
		{
			this.InputLayer.Clear();

			foreach (Layer layer in this.Layers)
			{
				layer.Clear();
			}
			this.OutputLayer.Clear();
		}

		public void SetInputs(double[] inputs)
		{
			for (int index = 0; index < inputs.Length; index++)
			{
				this.InputLayer.Neurons[index].Input = inputs[index];
			}
		}

		public void Fire()
		{
			this.InputLayer.Fire();

			foreach (Layer layer in this.Layers)
			{
				layer.Fire();
			}
			this.OutputLayer.Fire();
		}

		public double[] GetOutputs()
		{
			double[] outputs = new double[this.OutputLayer.Neurons.Count];

			for (int index = 0; index < this.OutputLayer.Neurons.Count; index++)
			{
				outputs[index] = this.OutputLayer.Neurons[index].Output.Value;
			}
			return outputs;
		}

		public IEnumerable<string> ToStrings()
		{
			foreach (Layer[] layers in new Layer[][]
			{
				new Layer[] { this.InputLayer},
				this.Layers.ToArray(),
				new Layer[] { this.OutputLayer},
			})
			{
				foreach (Layer layer in layers)
				{
					yield return "Layer {";

					foreach (string line in layer.ToStrings())
						yield return line;

					yield return "}";
				}
			}
		}

		public void Learn(double[] inputs, double[] correctOutputs)
		{
			this.Clear();
			this.SetInputs(inputs);
			this.Fire();

			double[] outputs = this.GetOutputs();

			foreach (Layer[] layers in new Layer[][]
			{
				this.Layers.ToArray(),
				new Layer[] { this.OutputLayer},
			})
			{
				foreach (Layer layer in layers)
				{
					foreach (Neuron n in layer.Neurons)
					{
						foreach (Axon a in n.Prevs)
						{
							double w = a.Weight;

							for (int index = 0; index < correctOutputs.Length; index++)
							{
								double d = outputs[index] - correctOutputs[index];
								d *= d;
								d *= a.GetDifferentialCoefficient_Weight_Output(this.OutputLayer.Neurons[index]);
								d *= 0.000001; // 学習係数

								if (double.IsNaN(d))
									throw null;

								w -= d;
							}
							if (w < -9.0 || 9.0 < w) // zantei ?????????
							{
								Console.WriteLine("!!!!");
								w = SecurityTools.CRandom.GetReal() * 0.2 - 0.1;
							}
							a.WeightNew = w;
						}
					}
				}
			}

			foreach (Layer[] layers in new Layer[][]
			{
				this.Layers.ToArray(),
				new Layer[] { this.OutputLayer},
			})
			{
				foreach (Layer layer in layers)
					foreach (Neuron n in layer.Neurons)
						foreach (Axon a in n.Prevs)
							a.Weight = a.WeightNew;
			}
		}
	}
}
