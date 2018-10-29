using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron.Utils;

namespace Charlotte.MultiLayerPerceptron
{
	public abstract class Neuron
	{
		public List<Axon> Prevs = new List<Axon>();
		public List<Axon> Nexts = new List<Axon>();

		public double? Input = null;
		public double? Output = null;

		public void Clear()
		{
			this.Input = null;
			this.Output = null;
		}

		public abstract void Fire();

		public override string ToString()
		{
			return
				"(" +
				string.Join(", ", this.Prevs.Select(prev => CommonUtils.PutSign(prev.Weight.ToString("F9")))) +
				") " +
				CommonUtils.ToString(this.Input, dummy => CommonUtils.PutSign(this.Input.Value.ToString("F9"))) +
				" => " +
				CommonUtils.ToString(this.Output, dummy => CommonUtils.PutSign(this.Output.Value.ToString("F9")));
		}
	}
}
