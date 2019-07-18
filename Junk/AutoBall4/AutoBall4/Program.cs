using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace AutoBall4
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("\\e");
			Console.ReadLine();
		}

		/// <summary>
		/// http://www.gamedesign.jp/flash/balls/balls_jp.html
		/// 1. chromeで開いて、左上の座標が(0,0)のモニタで最大化する。(1920x1080のモニタであること,ブックマークバー表示であること)
		/// 2. 自分のターンまで進めてから起動する。
		/// 3. コンソールをゲーム画面から避ける。
		/// </summary>
		private void Main2()
		{
			for (; ; )
			{
				for (; ; )
				{
					Thread.Sleep(3000);

					// ---- 自分のターンを処理する ----

					string field = ScanField();

					Console.WriteLine("field: " + field);

					if (field == null)
						break;

					string[] evals = GetEvals(field);
					int x = GetNextX(evals);
					PutStone(x);

					// ----
				}
				LClick_Bure(1080, 570 - 210, 5);
				Thread.Sleep(3000);
				LClick_Bure(1047, 747 - 210, 5);
			}
		}

		private string ScanField()
		{
#if true
			WinTools.PrintScreen("1.png");
#else // old 遅い
			//SystemTools.Execute(@"C:\app\kit\ImgTools\ImgTools.exe", "/WF 1.png /PRTSC");
#endif
			SystemTools.Execute(@"C:\app\kit\BmpToCsv\BmpToCsv.exe", "1.png 1.csv");

			string[][] csv = FileTools.ReadCsvFile("1.csv");

			// 試合終了チェック
			{
				string cell = csv[570 - 210][1080];

				if (cell == "ffc9d988") // ? 試合終了
					return null;
			}

			string ret = "";

			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 7; x++)
				{
					string cell = csv[410 + 4 * 40 - y * 40][840 + x * 40];

					if (cell == "ffc9d988") // 空き
					{
						ret += "0";
					}
					else if (cell == "ffff0066") // 自分
					{
						ret += "1";
					}
					else if (cell == "ff0099ff") // 相手
					{
						ret += "2";
					}
					else
					{
						throw new Exception("unknown cell: " + cell);
					}
				}
			}
			return ret;
		}

		private string[] GetEvals(string field)
		{
			SystemTools.Execute(@"C:\Factory\Labo\Junk\Ques\Ball4.exe", "//O 1.tmp /F " + field + " /N");
			string[] lines = File.ReadAllLines("1.tmp");
			return lines;
		}

#if true
		private int GetNextX(string[] evals)
		{
			int maxEval = int.MinValue;
			List<int> maxIndexes = new List<int>();

			for (int index = 0; index < evals.Length; index++)
			{
				int eval = int.Parse(evals[index]);

				if (maxEval < eval)
				{
					maxEval = eval;
					maxIndexes.Clear();
				}
				if (maxEval == eval)
				{
					maxIndexes.Add(index);
				}
			}
			int ret = maxIndexes[_r.Next(maxIndexes.Count)];

			Console.WriteLine("maxEval: " + maxEval);

			for (int index = 0; index < evals.Length; index++)
			{
				Console.WriteLine(
					index + " -> " + evals[index] +
					(maxIndexes.Contains(index) ? " max" : "") +
					(ret == index ? " ret" : "")
					);
			}
			Console.WriteLine("ret: " + ret);

			if (maxEval <= 1)
				throw new Exception("負け");

			return ret;
		}
#else // old
		private int GetNextX(string[] evals)
		{
			int maxEval = 0;
			int maxIndex = 0;

			for (int index = 0; index < evals.Length; index++)
			{
				int eval = int.Parse(evals[index]);

				if (maxEval < eval)
				{
					maxEval = eval;
					maxIndex = index;
				}
			}
			return maxIndex;
		}
#endif

		private void PutStone(int x)
		{
			LClick_Bure(840 + x * 40, 370, 10);
		}

		private Random _r = new Random();

		private void LClick_Bure(int x, int y, int bure)
		{
			LClick(
				x + _r.Next(-bure, bure),
				y + _r.Next(-bure, bure)
				);
		}

		private void LClick(int x, int y)
		{
			//for (int c = 0; c < 3; c++) // 時々クリック失敗するので、何回か押す。<- 失敗じゃなくてスクリプト対策ぽい？
			{
				SystemTools.Execute(@"C:\Factory\Labo\Tools\MouseTools.exe", "/LC " + x + " " + y);
			}
		}
	}
}
