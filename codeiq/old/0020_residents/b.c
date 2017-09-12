#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csv.h"
#include "C:\Factory\Common\Options\Calc2.h"

static char *GetDistanceP2(char *x1, char *y1, char *x2, char *y2)
{
	char *x = calc(x1, '-', x2);
	char *y = calc(y1, '-', y2);
	char *dx;
	char *dy;
	char *dp2;

	dx = calc(x, '*', x);
	dy = calc(y, '*', y);
	dp2 = calc(dx, '+', dy);

	memFree(x);
	memFree(y);
	memFree(dx);
	memFree(dy);
	return dp2;
}
static int Compare(char *a, char *b)
{
	calcOperand_t *co1 = calcFromString(a);
	calcOperand_t *co2 = calcFromString(b);
	int retval;

	retval = calcComp(co1, co2);

	calcRelease(co1);
	calcRelease(co2);
	return retval;
}
static void CheckNearest(char *rx, char *ry, autoList_t *answerTbl)
{
	char *np2 = NULL;
	uint ni = UINTMAX;
	autoList_t *row;
	uint rowidx;

	foreach(answerTbl, row, rowidx)
	{
		char *sx = getLine(row, 0);
		char *sy = getLine(row, 1);
		char *dp2;

		dp2 = GetDistanceP2(rx, ry, sx, sy);

		if(!np2 || Compare(np2, dp2) == 1)
		{
			memFree(np2);
			np2 = strx(dp2);
			ni = rowidx;
		}
		memFree(dp2);
	}

	cout("%s, %s -> %s [%u]\n", rx, ry, np2, ni);

	memFree(np2);
}

int main(int argc, char **argv)
{
	autoList_t *answerTbl;
	autoList_t *residentsTbl;
	autoList_t *row;
	uint rowidx;

	answerTbl = readCSVFileTR(nextArg());
	residentsTbl = readCSVFileTR(nextArg());

	foreach(residentsTbl, row, rowidx)
	{
		char *rx = getLine(row, 0);
		char *ry = getLine(row, 1);

		CheckNearest(rx, ry, answerTbl);
	}
	releaseDim(answerTbl, 2);
	releaseDim(residentsTbl, 2);
}
