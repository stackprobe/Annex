using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class MLPTester
	{
		public int TestCount;
		public int LoopCount;
		public int TrainCount;

		public Func<InputOutputPair> GetTrainingData;
		public Func<InputOutputPair> GetTestData;

		// <---- prm

		public void Perform()
		{
			for (int t = 0; t < this.TestCount; t++)
			{
				this.DoTestLoop();
			}
		}

		private void DoTestLoop()
		{
			for (int c = 0; c < this.LoopCount; c++)
			{
				this.DoTest();
				this.DoTraining();
			}
		}

		private void DoTest()
		{
			throw null; // TODO
		}

		private void DoTraining()
		{
			throw null; // TODO
		}
	}
}
