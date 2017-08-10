using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools.Types
{
	public class Int_t : Base_t<int>
	{
		private int _minval;
		private int _maxval;

		public Int_t(int minval = 0, int maxval = IntTools.IMAX)
		{
			_minval = minval;
			_maxval = maxval;

			this.Set(-1);
		}

		public override void Set(int value)
		{
			value = IntTools.ToRange(value, _minval, _maxval);
			_value = value;
		}

		public override int Get()
		{
			return _value;
		}

		public Int_t Init(int value)
		{
			this.Set(value);
			return this;
		}
	}
}
