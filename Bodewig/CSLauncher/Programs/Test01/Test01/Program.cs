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
		public const string APP_IDENT = "{16dff65c-995c-49d6-ac70-8d7b7ce7aa76}";

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
