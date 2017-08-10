using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XML
{
	class Program
	{
		static void Main(string[] args)
		{
			XNode root = XLoader.Load("C:\\tmp\\test.xml");
			//XNode root = XLoad.Load(args[0]);

			root.DebugPrint(0);

			Console.ReadLine();
		}
	}
}
