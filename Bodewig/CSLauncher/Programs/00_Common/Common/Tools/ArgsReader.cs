using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArgsReader
	{
		public ArgsReader()
		{ }

		public ArgsReader(string[] args)
		{
			this.SetArgs(args);
		}

		private string[] Args = null;
		private int ArgIndex;

		public void SetArgs(string[] args)
		{
			this.Args = args;
			this.ArgIndex = 0;
		}

		private class SpellInfo
		{
			public string Spell;
			public Action SpellAction;
		}

		private List<SpellInfo> SpellInfos = new List<SpellInfo>();

		public void Add(string spell, Action action)
		{
			this.SpellInfos.Add(new SpellInfo()
			{
				Spell = spell,
				SpellAction = action,
			});
		}

		public void Perform(string[] args)
		{
			this.SetArgs(args);
			this.Perform();
		}

		public void Perform()
		{
			while (this.Perform_Main())
			{ }
		}

		private bool Perform_Main()
		{
			foreach (SpellInfo info in this.SpellInfos)
			{
				if (this.ArgIs(info.Spell))
				{
					info.SpellAction();
					return true;
				}
			}
			return false;
		}

		public bool HasArgs(int count = 1)
		{
			return count <= this.Args.Length - this.ArgIndex;
		}

		public bool ArgIs(string spell)
		{
			if (this.HasArgs() && this.GetArg().ToUpper() == spell.ToUpper())
			{
				this.ArgIndex++;
				return true;
			}
			return false;
		}

		public string GetArg(int index = 0)
		{
			return this.Args[this.ArgIndex + index];
		}

		public string NextArg()
		{
			string arg = this.GetArg();
			this.ArgIndex++;
			return arg;
		}
	}
}
