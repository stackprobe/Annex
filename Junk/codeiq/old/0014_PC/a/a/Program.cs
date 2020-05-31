using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(12);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.ReadLine();
		}

		private int MemberNum;
		private bool[] PCTake;
		private int PtnNum;

		private void Main2(int num)
		{
			MemberNum = num;
			PCTake = new bool[MemberNum];
			PtnNum = 0;

			TryNext(0);

			Console.WriteLine(PtnNum + "通り");
		}

		private void TryNext(int member)
		{
			if (member == MemberNum)
			{
				PtnNum++;
				return;
			}

			for (int index = 0; index < MemberNum; index++)
			{
				if (index != member && PCTake[index] == false)
				{
					PCTake[index] = true;
					TryNext(member + 1);
					PCTake[index] = false;
				}
			}
		}
	}
}
