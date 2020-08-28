using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class ArgsReaderTest
	{
		public void Test01()
		{
			ArgsReader ar = new ArgsReader();

			ar.Add("/A", () => Console.WriteLine("This is option A."));
			ar.Add("/B", () => Console.WriteLine("This is option B."));
			ar.Add("/C", () => Console.WriteLine("This is option C."));

			ar.Perform(new string[] { "/C", "/B", "/A", "/B", "/B", "/X", "/Y", "/Z", "/A" });

			Console.WriteLine(ar.NextArg());
			Console.WriteLine(ar.NextArg());
			Console.WriteLine(ar.NextArg());
			Console.WriteLine(ar.NextArg());
		}
	}
}
