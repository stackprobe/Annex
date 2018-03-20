using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Test03
{
	class Program
	{
		public const string APP_IDENT = "{65e01651-29e6-4da0-a181-2563e3246505}";

		static void Main(string[] args)
		{
			try
			{
				new Program().Main2(new ArgsReader(args));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				MessageBox.Show("" + e);
			}
		}

		private void Main2(ArgsReader ar)
		{
			throw null; // TODO
		}
	}
}
