using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2("free", "work", "dictionary.txt", 'a', 'z');
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void Main2(string bgn, string end, string dicFile, int chrBgn, int chrEnd)
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();

			LoadDic(dic, bgn, end, dicFile);
			Search(dic, bgn, end, chrBgn, chrEnd);
			ShowResult(dic, end);
		}

		private void LoadDic(Dictionary<string, string> dic, string bgn, string end, string dicFile)
		{
			foreach(string word in File.ReadAllLines(dicFile))
				dic.Add(word, null);

			dic.Remove(bgn);
			dic.Remove(end);
			dic.Add(end, null);
		}

		private void Search(Dictionary<string, string> dic, string bgn, string end, int chrBgn, int chrEnd)
		{
			Queue<string> nextWords = new Queue<string>();
			nextWords.Enqueue(bgn);

			for (; ; )
			{
				string word = nextWords.Dequeue();

				for (int i = 0; i < word.Length; i++)
				{
					for (int c = chrBgn; c <= chrEnd; c++)
					{
						string tryWord = ChgChar(word, i, c);

						if (dic.ContainsKey(tryWord) && dic[tryWord] == null)
						{
							dic[tryWord] = word;

							if (tryWord == end)
								return;

							nextWords.Enqueue(tryWord);
						}
					}
				}
			}
		}

		private string ChgChar(string str, int chgPos, int newChr)
		{
			StringBuilder buff = new StringBuilder();

			for (int i = 0; i < str.Length; i++)
			{
				if (i == chgPos)
					buff.Append((char)newChr);
				else
					buff.Append(str[i]);
			}
			return buff.ToString();
		}

		private void ShowResult(Dictionary<string, string> dic, string word)
		{
			Stack<string> wordLink = new Stack<string>();
			wordLink.Push(word);

			while(dic.ContainsKey(word))
			{
				word = dic[word];
				wordLink.Push(word);
			}
			while (1 <= wordLink.Count)
			{
				Console.WriteLine(wordLink.Pop());
			}
		}
	}
}
