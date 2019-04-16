// use int

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\autoTable.h"

#define WH 5

static autoTable_t *Table;
static autoTable_t *OneTable;

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

static int IO_X;
static int IO_Y;

static int IsOne(void)
{
	int c = 0;
	int x;
	int y;

	for(x = 0; x < WH; x++)
	for(y = 0; y < WH; y++)
	{
		if(getTableCell(Table, x, y))
		{
			IO_X = x;
			IO_Y = y;
			c++;
		}
	}
	return c == 1;
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
static void Search_Ended(void)
{
	if(IsOne())
	{
		setTableCell(OneTable, IO_X, IO_Y, 1);
	}
	PrintTable();
}
static void Search(int x, int y)
{
	int nx;
	int ny;

	if(y == WH)
	{
		Search_Ended();
		return;
	}
	if(x + 1 == WH)
	{
		nx = 0;
		ny = y + 1;
	}
	else
	{
		nx = x + 1;
		ny = y;
	}
	Search(nx, ny);
	Kick(x, y);
	Search(nx, ny);
	Kick(x, y);
}
static void DoTest(void)
{
	int x;
	int y;

	Search(0, 0);

	for(y = 0; y < WH; y++)
	{
		for(x = 0; x < WH; x++)
		{
			cout("%c", getTableCell(OneTable, x, y) ? '@' : '-');
		}
		cout("\n");
	}
}
int main(int argc, char **argv)
{
	Table    = newTable(getZero, noop_u);
	OneTable = newTable(getZero, noop_u);

	resizeTable(Table,    WH, WH);
	resizeTable(OneTable, WH, WH);

	DoTest();
}
