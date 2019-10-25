using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 入力層・隠れ層・出力層 のいずれかと 次の層との間にある軸索層 の組み合わせ
	/// </summary>
	public class Layer
	{
		/// <summary>
		/// この層におけるニューロンの集まり
		/// </summary>
		public Neuron[] Neurons;

		/// <summary>
		/// この層から次の層に繋がる軸索の集まり
		/// 添字:[この層のニューロンのインデックス, 次の層のニューロンのインデックス]
		/// </summary>
		public Axon[,] Axons;
	}
}
