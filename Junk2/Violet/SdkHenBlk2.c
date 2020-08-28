/*
	‰½‚©’x‚¢H
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static char *CellChrs = "__™š›œŸ ¡";

static int Map[9][9];

static void PrintMap(void)
{
	int x;
	int y;

	cout("----\n");

	for(y = 0; y < 9; y++)
	{
		for(x = 0; x < 9; x++)
		{
			int g = Map[x][y];

			cout("%c%c", CellChrs[g * 2], CellChrs[g * 2 + 1]);
		}
		cout("\n");
	}
	cout("----\n");
}

autoList_t *G1Xs;
autoList_t *G1Ys;
autoList_t *G2Xs;
autoList_t *G2Ys;

static int IsInside(int x, int y)
{
	return
		m_isRange(x, 0, 9 - 1) &&
		m_isRange(y, 0, 9 - 1);
}
static int IsCell_G(int g, int x, int y)
{
	return IsInside(x, y) && Map[x][y] == g;
}
static void Find_G_XYs(int g1, int g2, autoList_t *gXs, autoList_t *gYs)
{
	int x;
	int y;

	for(x = 0; x < 9; x++)
	for(y = 0; y < 9; y++)
	{
		if(Map[x][y] == g1)
		if(
			IsCell_G(g2, x - 1, y    ) ||
			IsCell_G(g2, x,     y - 1) ||
			IsCell_G(g2, x + 1, y    ) ||
			IsCell_G(g2, x,     y + 1)
			)
		{
			addElement(gXs, x);
			addElement(gYs, y);
		}
	}
}

static int FX;
static int FY;

static int Find_XY_G(int g)
{
	int x;
	int y;

	for(x = 0; x < 9; x++)
	for(y = 0; y < 9; y++)
	{
		if(Map[x][y] == g)
		{
			FX = x;
			FY = y;
			return 1;
		}
	}
	return 0;
}
static void Fill_G(int x, int y, int gR, int gW)
{
	if(IsInside(x, y) && Map[x][y] == gR)
	{
		Map[x][y] = gW;

		Fill_G(x - 1, y,     gR, gW);
		Fill_G(x,     y - 1, gR, gW);
		Fill_G(x + 1, y,     gR, gW);
		Fill_G(x,     y + 1, gR, gW);
	}
}
static void Change_G(int gR, int gW)
{
	int x;
	int y;

	for(x = 0; x < 9; x++)
	for(y = 0; y < 9; y++)
	{
		if(Map[x][y] == gR)
			Map[x][y] = gW;
	}
}
static int IsBundan_G(int g)
{
	int ret;

	errorCase(!Find_XY_G(g));

	Fill_G(FX, FY, g, -1);
	ret = Find_XY_G(g);
	Change_G(-1, g);
	return ret;
}
static int IsBundan(void)
{
	int g;

	for(g = 1; g <= 9; g++)
		if(IsBundan_G(g))
			return 1;

	return 0;
}
static void HM_Main(int g1, int g2)
{
	int i1;
	int i2;

	setCount(G1Xs, 0);
	setCount(G1Ys, 0);
	setCount(G2Xs, 0);
	setCount(G2Ys, 0);

	Find_G_XYs(g1, g2, G1Xs, G1Ys);
	Find_G_XYs(g2, g1, G2Xs, G2Ys);

	if(!getCount(G1Xs) || !getCount(G2Xs))
		return;

	i1 = mt19937_rnd(getCount(G1Xs));
	i2 = mt19937_rnd(getCount(G2Xs));

	Map[getElement(G1Xs, i1)][getElement(G1Ys, i1)] = g2;
	Map[getElement(G2Xs, i2)][getElement(G2Ys, i2)] = g1;

	if(IsBundan()) // ? •ª’f‚µ‚½B-> –ß‚·B
	{
		Map[getElement(G1Xs, i1)][getElement(G1Ys, i1)] = g1;
		Map[getElement(G2Xs, i2)][getElement(G2Ys, i2)] = g2;
	}
}
static void HenkeiMap(void)
{
	int c = mt19937_rnd(9) + 1;
	int d = mt19937_rnd(9) + 1;

	if(c != d)
	{
		HM_Main(c, d);
	}
}

int main(int argc, char **argv)
{
	int x;
	int y;
	int c;

	mt19937_initCRnd();

	for(x = 0; x < 9; x++)
	for(y = 0; y < 9; y++)
	{
		Map[x][y] = 1 + (x / 3) + (y / 3) * 3;
	}

	G1Xs = newList();
	G1Ys = newList();
	G2Xs = newList();
	G2Ys = newList();

	PrintMap();

#define C_MAX 100

	for(c = 0; c < C_MAX; c++)
	{
		cmdTitle_x(xcout("SdkHenBlk - %d", c));

		HenkeiMap();
	}
	cmdTitle("SdkHenBlk");

	PrintMap();
}
