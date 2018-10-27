using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public class Axon
	{
		public Neuron Prev;
		public Neuron Next;

		// <---- prm

		public double Weight = 0.1;

		// <---- prop
	}
}
