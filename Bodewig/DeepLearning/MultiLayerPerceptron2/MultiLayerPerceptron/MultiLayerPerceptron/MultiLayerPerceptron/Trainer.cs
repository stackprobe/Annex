using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// トレーナー
	/// </summary>
	public class Trainer
	{
		/// <summary>
		/// 評価用
		/// </summary>
		private MultiLayer ML;

		/// <summary>
		/// 学習用
		/// </summary>
		private MultiLayer BP;

		public Trainer(int[] nuronCounts)
		{
			if (nuronCounts.Length < 3 || IntTools.IMAX < nuronCounts.Length)
				throw new ArgumentException();

			foreach (int nuronCount in nuronCounts)
				if (nuronCount < 1 || IntTools.IMAX < nuronCount)
					throw new ArgumentException();

			this.ML = new MultiLayer(nuronCounts);
			this.BP = new MultiLayer(nuronCounts);

			this.ML.Randomize();
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

		private void SetInputs(double[] values)
		{
			if (values.Length != this.ML.Layers[0].Neurons.Length)
				throw new ArgumentException();

			if (values.Any(value => value < 0.0 || 1.0 < value))
				throw new ArgumentException();

			for (int index = 0; index < values.Length; index++)
				this.ML.Layers[0].Neurons[index].OutputValue = values[index]; // 入力は活性化関数を通さないので OutputValue に直接セットする。
		}

		private void GetOutputs(double[] values)
		{
			if (values.Length != this.ML.Layers[this.ML.Layers.Length - 1].Neurons.Length)
				throw new ArgumentException();

			for (int index = 0; index < values.Length; index++)
				values[index] = this.ML.Layers[this.ML.Layers.Length - 1].Neurons[index].OutputValue;
		}

		private void Activate()
		{
			for (int index = 1; index < this.ML.Layers.Length; index++)
			{
				Layer cl = this.ML.Layers[index - 1];
				Layer nl = this.ML.Layers[index];

				for (int n = 0; n < nl.Neurons.Length; n++)
				{
					nl.Neurons[n].InputValue = cl.Axons[cl.Neurons.Length, n].Weight; // バイアスからの入力

					for (int c = 0; c < cl.Neurons.Length; c++)
						nl.Neurons[n].InputValue += cl.Neurons[c].OutputValue * cl.Axons[c, n].Weight;

					nl.Neurons[n].OutputValue = ActivationFunction.GetOutput(nl.Neurons[n].InputValue);
				}
			}
		}

		private void BackPropagation(double[] expectedOutputs)
		{
			if (expectedOutputs.Length != this.ML.Layers[this.ML.Layers.Length - 1].Neurons.Length)
				throw new ArgumentException();

			if (expectedOutputs.Any(value => value < 0.0 || 1.0 < value))
				throw new ArgumentException();

			throw null; // TODO
		}
	}
}
