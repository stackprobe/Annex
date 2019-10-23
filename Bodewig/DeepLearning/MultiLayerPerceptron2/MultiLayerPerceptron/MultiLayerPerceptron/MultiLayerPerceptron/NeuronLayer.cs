using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// ニューロン層
	/// 入力層・隠れ層・出力層 に相当する。
	/// </summary>
	public class NeuronLayer
	{
		public double[] Inputs;
		public double[] Outputs;

		/// <summary>
		/// ニューロン数
		/// </summary>
		public int Count
		{
			get
			{
				return this.Inputs.Length;
			}
		}

		public NeuronLayer(int count)
		{
			this.Inputs = new double[count];
			this.Outputs = new double[count];
		}
	}
}
