using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	/// <summary>
	/// ダブルクォーテーションで囲まれていない値
	/// </summary>
	public class JsonValue
	{
		private string _value;

		public JsonValue(string value)
		{
			_value = value;
		}

		public String GetValue()
		{
			return _value;
		}

		public override string ToString()
		{
			return GetValue();
		}
	}
}
