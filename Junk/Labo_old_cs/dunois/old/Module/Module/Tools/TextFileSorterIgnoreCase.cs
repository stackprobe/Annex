using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class TextFileSorterIgnoreCase : TextFileSorter
	{
		public TextFileSorterIgnoreCase(Encoding encoding) :
			base(encoding)
		{ }

		protected override int Comp(string str1, string str2)
		{
			return StringTools.CompIgnoreCase(str1, str2);
		}
	}
}
