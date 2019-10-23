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
		public NeuronLayer[] NeuronLayers;
		public AxonLayer[] AxonLayers;

		public MultiLayer(int inputCount, int outputCount, int[] hiddenCounts)
		{
			if (inputCount < 1 || IntTools.IMAX < inputCount)
				throw new ArgumentException();

			if (outputCount < 1 || IntTools.IMAX < outputCount)
				throw new ArgumentException();

			if (hiddenCounts.Length < 1 || IntTools.IMAX < hiddenCounts.Length)
				throw new ArgumentException();

			foreach (int hiddenCount in hiddenCounts)
				if (hiddenCount < 1 || IntTools.IMAX < hiddenCount)
					throw new ArgumentException();

			this.NeuronLayers = new NeuronLayer[hiddenCounts.Length + 2];
			this.AxonLayers = new AxonLayer[hiddenCounts.Length + 1];

			this.NeuronLayers[0] = new NeuronLayer(inputCount);

			for (int index = 0; index < hiddenCounts.Length; index++)
				this.NeuronLayers[index + 1] = new NeuronLayer(hiddenCounts[index]);

			this.NeuronLayers[this.NeuronLayers.Length - 1] = new NeuronLayer(outputCount);

			for (int index = 0; index < this.AxonLayers.Length; index++)
				this.AxonLayers[index] = new AxonLayer(this.NeuronLayers[index].Count, this.NeuronLayers[index + 1].Count);
		}
	}
}
