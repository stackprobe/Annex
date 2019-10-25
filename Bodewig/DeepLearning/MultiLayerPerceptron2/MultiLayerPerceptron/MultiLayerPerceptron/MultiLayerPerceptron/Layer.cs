using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
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
