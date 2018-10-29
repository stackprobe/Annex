using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class MultiLayer
	{
		public Layer InputLayer;
		public List<Layer> Layers = new List<Layer>();
		public Layer OutputLayer;

		public MultiLayer(int inputCount, int outputCount, int[] counts)
		{
			this.InputLayer = new Layer(this, inputCount, () => new InputNeuron());

			for (int index = 0; index < counts.Length; index++)
			{
				this.Layers.Add(new Layer(this, counts[index], () => new HiddenNeuron()));
			}
			this.OutputLayer = new Layer(this, outputCount, () => new OutputNeuron());

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
	}
}
