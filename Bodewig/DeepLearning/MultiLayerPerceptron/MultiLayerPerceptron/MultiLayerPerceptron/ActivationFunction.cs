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
		}

		public static double GetDifferentialCoefficient(double value)
		{
			return Sigmoid.GetDifferentialCoefficient(value);
		}

		private class Sigmoid
		{
			public static double GetOutput(double value)
			{
				return 1.0 / (1.0 + Math.Pow(Math.E, value * -1.0));
			}

			public static double GetDifferentialCoefficient(double value)
			{
				value = GetOutput(value);
				value *= 1.0 - value;
				return value;
			}
		}
	}
}
