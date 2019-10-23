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
			new MultiLayer(3, 2, new int[] { 8, 8, 8, 8, 8, 8, 8, 8 });
		}
	}
}
