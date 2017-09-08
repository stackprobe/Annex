using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class SortedList<T>
	{
		private List<T> List;
		private Comparison<T> Comp;
		private bool SortedFlag;

		public SortedList(List<T> bind_list, Comparison<T> cmp, bool sortedFlag = false)
		{
			this.List = bind_list;
			this.Comp = cmp;
			this.SortedFlag = sortedFlag;
		}

		public SortedList(Comparison<T> cmp)
			: this(new List<T>(), cmp, true)
		{ }

		public void Add(T element, bool keepOrder = false)
		{
			this.List.Add(element);
			this.SortedFlag = this.SortedFlag && keepOrder;
		}

		public int Count
		{
			get { return this.List.Count; }
		}

		public T this[int index]
		{
			get
			{
				this.TrySort();
				return this.List[index];
			}
		}

		private void TrySort()
		{
			if (this.SortedFlag == false)
			{
				this.Sort();
				this.SortedFlag = true;
			}
		}

		private void Sort()
		{
			this.List.Sort(this.Comp);
		}

		public List<T> GetMatch(T ferret, Comparison<T> cmp = null)
		{
			List<T> buff = new List<T>();
			int[] lr = this.GetRange(ferret, cmp);

			for (int index = lr[0] + 1; index < lr[1]; index++)
				buff.Add(this[index]);

			return buff;
		}

		public List<T> GetMatchAndEdge(T ferret, T edge, Comparison<T> cmp = null)
		{
			List<T> buff = new List<T>();
			int[] lr = this.GetRange(ferret, cmp);

			for (int index = lr[0]; index <= lr[1]; index++)
			{
				if (0 <= index && index < this.Count)
					buff.Add(this[index]);
				else
					buff.Add(edge);
			}
			return buff;
		}

		public int[] GetRange(T ferret, Comparison<T> cmp = null)
		{
			if (cmp == null)
				cmp = this.Comp;

			int l = -1;
			int r = this.Count;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int ret = cmp(this[m], ferret);

				if (ret == 0)
				{
					l = this.GetBorder(l, m, ferret, delegate(T a, T b) { return cmp(a, b) == 0 ? 1 : 0; })[0];
					r = this.GetBorder(m, r, ferret, cmp)[1];
					break;
				}
				if (ret < 0)
					l = m;
				else
					r = m;
			}
			return new int[] { l, r };
		}

		private int[] GetBorder(int l, int r, T ferret, Comparison<T> cmp)
		{
			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int ret = cmp(this[m], ferret);

				if (ret == 0)
					l = m;
				else
					r = m;
			}
			return new int[] { l, r };
		}
	}
}
