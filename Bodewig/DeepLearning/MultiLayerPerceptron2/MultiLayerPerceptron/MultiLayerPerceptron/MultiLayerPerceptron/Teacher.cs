using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 教師
	/// </summary>
	public class Teacher
	{
		/// <summary>
		/// 評価用
		/// </summary>
		private MultiLayer ML;

		/// <summary>
		/// 学習用
		/// </summary>
		private MultiLayer BP;

		public Teacher(int[] neuronCounts)
		{
			if (neuronCounts.Length < 3 || IntTools.IMAX < neuronCounts.Length)
				throw new ArgumentException();

			foreach (int neuronCount in neuronCounts)
				if (neuronCount < 1 || IntTools.IMAX < neuronCount)
					throw new ArgumentException();

			this.ML = new MultiLayer(neuronCounts);
			this.BP = new MultiLayer(neuronCounts);

			this.ML.Randomize();
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
		/// 学習させる。
		/// </summary>
		/// <param name="inputs">入力値</param>
		/// <param name="expectedOutputs">期待される出力値</param>
		public void Train(double[] inputs, double[] expectedOutputs)
		{
			this.SetInputs(inputs);
			this.Activate();
			this.Teach(expectedOutputs);
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
				values[index] = this.ML.Layers[this.ML.Layers.Length - 1].Neurons[index].InputValue; // 最後は恒等関数なので InputValue から取得する。
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

					nl.Neurons[n].OutputValue = ActivationFunction.GetOutput(nl.Neurons[n].InputValue); // 最後は恒等関数なので出力層の OutputValue は不要！
				}
			}
		}

		private void Teach(double[] expectedOutputs)
		{
			if (expectedOutputs.Length != this.ML.Layers[this.ML.Layers.Length - 1].Neurons.Length)
				throw new ArgumentException();

			if (expectedOutputs.Any(value => value < 0.0 || 1.0 < value))
				throw new ArgumentException();

			for (int index = 0; index < this.ML.Layers[this.ML.Layers.Length - 1].Neurons.Length; index++)
			{
				this.MakeRateOfChangeTable(this.BP, this.ML.Layers[this.ML.Layers.Length - 1].Neurons[index], index);
				this.ChangeWeight_Output(this.BP, index, expectedOutputs[index]);
			}
		}

		private void MakeRateOfChangeTable(MultiLayer bp, Neuron output, int outputIndex)
		{
			int li = bp.Layers.Length - 2;

			//bp.Layers[li + 1].Neurons[outputIndex].InputValue = 1.0;

			for (int c = 0; c < bp.Layers[li].Neurons.Length; c++)
			{
				double t = this.ML.Layers[li].Axons[c, outputIndex].Weight;
				double x = this.ML.Layers[li].Neurons[outputIndex].InputValue;
				double r = ActivationFunction.GetRateOfChange(x);

				bp.Layers[li].Neurons[c].OutputValue = t;

				t *= r;

				bp.Layers[li].Neurons[c].InputValue = t;
			}
			while (0 <= --li)
			{
				for (int c = 0; c < bp.Layers[li].Neurons.Length; c++)
				{
					double t = 0.0;

					for (int n = 0; n < bp.Layers[li + 1].Neurons.Length; n++)
					{
						double v = bp.Layers[li + 1].Neurons[n].InputValue;
						double w = this.ML.Layers[li].Axons[c, n].Weight;

						t += v * w;
					}

					bp.Layers[li].Neurons[c].OutputValue = t;

					if (1 <= li)
					{
						double x = this.ML.Layers[li].Neurons[c].InputValue;
						double r = ActivationFunction.GetRateOfChange(x);

						t *= r;

						bp.Layers[li].Neurons[c].InputValue = t;
					}
				}
			}
		}

		private const double LEARNING_RATE = 0.1;

		private void ChangeWeight_Output(MultiLayer bp, int outputIndex, double expectedOutputValue)
		{
			double currentOutputValue = this.ML.Layers[this.ML.Layers.Length - 1].Neurons[outputIndex].InputValue;
			double d = expectedOutputValue - currentOutputValue; // 誤差

			d *= LEARNING_RATE;

			int li = bp.Layers.Length - 2;

			for (int c = 0; c < bp.Layers[li].Neurons.Length; c++)
			{
				double v = this.ML.Layers[li].Neurons[c].OutputValue;

				v *= d;

				this.ML.Layers[li].Axons[c, outputIndex].Weight += v;
			}
			while (0 <= --li)
			{
				for (int c = 0; c < bp.Layers[li].Neurons.Length; c++)
				{
					for (int n = 0; n < bp.Layers[li + 1].Neurons.Length; n++)
					{
						double v = this.ML.Layers[li].Neurons[c].OutputValue;

						v *= bp.Layers[li + 1].Neurons[n].InputValue;
						v *= d;

						this.ML.Layers[li].Axons[c, n].Weight += v;
					}
				}
			}
		}
	}
}
