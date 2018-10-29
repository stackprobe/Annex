using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using System.IO;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class MultiLayerTest
	{
		public void Test01()
		{
			MultiLayer ml = new MultiLayer(3, 2, new int[] { 8, 8, 8, 8, 8, 8, 8, 8 });

			File.WriteAllLines(@"C:\temp\0001.txt", ml.ToStrings(), Encoding.ASCII); // test
			ml.Clear();
			File.WriteAllLines(@"C:\temp\0002.txt", ml.ToStrings(), Encoding.ASCII); // test
			ml.SetInputs(new double[] { 0.1, 0.5, 0.9 });
			File.WriteAllLines(@"C:\temp\0003.txt", ml.ToStrings(), Encoding.ASCII); // test
			ml.Fire();
			File.WriteAllLines(@"C:\temp\0004.txt", ml.ToStrings(), Encoding.ASCII); // test

			double[] outputs = ml.GetOutputs();

			Console.WriteLine(string.Join(", ", outputs.Select(output => output.ToString("F9"))));

			File.WriteAllLines(@"C:\temp\1000.txt", ml.ToStrings(), Encoding.ASCII);

			foreach (string line in ml.ToStrings())
				Console.WriteLine(line);
		}

		public void Test02()
		{
			MultiLayer ml = new MultiLayer(2, 1, new int[] { 4, 4, 4, 4 });

			ml.Clear();
			ml.SetInputs(new double[] { 0.1, 0.9 });
			ml.Fire();

			double[] outputs = ml.GetOutputs();

			Console.WriteLine(string.Join(", ", outputs.Select(output => output.ToString("F9"))));

			File.WriteAllLines(@"C:\temp\1000.txt", ml.ToStrings(), Encoding.ASCII);
		}
	}
}
