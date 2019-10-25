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
			new MultiLayer(new int[] { 6, 7, 7, 4 });
		}

		public void Test02()
		{
			Teacher trainer = new Teacher(new int[] { 2, 3, 3, 3, 2 });
		}
	}
}
