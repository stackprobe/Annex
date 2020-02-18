using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public class TreeViewWP : TreeView
	{
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x203)
			{
				return;
			}
			base.WndProc(ref m);
		}
	}
}
