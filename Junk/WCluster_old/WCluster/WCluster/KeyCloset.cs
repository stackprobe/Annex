using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public static class KeyCloset
	{
		public static string closetDir
		{
			get
			{
				return Path.Combine(Program.selfDir, "key-closet");
			}
		}

		public static List<Key> getAll()
		{
			List<Key> dest = new List<Key>();

			foreach (string file in Directory.GetFiles(closetDir))
				dest.Add(Key.load(XNode.load(file)));

			sort(dest);
			return dest;
		}

		private static void sort(List<Key> list)
		{
			ArrayTools.sort<Key>(list, delegate(Key a, Key b)
			{
				int ret = StringTools.comp(a.getName(), b.getName());

				if (ret != 0)
					return ret;

				ret = StringTools.comp(a.getIdent(), b.getIdent());
				return ret;
			});
		}

		public static Key load(string ident)
		{
			return Key.load(XNode.load(Path.Combine(closetDir, ident + ".xml")));
		}

		public static void save(Key kb)
		{
			kb.save().save(Path.Combine(closetDir, kb.getIdent() + ".xml"));
		}

		public static void remove(Key kb)
		{
			File.Delete(Path.Combine(closetDir, kb.getIdent() + ".xml"));
		}
	}
}
