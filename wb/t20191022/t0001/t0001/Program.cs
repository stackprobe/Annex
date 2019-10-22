using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{e6434eaf-4c6f-4017-9385-ce0bc283167a}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			var ps = EnumerableTools.Linearize(
				Enumerable.Range(1, 13).Select(a => Enumerable.Range(1, 13).Where(b => a < b).Select(b => new { A = a, B = b }))
				)
				.ToList();

			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList();

			{
				var pairs2 = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList();
				var pairs3 = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList();

				ps = pairs2.Where(p => pairs3.Any(q => q.A == p.A && q.B == p.B)).ToList();
			}

			ps.ForEach(p => Console.WriteLine(p.A + " " + p.B));
		}
	}
}
