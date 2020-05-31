#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\bitTable.h"

static autoList_t *OutLines;
static bitTable_t *Map;

static void MoveCoin(int x, int y, int xD, int yD)
{
	addElement(OutLines, (uint)xcout("%d %d %d %d", y + 1, x + 1, yD + 1, xD + 1));

	invTableBit(Map, x, y);
	invTableBit(Map, xD, yD);
}
static void MoveCoin_IN(int x, int y, int xD, int yD)
{
	if(getTableBit(Map, x, y))
	{
		MoveCoin(x, y, xD, yD);
	}
}
int main(int argc, char **argv)
{
	int h;
	int w;
	int x;
	int y;

	OutLines = newList();

	scanf("%d", &h);
	scanf("%d", &w);

	Map = newBitTable(w, h);

	for(y = 0; y < h; y++)
	for(x = 0; x < w; x++)
	{
		int c;

		scanf("%d", &c);

		setTableBit(Map, x, y, c & 1);
	}

	for(x = 0; x < w; x++)
	for(y = h - 1; y; y--)
	{
		MoveCoin_IN(x, y, x, y - 1);
	}

	for(x = w - 1; x; x--)
	{
		MoveCoin_IN(x, 0, x - 1, 0);
	}

	cout("%d\n", (int)getCount(OutLines));

	{
		char *line;
		int i;

		foreach(OutLines, line, i)
			cout("%s\n", line);
	}

#if 0 // test
	for(y = 0; y < h; y++)
	{
		for(x = 0; x < w; x++)
			cout("%d", getTableBit(Map, x, y));

		cout("\n");
	}
#endif
}
