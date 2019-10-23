using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// トレーナー
	/// </summary>
	public class Trainer
	{
		private MultiLayer ML;

		public Trainer(MultiLayer ml)
		{
			this.ML = ml;
		}

		private void SetInputs(double[] values)
		{
			if (values.Length != this.ML.NeuronLayers[0].Count)
				throw new ArgumentException();

			if (values.Any(value => value < -1.0 || 1.0 < value))
				throw new ArgumentException();

			for (int index = 0; index < values.Length; index++)
				this.ML.NeuronLayers[0].Outputs[index] = values[index]; // 入力は活性化関数を通さないので Outputs に直接セットする。
		}

		private void GetOutputs(double[] values)
		{
			if (values.Length != this.ML.NeuronLayers[this.ML.NeuronLayers.Length - 1].Count)
				throw new ArgumentException();

			for (int index = 0; index < values.Length; index++)
				values[index] = this.ML.NeuronLayers[this.ML.NeuronLayers.Length - 1].Outputs[index];
		}

		private void Activate()
		{
			for (int index = 0; index < this.ML.AxonLayers.Length; index++)
			{
				NeuronLayer inputLayer = this.ML.NeuronLayers[index];
				AxonLayer axonLayer = this.ML.AxonLayers[index];
				NeuronLayer outputLayer = this.ML.NeuronLayers[index + 1];

				for (int n = 0; n < outputLayer.Count; n++)
				{
					outputLayer.Inputs[n] = axonLayer.WeightTable[inputLayer.Count, n]; // バイアスからの入力

					for (int p = 0; p < inputLayer.Count; p++)
						outputLayer.Inputs[n] += inputLayer.Outputs[p] * axonLayer.WeightTable[p, n];

					outputLayer.Outputs[n] = ActivationFunction.GetOutput(outputLayer.Inputs[n]);
				}
			}
		}

		private void BackPropagation(double[] expectedOutputs)
		{
			if (expectedOutputs.Length != this.ML.NeuronLayers[this.ML.NeuronLayers.Length - 1].Count)
				throw new ArgumentException();

			if (expectedOutputs.Any(value => value < -1.0 || 1.0 < value))
				throw new ArgumentException();

			throw null; // TODO
		}

		/// <summary>
		/// 評価させる。
		/// </summary>
		/// <param name="inputs">入力値</param>
		/// <param name="outputs">出力値の出力先</param>
		public void Fire(double[] inputs, double[] outputs)
		{
			this.SetInputs(inputs);
			this.Activate();
			this.GetOutputs(outputs);
		}

		/// <summary>
		/// 学習させる。
		/// </summary>
		/// <param name="inputs">入力値</param>
		/// <param name="expectedOutputs">期待される出力値</param>
		public void Train(double[] inputs, double[] expectedOutputs)
		{
			this.SetInputs(inputs);
			this.Activate();
			this.BackPropagation(expectedOutputs);
		}
	}
}
