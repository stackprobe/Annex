using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 教師
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

			for (int index = 0; index < values.Length; index++)
				this.ML.NeuronLayers[0].Inputs[index] = values[index];
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
			throw null; // TODO
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
		/// 評価する。
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
		/// 教育する。
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
