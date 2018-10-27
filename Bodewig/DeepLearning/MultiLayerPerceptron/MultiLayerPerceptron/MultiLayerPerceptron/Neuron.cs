using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MultiLayerPerceptron
{
	public abstract class Neuron
	{
		public List<Axon> Prevs = new List<Axon>();
		public List<Axon> Nexts = new List<Axon>();

		public abstract double GetOutput();
	}
}
