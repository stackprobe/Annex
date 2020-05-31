/*
	バブルソートのスワップ回数 == 各要素の「自分以降の自分より小さい要素の数」の合計
	であるかどうか？ -> そうみたい
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

static autoList_t *List;

static void MkList(void)
{
	uint num = mt19937_range(2, 1000);
	uint c;

	if(!List)
		List = newList();

	setCount(List, 0);

	for(c = num; c; c--)
		addElement(List, mt19937_range(1, 1000));
}
static uint GetLLCount(void)
{
	uint i;
	uint j;
	uint cnt = 0;

	for(i = 0;     i < getCount(List); i++)
	for(j = i + 1; j < getCount(List); j++)
	{
		if(getElement(List, j) < getElement(List, i))
			cnt++;
	}
	return cnt;
}
static uint DoBSort(void)
{
	uint cnt = 0;

	for(; ; )
	{
		uint i;
		int swapped = 0;

		for(i = 0; i + 1 < getCount(List); i++)
		{
			if(getElement(List, i + 1) < getElement(List, i))
			{
				swapElement(List, i, i + 1);
				swapped = 1;
				cnt++;
			}
		}
		if(!swapped)
			break;
	}
	return cnt;
}

static void ShowList(void)
{
	uint i;

	for(i = 0; i < getCount(List); i++)
	{
		if(i)
			cout(", ");

		cout("%u", getElement(List, i));
	}
	cout("\n");
}

int main(int argc, char **argv)
{
	mt19937_init();

	while(!hasKey())
	{
		uint cnt1;
		uint cnt2;

		MkList();
//		ShowList();
		cout("%u\n", getCount(List));

		cnt1 = GetLLCount();
		cnt2 = DoBSort();
//		ShowList();

		cout("%u %u\n", cnt1, cnt2);

		errorCase(cnt1 != cnt2);
	}
}
