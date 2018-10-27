using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class MultiLayerTest
	{
		public void Test01()
		{
			MultiLayer ml = new MultiLayer(3, 2, new int[] { 8, 8, 8, 8, 8, 8, 8, 8 });

			ml.SetInputValues(new double[] { 0.1, 0.9 });

			double[] outputs = ml.GetOutputs();
		}
	}
}
