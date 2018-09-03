using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd I
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}
	}
}
