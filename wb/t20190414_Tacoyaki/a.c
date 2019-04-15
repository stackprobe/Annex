// use int

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\autoTable.h"
#include "C:\Factory\Common\Options\CRRandom.h"

//#define WH 40 // èdÇ¢
//#define WH 35 // ë¨Ç¢
//#define WH 30 // ÇﬂÇ¡ÇøÇ·ë¨Ç¢
#define WH 25 // íxÇ¢
//#define WH 20 // íxÇ¢
//#define WH 15
//#define WH 10
//#define WH 5

static autoTable_t *Table;

#if 0
static void PrintTable(void)
{
	int x;
	int y;

	cout("====\n");

	for(y = 0; y < WH; y++)
	{
		for(x = 0; x < WH; x++)
			cout("%c", getTableCell(Table, x, y) ? 'X' : '-');

		cout("\n");
	}
	cout("====\n");
}
#else
#define PrintTable() 1
#endif
static int IsAllZero(void)
{
	int x;
	int y;

	for(x = 0; x < WH; x++)
	for(y = 0; y < WH; y++)
	{
		if(getTableCell(Table, x, y))
		{
			return 0;
		}
	}
	return 1;
}
static void Reverse(int x, int y)
{
	if(m_isRange(x, 0, WH - 1))
	if(m_isRange(y, 0, WH - 1))
	{
		*tableCellAt(Table, x, y) ^= 1;
	}
}
static void Kick(int x, int y)
{
	Reverse(x, y);
	Reverse(x - 1, y);
	Reverse(x + 1, y);
	Reverse(x, y - 1);
	Reverse(x, y + 1);
}
static void MkTable(void)
{
	int x;
	int y;

	// 0-clear
	resizeTable(Table, 0, 0);
	resizeTable(Table, WH, WH);

	for(x = 0; x < WH; x++)
	for(y = 0; y < WH; y++)
	{
		if(mt19937_rnd(2))
		{
			Kick(x, y);
		}
	}
}
#if 0 // ng
static void Resolve(void)
{
	int x;
	int y;

	while(!IsAllZero())
	{
		switch(mt19937_rnd(4))
		{
		case 0: break;
		case 1: rot1Table(Table); break;
		case 2: rot2Table(Table); break;
		case 3: rot3Table(Table); break;

		default:
			error();
		}

		for(x = 1; x < WH; x++)
		for(y = 0; y < WH; y++)
		{
			if(getTableCell(Table, x - 1, y))
			{
				Kick(x, y);
			}
		}
	}
}
#elif 0 // ng
static void Resolve(void)
{
	int i;
	int x;
	int y;

	for(; ; )
	{
		PrintTable();

		if(IsAllZero())
			break;

		{
			autoList_t *pairs = createAutoList(WH * WH * 2);

			for(x = 0; x < WH; x++)
			for(y = 0; y < WH; y++)
			{
				if(getTableCell(Table, x, y))
				{
					addElement(pairs, x);
					addElement(pairs, y);
				}
			}
			for(i = 0; i < getCount(pairs); )
			{
				x = getElement(pairs, i++);
				y = getElement(pairs, i++);

				Kick(x, y);
			}
			releaseAutoList(pairs);
		}
	}
}
#else // ok
static void Resolve(void)
{
	int c;
	int i;
	int x;
	int y;

	for(; ; )
	{
		PrintTable();

		if(IsAllZero())
			break;

		for(c = mt19937_rnd(WH); c; c--)
		{
			autoList_t *pairs = createAutoList(WH * WH * 2);
//			autoList_t *pairs = newList();

			for(x = 0; x < WH; x++)
			for(y = 0; y < WH; y++)
			{
				if(getTableCell(Table, x, y))
				{
					addElement(pairs, x);
					addElement(pairs, y);
				}
			}
			for(i = 0; i < getCount(pairs); )
			{
				x = getElement(pairs, i++);
				y = getElement(pairs, i++);

				Kick(x, y);
			}
			releaseAutoList(pairs);
		}

		PrintTable();

		if(IsAllZero())
			break;

		switch(mt19937_rnd(3))
		{
		case 0: rot1Table(Table); break;
		case 1: rot2Table(Table); break;
		case 2: rot3Table(Table); break;

		default:
			error();
		}

		for(x = 1; x < WH; x++)
		for(y = 0; y < WH; y++)
		{
			if(getTableCell(Table, x - 1, y))
			{
				Kick(x, y);
			}
		}
	}
}
#endif
static void DoTest(void)
{
	LOGPOS();
	MkTable();
	LOGPOS();
	Resolve();
	LOGPOS();
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	Table = newTable(getZero, noop_u);

	while(!waitKey(0))
	{
		DoTest();
	}
}
