#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc.h"

#define BASEMENT 6

static char *Permutation(uint n, uint m)
{
	char *ans = strx("1");

	while(m)
	{
		ans = calcLine_xx(ans, '*', xcout("%u", n), 10, BASEMENT);
		n--;
		m--;
	}
	return ans;
}
int main(int argc, char **argv)
{
	uint m;

	hasArgs(0); // for ReadSysArgs()

	for(m = 1; m <= 2015 && !waitKey(0); m++)
	{
		char *numer = Permutation(2015, m);
		char *denom = Permutation(m, m);
		char *ans;
		int cEven;

		ans = calcLine_xx(numer, '/', denom, 10, BASEMENT);

		if(calcLastMarume)
			ans = calcLineToMarume_x(ans, BASEMENT);

		// set cEven
		{
			cEven = ' ';

			if(!calcLastMarume)
			{
				char *tmp = calcLine(ans, '/', "2", 10, 0);

				if(!calcLastMarume)
					cEven = 'E';

				memFree(tmp);
			}
		}

		cout("[%c] C(2015, %u) = %s\n", cEven, m, ans);

		memFree(ans);
	}
}
