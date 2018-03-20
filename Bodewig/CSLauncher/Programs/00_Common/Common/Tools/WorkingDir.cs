using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class WorkingDir
	{
		private const string ROOT_IDENT = "{7f54a509-d3f6-4fe4-a032-4405be839829}";

		private static string RootDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), ROOT_IDENT);

		private string Dir = Path.Combine(RootDir, Guid.NewGuid().ToString("B"));

		public WorkingDir()
		{
			using (MSection m = new MSection(ROOT_IDENT)) // RootDir を作成する場合もあるので、排他する。
			{
				FileTools.CreateDir(this.Dir);
			}
		}

		public string MakePath()
		{
			return this.GetPath(Guid.NewGuid().ToString("B"));
		}

		public string GetPath(string localName)
		{
			return Path.Combine(this.Dir, localName);
		}

		public void Dispose()
		{
			FileTools.Delete(this.Dir);
		}

		public static void Cleanup()
		{
			using (MSection m = new MSection(ROOT_IDENT))
			{
				FileTools.Delete(RootDir);
			}
		}
	}
}
