#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc2.h"
#include "C:\Factory\Common\Options\csv.h"

static int Compare_Sym(char *str1, char *str2)
{
	char *ans = calc(str1, '-', str2);

	if(ans[0] == '-')
		return '<';

	if(!strcmp(ans, "0"))
		return '=';

	return '>';
}
int main(int argc, char **argv)
{
	autoList_t *rows = readCSVFileTR(nextArg());
	autoList_t *row;
	uint rowidx;
	char *lSum = strx("0");
	char *rSum = strx("0");

	foreach(rows, row, rowidx)
	{
		lSum = calc_xc(lSum, '+', getLine(row, 0));
		rSum = calc_xc(rSum, '+', getLine(row, 1));
//cout("%s %s\n", lSum, rSum); // test
	}
	cout("%s %c %s\n", lSum, Compare_Sym(lSum, rSum), rSum);

	memFree(lSum);
	memFree(rSum);
}
