using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Test
{
	public class Test1
	{
		public void Test01()
		{
			Test01_01(new string[] { "o", "o'", "o-", "'o", "-o" });

			DebugTools.WriteLog_Console("CC.1");
			Test01_02(new string[] { "o", "o'", "o-", "'o", "-o" }, StringTools.Comp);
			DebugTools.WriteLog_Console("CC.2");
			Test01_02(new string[] { "o", "o'", "o-", "'o", "-o" }, delegate(string a, string b) { return a.CompareTo(b); });

			DebugTools.WriteLog_Console("CC_2.1");
			Test01_02(DebugTools.MakeRandLines(StringTools.ASCII, 2, 2, 1000), StringTools.Comp);
			DebugTools.WriteLog_Console("CC_2.2");
			Test01_02(DebugTools.MakeRandLines(StringTools.ASCII, 2, 2, 1000), delegate(string a, string b) { return a.CompareTo(b); });

			DebugTools.WriteLog_Console("CC_3");
			//Test01_02(File.ReadAllLines("C:/tmp/20161216_FileSorter_div_2.txt", StringTools.ENCODING_SJIS), StringTools.Comp); // エラーになる。

			Test01_01(new string[] { "C", "c-", "-c" });

			DebugTools.WriteLog("" + ("C".CompareTo("c-")));
			DebugTools.WriteLog("" + ("c-".CompareTo("-c")));
			DebugTools.WriteLog("" + ("-c".CompareTo("C")));

			DebugTools.WriteLog("" + ("X".CompareTo("x-")));
			DebugTools.WriteLog("" + ("x-".CompareTo("-x")));
			DebugTools.WriteLog("" + ("-x".CompareTo("X")));

			Test01_02(new string[] { "X", "x-", "-x" }, StringTools.Comp);
		}

		private void Test01_01(string[] strSet)
		{
			foreach (string a in strSet)
			{
				foreach (string b in strSet)
				{
					DebugTools.WriteLog("\"" + a + "\" - \"" + b + "\" -> " + StringTools.Comp(a, b) + ", " + a.CompareTo(b));
				}
			}
		}

		private void Test01_02(string[] strSet, Comparison<string> comp)
		{
			CompChecker<string> cc = new CompChecker<string>(comp);

			foreach (string a in strSet)
			{
				if (cc.Add(a) == false)
				{
					{
						int layerIndex = 0;

						foreach (List<string> layer in cc.GetLayers())
						{
							DebugTools.WriteLog("Layer {");

							foreach (string value in layer)
							{
								DebugTools.WriteLog("Layer-" + layerIndex + "=[" + value + "]");
							}
							DebugTools.WriteLog("}");

							layerIndex++;
						}
					}

					DebugTools.WriteLog("Compare-History {");

					foreach (string line in cc.GetLastCompHistory())
					{
						DebugTools.WriteLog("\t" + line);
					}
					DebugTools.WriteLog("}");

					DebugTools.WriteLog("追加出来なかった要素=[" + a + "]");
					throw null;
				}
			}
		}

		private class CompChecker<T>
		{
			private Comparison<T> _comp;
			private List<List<T>> _layers = new List<List<T>>();

			public CompChecker(Comparison<T> comp)
			{
				_comp = comp;
			}

			private List<string> _compHist = new List<string>();

			public bool Add(T valueNew)
			{
				_compHist.Clear();

				if (_layers.Count == 0)
				{
					_layers.Add(new List<T>());
					_layers[0].Add(valueNew);
				}
				else
				{
					int lowOutIndex = -1;
					int hiOutIndex = _layers.Count;

					for (int index = 0; index < _layers.Count; index++)
					{
						List<T> layer = _layers[index];

						foreach (T value in layer)
						{
							int ret = _comp(valueNew, value);

							{
								string neeq;

								if (ret < 0)
									neeq = "<";
								else if (0 < ret)
									neeq = ">";
								else
									neeq = "=";

								_compHist.Add("[" + valueNew + "] " + neeq + " [" + value + "]");
							}

							if (ret < 0) // valueNew < value
							{
								if (index <= lowOutIndex)
									return false;

								hiOutIndex = Math.Min(hiOutIndex, index);
							}
							else if (0 < ret) // value < valueNew
							{
								if (hiOutIndex <= index)
									return false;

								lowOutIndex = Math.Max(lowOutIndex, index);
							}
							else // value == valueNew
							{
								if (index <= lowOutIndex)
									return false;

								if (hiOutIndex <= index)
									return false;

								lowOutIndex = index - 1;
								hiOutIndex = index + 1;
							}
						}
					}
					if (lowOutIndex + 1 == hiOutIndex)
					{
						_layers.Insert(hiOutIndex, new List<T>());
						_layers[hiOutIndex].Add(valueNew);
					}
					else if (lowOutIndex + 2 == hiOutIndex)
					{
						_layers[lowOutIndex + 1].Add(valueNew);
					}
					else
					{
						return false;
					}
				}
				return true;
			}

			public List<List<T>> GetLayers()
			{
				return _layers;
			}

			public List<string> GetLastCompHistory()
			{
				return _compHist;
			}
		}
	}
}
