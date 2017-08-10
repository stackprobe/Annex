using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFrmApp1
{
	public class G
	{
		private G()
		{ }
		public static G I = new G();

		public bool CloseForm1Flag;
	}
}
