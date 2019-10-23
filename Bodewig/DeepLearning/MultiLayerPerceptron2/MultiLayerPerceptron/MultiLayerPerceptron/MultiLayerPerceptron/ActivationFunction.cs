using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	/// <summary>
	/// 活性化関数
	/// シグモイド関数を使用する。
	/// </summary>
	public static class ActivationFunction
	{
		/// <summary>
		/// 活性化関数の評価値を得る。
		/// </summary>
		/// <param name="value">入力値</param>
		/// <returns>出力値</returns>
		public static double GetOutput(double value)
		{
			return 1.0 / (1.0 + Math.Pow(Math.E, value * -1.0));
		}

		/// <summary>
		/// 活性化関数の評価値の変化率を得る。
		/// </summary>
		/// <param name="value">入力値</param>
		/// <returns>出力値の変化率</returns>
		public static double GetRateOfChange(double value)
		{
			value = GetOutput(value);
			value *= 1.0 - value;
			return value;
		}
	}
}
