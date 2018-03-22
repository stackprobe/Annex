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
		public const string APP_IDENT = "{8c4247c9-4dc9-445c-a57d-0afb77d108f4}";

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
