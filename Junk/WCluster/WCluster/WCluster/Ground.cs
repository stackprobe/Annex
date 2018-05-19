using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class Gnd
	{
		private static Gnd _i = null;

		public static Gnd i
		{
			get
			{
				if (_i == null)
					_i = new Gnd();

				return _i;
			}
		}

		private Gnd()
		{ }

		// ---- conf data ----

		public string dummyConfString = "default";

		public void loadConf()
		{
			try
			{
				List<string> lines = new List<string>();

				foreach (string line in FileTools.readAllLines(getConfFile(), StringTools.ENCODING_SJIS))
					if (line != "" && line[0] != ';')
						lines.Add(line);

				int c = 0;

				// items >

				dummyConfString = lines[c++];

				// < items
			}
			catch
			{ }
		}

		private string getConfFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".conf");
		}

		// ---- saved data ----

		public Consts.DropImageSize_e compressDropImageSize = Consts.DropImageSize_e._256;
		public WinRect compressMainDlgRect = new WinRect();

		public Consts.DropImageSize_e decompressDropImageSize = Consts.DropImageSize_e._256;
		public WinRect decompressMainDlgRect = new WinRect();

		public WinRect settingWinRect = new WinRect();
		public WinRect keyClosetWinRect = new WinRect();
		public WinRect keyBundleClosetWinRect = new WinRect();

		public void loadData()
		{
			try
			{
				string[] lines = File.ReadAllLines(getDataFile(), Encoding.UTF8);
				int c = 0;

				// items >

				compressDropImageSize = (Consts.DropImageSize_e)int.Parse(lines[c++]);
				compressMainDlgRect.fromLine(lines[c++]);

				decompressDropImageSize = (Consts.DropImageSize_e)int.Parse(lines[c++]);
				decompressMainDlgRect.fromLine(lines[c++]);

				settingWinRect.fromLine(lines[c++]);
				keyClosetWinRect.fromLine(lines[c++]);
				keyBundleClosetWinRect.fromLine(lines[c++]);

				// < items
			}
			catch
			{ }
		}

		public void saveData()
		{
			try
			{
				List<string> lines = new List<string>();

				// items >

				lines.Add("" + (int)compressDropImageSize);
				lines.Add(compressMainDlgRect.toLine());

				lines.Add("" + (int)decompressDropImageSize);
				lines.Add(decompressMainDlgRect.toLine());

				lines.Add(settingWinRect.toLine());
				lines.Add(keyClosetWinRect.toLine());
				lines.Add(keyBundleClosetWinRect.toLine());

				// < items

				File.WriteAllLines(getDataFile(), lines, Encoding.UTF8);
			}
			catch
			{ }
		}

		private string getDataFile()
		{
			return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".dat");
		}

		// ----

		public Logger logger = new Logger();
	}
}
