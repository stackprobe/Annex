using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tartelette
{
	public class SortedList<T>
	{
		private Comparison<T> _comp;
		private List<T> _list;
		private bool _sorted;

		public SortedList(Comparison<T> comp)
		{
			_comp = comp;
			_list = new List<T>();
			_sorted = false;
		}

		public SortedList(Comparison<T> comp, List<T> bind_list, bool sorted = false)
		{
			_comp = comp;
			_list = bind_list;
			_sorted = sorted;
		}

		public void Add(T e)
		{
			_list.Add(e);
			_sorted = false;
		}

		public void AddRange(IEnumerable<T> c)
		{
			_list.AddRange(c);
			_sorted = false;
		}

		public int Count
		{
			get
			{
				return _list.Count;
			}
		}

		public T this[int index]
		{
			get
			{
				this.TrySort();
				return _list[index];
			}
		}

		public T this[T ferret]
		{
			get
			{
				return this[this.IndexOf(ferret)];
			}
		}

		public int IndexOf(T ferret)
		{
			this.TrySort();

#if true
			int l = -1;
			int r = _list.Count;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int ret = _comp(ferret, _list[m]);

				if (ret == 0)
					return m;

				if (ret < 0)
					r = m;
				else
					l = m;
			}
			return -1;
#else
			int l = 0;
			int r = _list.Count;

			while(l < r)
			{
				int m = (l + r) / 2;
				int ret = _comp(ferret, _list[m]);

				if (ret == 0)
					return m;

				if (ret < 0)
					r = m;
				else
					l = m + 1;
			}
			return -1;
#endif
		}

		private void TrySort()
		{
			if (_sorted == false)
			{
				this.DoSort();
				_sorted = true;
			}
		}

		private void DoSort()
		{
			_list.Sort(_comp);
		}

		private delegate bool IsHi(T e);

		public int[] GetRange(T ferret)
		{
			int m = this.IndexOf(ferret);

			if (m == -1)
				return null;

			int beginPos = this.GetBorder(
				-1,
				m,
				(IsHi)delegate(T e)
				{
					return _comp(ferret, e) == 0;
				}
				) + 1;

			int endPos = this.GetBorder(
				m,
				_list.Count,
				(IsHi)delegate(T e)
				{
					return _comp(ferret, e) != 0;
				}
				);

			return new int[] { beginPos, endPos };
		}

		private int GetBorder(int l, int r, IsHi isHi)
		{
			while (l + 1 < r)
			{
				int m = (l + r) / 2;

				if (isHi(_list[m]))
					r = m;
				else
					l = m;
			}
			return l;
		}

		public int GetCount(T ferret)
		{
			int[] range = this.GetRange(ferret);

			if (range == null)
				return 0;

			return range[1] - range[0] + 1;
		}
	}
}
