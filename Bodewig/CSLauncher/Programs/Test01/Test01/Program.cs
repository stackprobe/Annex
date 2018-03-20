using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Test01
{
	class Program
	{
		public const string APP_IDENT = "{de42af6e-d546-483e-b4c7-c2efbd65dbc8}";

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
