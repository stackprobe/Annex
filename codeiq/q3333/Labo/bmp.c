#include "C:\Factory\Common\all.h"
#include "C:\Factory\SubTools\libs\bmp.h"

// ---- copp ---

#define SZ_MAX 3000

static char Board[SZ_MAX][SZ_MAX];
static int Sz;
static int Hi;

static void GenBoard(void)
{
	int x;
	int y;
	int r;
	int max;
	int i;
	int r0;
	int r1;
	int r2;
	int r3;
	int r4;
	int sqrX;
	int sqrY;
	int sqrW;
	int sqrH;
	int brdH;

	for(x = 0; x < Sz; x++)
	for(y = 0; y < Sz; y++)
	{
		Board[x][y] = 1;
	}
	r = 1;
	max = Sz < 100 ? Sz : 100;

	for(i = 0; i < max; i++)
	{
		r0 = r = (r % 10009) * 99991;
		r1 = r = (r % 10009) * 99991;
		r2 = r = (r % 10009) * 99991;
		r3 = r = (r % 10009) * 99991;
		r4 = r = (r % 10009) * 99991;

		sqrX = r0 % Sz;
		sqrY = r1 % Sz;
		sqrW = (r2 % (Sz - sqrX)) % 100;
		sqrH = (r3 % (Sz - sqrY)) % 100;
		brdH = (r4 % Hi) + 1;

		for(x = 0; x < sqrW; x++)
		for(y = 0; y < sqrH; y++)
		{
			Board[sqrX + x][sqrY + y] = brdH;
		}
	}
}

// ----

static uint Colors[] =
{
	0,
	0x000000,
	0x0000ff,
	0x00ff00,
	0x00ffff,
	0xff0000,
	0xff00ff,
	0xffff00,
	0xffffff,
	0x808080,
};

static MkBmp(int sz, int hi)
{
	autoList_t *bmp = newList();
	uint rr;
	uint cc;

	Sz = sz;
	Hi = hi;

	cout("%d %d\n", Sz, Hi);
	LOGPOS();
	GenBoard();
	LOGPOS();

	for(rr = 0; rr < Sz; rr++)
	{
		autoList_t *row = newList();

		for(cc = 0; cc < Sz; cc++)
		{
			addElement(row, Colors[Board[cc][rr]]);
		}
		addElement(bmp, (uint)row);
	}
	writeBMPFile(getOutFile_x(xcout("%d_%d.bmp", sz, hi)), bmp);

	releaseDim_BR(bmp, 2, NULL);
}
static void Go(int sz)
{
	MkBmp(sz, 2);
	MkBmp(sz, 5);
	MkBmp(sz, 9);
}
int main(int argc, char **argv)
{
	int sz;
	int hi;

	Go(100);
	Go(300);
	Go(1000);
	Go(3000);

	MkBmp(14, 4);

	openOutDir();
}
