using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class ActivationFunction
	{
		public static double GetOutput(double value)
		{
			return Sigmoid.GetOutput(value);
			//return ReLU.GetOutput(value);
		}

		public static double GetDifferentialCoefficient(double value)
		{
			return Sigmoid.GetDifferentialCoefficient(value);
			//return ReLU.GetDifferentialCoefficient(value);
		}

		private class Sigmoid
		{
			public static double GetOutput(double value)
			{
				//if (double.IsNaN(value))
				//throw null;

				double ret = 1.0 / (1.0 + Math.Pow(Math.E, value * -1.0));

				//if (double.IsNaN(ret))
				//throw null;

				return ret;
			}

			public static double GetDifferentialCoefficient(double value)
			{
				value = GetOutput(value);
				value *= 1.0 - value;
				return value;
			}
		}

		private class ReLU
		{
			public static double GetOutput(double value)
			{
				return Math.Max(0.0, value);
			}

			public static double GetDifferentialCoefficient(double value)
			{
				return value < 0.0 ? 0.0 : 1.0;
			}
		}
	}
}
