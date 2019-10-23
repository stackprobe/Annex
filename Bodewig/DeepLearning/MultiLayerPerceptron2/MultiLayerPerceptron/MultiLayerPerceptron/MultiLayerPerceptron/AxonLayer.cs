using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 軸索層
	/// </summary>
	public class AxonLayer
	{
		public double[,] WeightTable;

		/// <summary>
		/// 入力側ニューロン数
		/// バイアスは含まない。
		/// 直前のニューロンレイヤ(入力層又は隠れ層)のニューロンの個数と同じ。
		/// </summary>
		public int PrevCount;

		/// <summary>
		/// 出力側ニューロン数
		/// 直後のニューロンレイヤ(出力層又は隠れ層)のニューロンの個数と同じ。
		/// </summary>
		public int NextCount;

		public AxonLayer(int prevCount, int nextCount)
		{
			this.WeightTable = new double[prevCount + 1, nextCount];
			this.PrevCount = prevCount;
			this.NextCount = nextCount;
		}

		/// <summary>
		/// 全ての重みを適当な乱数で初期化する。
		/// </summary>
		public void Randomize()
		{
			for (int n = 0; n < this.NextCount; n++)
			{
				double wt = 0.0;
				//double wwt = 0.0;

				for (int p = 0; p < this.PrevCount; p++)
				{
					double w = SecurityTools.CRandom.GetReal() * 2.0 - 1.0;

					wt += w;
					//wwt += w * w;
				}
				this.WeightTable[this.PrevCount, n] = -wt; // 重みの平均が 0.0 になるようにバイアスで調整する。

				// 重みの標準偏差を調整する必要があるか？
			}
		}
	}
}
