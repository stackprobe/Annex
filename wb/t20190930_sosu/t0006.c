#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

static uint ModPow(uint b, uint e, uint m)
{
	uint64 a = 1;

	for(; e; e >>= 1)
	{
		if(e & 1)
			a = (a * b) % m;

		b = ((uint64)b * b) % m;
	}
	return a;
}
static void FindLiar(uint n)
{
	uint d;
	uint x;
	uint r;
	uint s;
	uint t;
	uint liarCount;
	uint honestCount;

	if(n <= 1)
		error();

	if(n <= 3)
		error();

	if(!(n & 1))
		error();

	cout("%u\n", n);

	d = n;

	for(r = 0; !((d >>= 1) & 1); r++);

	liarCount = 0;
	honestCount = 0;

	for(t = 2; t <= n - 2; t++)
	{
		int primeFlag = 1;

		x = t;

		if(x == 1)
			goto endJudge;

		if(x == n - 1)
			goto endJudge;

		for(s = r; ; s--)
		{
			if(!s)
			{
				primeFlag = 0;
				break;
			}
			x = ModPow(x, 2, n);

			if(x == n - 1)
				break;
		}
	endJudge:

		if(primeFlag)
		{
			cout("\tliar %u\n", t);
			liarCount++;
		}
		else
			honestCount++;
	}
	cout("\t%u / %u == %.9f\n", liarCount, liarCount + honestCount, liarCount * 1.0 / (liarCount + honestCount));
}
int main()
{
	uint c;

	for(c = 5; c < UINT_MAX; c += 2)
	{
		if(!IsPrime(c))
		{
			FindLiar(c);
		}
	}
}
