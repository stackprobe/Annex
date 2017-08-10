using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools.Types
{
	public abstract class Base_t<T>
	{
		protected T _value;

		public abstract void Set(T value);
		public abstract T Get();

		public T Value
		{
			set
			{
				this.Set(value);
			}
			get
			{
				return this.Get();
			}
		}
	}
}
