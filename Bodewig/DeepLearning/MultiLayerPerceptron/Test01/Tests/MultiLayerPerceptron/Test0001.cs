using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MultiLayerPerceptron;
using System.Drawing;

namespace Charlotte.Tests.MultiLayerPerceptron
{
	public class Test0001
	{
		public void Test01()
		{
			int BMP_WH = 1000;

			for (int testCount = 0; testCount < 100; testCount++)
			{
				Bitmap bmp = new Bitmap(BMP_WH, BMP_WH);

				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.FillRectangle(Brushes.White, 0, 0, 1000, 1000);

					{
						//MultiLayer ml = new MultiLayer(1, 1, new int[] { 8, 8, 8, 8, 8, 8, 8, 8 });
						MultiLayer ml = new MultiLayer(1, 1, new int[] { 1, 1, 1 });

						double X_MIN = -20.0;
						double X_MAX = 20.0;

						double[] ys = new double[BMP_WH];
						double y_min = double.MaxValue;
						double y_max = double.MinValue;

						double[] yDCs = new double[BMP_WH];
						double yDC_min = double.MaxValue;
						double yDC_max = double.MinValue;

						for (int x = 0; x < BMP_WH; x++)
						{
							ml.Clear();
							ml.SetInputs(new double[] { x * (X_MAX - X_MIN) / BMP_WH + X_MIN });
							ml.Fire();
							double y = ml.GetOutputs()[0];

							ys[x] = y;
							y_min = Math.Min(y_min, y);
							y_max = Math.Max(y_max, y);

							yDCs[x] = GetYDC(ml);
							yDC_min = Math.Min(yDC_min, yDCs[x]);
							yDC_max = Math.Max(yDC_max, yDCs[x]);
						}
						for (int x = 1; x < BMP_WH; x++)
						{
							int x1 = x - 1;
							int x2 = x;
							double y1 = ys[x1];
							double y2 = ys[x2];

							//Console.WriteLine(y1 + " -> " + y2);

							y1 = (y1 - y_min) * (double)BMP_WH / (y_max - y_min);
							y2 = (y2 - y_min) * (double)BMP_WH / (y_max - y_min);

							g.DrawLine(new Pen(Color.Blue), x1, (float)y1, x2, (float)y2);
						}
						for (int x = 1; x < BMP_WH; x++)
						{
							int x1 = x - 1;
							int x2 = x;
							double y1 = yDCs[x1];
							double y2 = yDCs[x2];

							//Console.WriteLine(y1 + " -> " + y2);

							y1 = (y1 - yDC_min) * (double)BMP_WH / (yDC_max - yDC_min);
							y2 = (y2 - yDC_min) * (double)BMP_WH / (yDC_max - yDC_min);

							g.DrawLine(new Pen(Color.OrangeRed), x1, (float)y1, x2, (float)y2);
						}
					}
				}
				bmp.Save(@"C:\temp\Test0001_Test01_" + testCount + ".png");
			}
		}

		private double GetYDC(MultiLayer ml)
		{
			double yDC = 1.0;

			yDC *= ml.Layers[0].Neurons[0].Prevs[0].Weight;
			yDC *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[0].Neurons[0].Input.Value);
			yDC *= ml.Layers[1].Neurons[0].Prevs[0].Weight;
			yDC *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[1].Neurons[0].Input.Value);
			yDC *= ml.Layers[2].Neurons[0].Prevs[0].Weight;
			yDC *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[2].Neurons[0].Input.Value);
			yDC *= ml.OutputLayer.Neurons[0].Prevs[0].Weight;

			return yDC;
		}

		public void Test02()
		{
			int BMP_WH = 1000;

			for (int testCount = 0; testCount < 100; testCount++)
			{
				Bitmap bmp = new Bitmap(BMP_WH, BMP_WH);

				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.FillRectangle(Brushes.White, 0, 0, 1000, 1000);

					{
						MultiLayer ml = new MultiLayer(1, 1, new int[] { 2, 2 });
						//MultiLayer ml = new MultiLayer(1, 1, new int[] { 1, 1, 1 });

						double X_MIN = -20.0;
						double X_MAX = 20.0;

						double[] ys = new double[BMP_WH];
						double y_min = double.MaxValue;
						double y_max = double.MinValue;

						double[] yDCs = new double[BMP_WH];
						double yDC_min = double.MaxValue;
						double yDC_max = double.MinValue;

						for (int x = 0; x < BMP_WH; x++)
						{
							ml.Clear();
							ml.SetInputs(new double[] { x * (X_MAX - X_MIN) / BMP_WH + X_MIN });
							ml.Fire();
							double y = ml.GetOutputs()[0];

							ys[x] = y;
							y_min = Math.Min(y_min, y);
							y_max = Math.Max(y_max, y);

							yDCs[x] = GetYDC_Test02(ml);
							yDC_min = Math.Min(yDC_min, yDCs[x]);
							yDC_max = Math.Max(yDC_max, yDCs[x]);
						}
						for (int x = 1; x < BMP_WH; x++)
						{
							int x1 = x - 1;
							int x2 = x;
							double y1 = ys[x1];
							double y2 = ys[x2];

							//Console.WriteLine(y1 + " -> " + y2);

							y1 = (y1 - y_min) * (double)BMP_WH / (y_max - y_min);
							y2 = (y2 - y_min) * (double)BMP_WH / (y_max - y_min);

							g.DrawLine(new Pen(Color.Blue), x1, (float)y1, x2, (float)y2);
						}
						for (int x = 1; x < BMP_WH; x++)
						{
							int x1 = x - 1;
							int x2 = x;
							double y1 = yDCs[x1];
							double y2 = yDCs[x2];

							//Console.WriteLine(y1 + " -> " + y2);

							y1 = (y1 - yDC_min) * (double)BMP_WH / (yDC_max - yDC_min);
							y2 = (y2 - yDC_min) * (double)BMP_WH / (yDC_max - yDC_min);

							g.DrawLine(new Pen(Color.OrangeRed), x1, (float)y1, x2, (float)y2);
						}
					}
				}
				bmp.Save(@"C:\temp\Test0001_Test02_" + testCount + ".png");
			}
		}

		private double GetYDC_Test02(MultiLayer ml)
		{
			double k1 = ml.Layers[0].Neurons[0].Prevs[0].Weight;
			double k2 = ml.Layers[0].Neurons[1].Prevs[0].Weight;

			k1 *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[0].Neurons[0].Input.Value);
			k2 *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[0].Neurons[1].Input.Value);

			double k3 =
				k1 * ml.Layers[1].Neurons[0].Prevs[0].Weight +
				k2 * ml.Layers[1].Neurons[0].Prevs[1].Weight;

			double k4 =
				k1 * ml.Layers[1].Neurons[1].Prevs[0].Weight +
				k2 * ml.Layers[1].Neurons[1].Prevs[1].Weight;

			k3 *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[1].Neurons[0].Input.Value);
			k4 *= ActivationFunction.GetDifferentialCoefficient(ml.Layers[1].Neurons[1].Input.Value);

			double k =
				k3 * ml.OutputLayer.Neurons[0].Prevs[0].Weight +
				k4 * ml.OutputLayer.Neurons[0].Prevs[1].Weight;

			return k;
		}
	}
}
