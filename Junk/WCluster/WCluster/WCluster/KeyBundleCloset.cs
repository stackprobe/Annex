using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class KeyBundleCloset
	{
		public static string closetDir
		{
			get
			{
				return Path.Combine(Program.selfDir, "key-bundle-closet");
			}
		}

		public static List<KeyBundle> getAll()
		{
			List<KeyBundle> dest = new List<KeyBundle>();

			foreach (string file in Directory.GetFiles(closetDir))
				dest.Add(KeyBundle.load(XNode.load(file)));

			sort(dest);
			return dest;
		}

		private static void sort(List<KeyBundle> list)
		{
			ArrayTools.sort<KeyBundle>(list, delegate(KeyBundle a, KeyBundle b)
			{
				int ret = StringTools.comp(a.getName(), b.getName());

				if (ret != 0)
					return ret;

				ret = StringTools.comp(a.getIdent(), b.getIdent());
				return ret;
			});
		}

		public static KeyBundle load(string ident)
		{
			return KeyBundle.load(XNode.load(Path.Combine(closetDir, ident + ".xml")));
		}

		public static void save(KeyBundle kb)
		{
			kb.save().save(Path.Combine(closetDir, kb.getIdent() + ".xml"));
		}

		public static void remove(KeyBundle kb)
		{
			File.Delete(Path.Combine(closetDir, kb.getIdent() + ".xml"));
		}
	}
}
