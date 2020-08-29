/*
	2 以上の整数 n で ( 2^n + 1 ) / n^2 が整数となるようなものを全て求めよ。

	- - -

	3 しか無いらしい。
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc.h"

int main(int argc, char **argv)
{
	uint n;

	// ついでに 1 も、
	for(n = 1; waitKey(0) != 0x1b; n++)
	{
		char *numer = calcLine_xc(calcPower("2", n, 10), '+', "1", 10, 0);
		char *denom = calcPower_x(xcout("%u", n), 2, 10);
		char *ans;

		ans = calcLine_xx(numer, '/', denom, 10, 0);

//		cout("[%c] %u\n", calcLastMarume ? ' ' : '*', n);

		cmdTitle_x(xcout("%u なう", n));

		if(!calcLastMarume)
			cout("%u\n", n);

		memFree(ans);
	}
}
