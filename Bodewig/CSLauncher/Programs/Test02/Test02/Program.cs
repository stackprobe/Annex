using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{14552449-d3ac-4d7b-a26f-f036e84c222a}";

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
