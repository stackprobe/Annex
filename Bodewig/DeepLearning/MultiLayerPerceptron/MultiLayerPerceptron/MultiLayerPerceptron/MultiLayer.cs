using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class MultiLayer
	{
		public Layer InputLayer;
		public Layer OutputLayer;
		public List<Layer> Layers = new List<Layer>();

		public MultiLayer(int inputCount, int outputCount, int[] counts)
		{
			this.InputLayer = new Layer(this, inputCount, () => new InputNeuron());
			this.OutputLayer = new Layer(this, outputCount, () => new OutputNeuron());

			for (int index = 0; index < counts.Length; index++)
			{
				this.Layers.Add(new Layer(this, counts[index], () => new HiddenNeuron()));
			}
			this.InputLayer.Connect(this.Layers[0]);

			for (int index = 1; index < counts.Length; index++)
			{
				this.Layers[index - 1].Connect(this.Layers[index]);
			}
			this.Layers[counts.Length - 1].Connect(this.OutputLayer);
		}

		public void SetInputValues(double[] values)
		{
			for (int index = 0; index < values.Length; index++)
			{
				((InputNeuron)this.Layers[0].Neurons[index]).Value = values[index];
			}
		}

		public double[] GetOutputs()
		{
			double[] outputs = new double[this.OutputLayer.Neurons.Count];

			for (int index = 0; index < this.OutputLayer.Neurons.Count; index++)
			{
				outputs[index] = this.OutputLayer.Neurons[index].GetOutput();
			}
			return outputs;
		}
	}
}
