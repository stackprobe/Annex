using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte
{
	public class WinRect
	{
		public int l;
		public int t;
		public int w = -1; // -1 == not inited
		public int h;
		public bool maximized;

		public void fromLine(string line)
		{
			List<string> tokens = StringTools.tokenize(line, StringTools.DIGIT, true, true);

			l = int.Parse(tokens[0]);
			t = int.Parse(tokens[1]);
			w = int.Parse(tokens[2]);
			h = int.Parse(tokens[3]);
			maximized = StringTools.toFlag(tokens[4]);
		}

		public string toLine()
		{
			return l + ", " + t + ", " + w + ", " + h + ", " + StringTools.toString(maximized);
		}

		public void apply(Form f)
		{
			if (w == -1) // ? not inited
				return;

			f.Left = l;
			f.Top = t;
			f.Width = w;
			f.Height = h;
			f.WindowState = maximized ? FormWindowState.Maximized : FormWindowState.Normal;
		}

		public void set(Form f)
		{
			switch (f.WindowState)
			{
				case FormWindowState.Normal:
					l = f.Left;
					t = f.Top;
					w = f.Width;
					h = f.Height;
					maximized = false;
					break;

				case FormWindowState.Maximized:
					maximized = true;
					break;

				case FormWindowState.Minimized:
					maximized = false;
					break;
			}
		}
	}
}
