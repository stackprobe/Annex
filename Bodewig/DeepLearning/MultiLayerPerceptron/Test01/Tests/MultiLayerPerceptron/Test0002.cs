using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.MultiLayerPerceptron;
using System.Drawing;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class Test0002
	{
		public void Test01()
		{
			//MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5 });
			MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5, 5 });
			//MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5, 5, 5 });

			ml.Clear();
			ml.SetInputs(new double[]
			{
				SecurityTools.CRandom.GetReal(),
				SecurityTools.CRandom.GetReal(),
				SecurityTools.CRandom.GetReal(),
			});
			ml.Fire();

			Axon axon = ml.Layers[1].Neurons[1].Nexts[2]; // 第2隠れ層の第2ニューロンから第3隠れ層の第3ニューロンへの軸索

			OutputNeuron output = (OutputNeuron)ml.OutputLayer.Neurons[1];

			//double k = axon.GetDifferentialCoefficient_Weight_Output(output);
			//double t = output.Output.Value;

			{
				int BMP_WH = 1000;
				double W_MIN = -100.0;
				double W_MAX = 100.0;

				double[] tts = new double[BMP_WH];
				double tt_min = double.MaxValue;
				double tt_max = double.MinValue;

				Bitmap bmp = new Bitmap(BMP_WH, BMP_WH);

				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.FillRectangle(Brushes.White, 0, 0, BMP_WH, BMP_WH);

					for (int x = 0; x < BMP_WH; x++)
					{
						axon.Weight = x * (W_MAX - W_MIN) / BMP_WH + W_MIN;

						ml.Fire();

						double tt = output.Output.Value;

						tts[x] = tt;
						tt_min = Math.Min(tt_min, tt);
						tt_max = Math.Max(tt_max, tt);
					}
					for (int x = 1; x < BMP_WH; x++)
					{
						int x1 = x - 1;
						int x2 = x;

						double y1 = (tts[x1] - tt_min) * BMP_WH / (tt_max - tt_min);
						double y2 = (tts[x2] - tt_min) * BMP_WH / (tt_max - tt_min);

						g.DrawLine(new Pen(Color.Blue), x1, (float)y1, x2, (float)y2);
					}
				}
				bmp.Save(@"C:\temp\Test0002_Test01.png");
			}
		}

		public void Test01b()
		{
			//MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5 });
			//MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5, 5 });
			//MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5, 5, 5 });
			MultiLayer ml = new MultiLayer(3, 2, new int[] { 5, 5, 5, 5, 5, 6, 7, 8, 7, 6 });

			ml.Clear();
			ml.SetInputs(new double[]
			{
				SecurityTools.CRandom.GetReal(),
				SecurityTools.CRandom.GetReal(),
				SecurityTools.CRandom.GetReal(),
			});
			ml.Fire();

			Axon axon = ml.Layers[1].Neurons[1].Nexts[2]; // 第2隠れ層の第2ニューロンから第3隠れ層の第3ニューロンへの軸索

			OutputNeuron output = (OutputNeuron)ml.OutputLayer.Neurons[1];

			Console.WriteLine(axon.Weight.ToString("F9"));
			Console.WriteLine(axon.GetDifferentialCoefficient_Weight_Output(output).ToString("F9")); // *1
			Console.WriteLine(output.Output.Value.ToString("F9"));

			// ----

			double W_SPAN = 0.001;

			double x1 = output.Output.Value;
			axon.Weight += W_SPAN;
			ml.Fire();
			double x2 = output.Output.Value;
			double k = (x2 - x1) / W_SPAN;

			Console.WriteLine(k.ToString("F9")); // *1とだいたい合うはず！
		}
	}
}
