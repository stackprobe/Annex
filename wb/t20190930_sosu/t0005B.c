#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

typedef unsigned __int32 uint;
typedef unsigned __int64 uint64;

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
static int IsPrime(uint n)
{
	static uint a[] = { 2, 7, 61 };
	uint d;
	uint x;
	uint r;
	uint s;
	uint t;

	if(n <= 1)
		return 0;

	if(n <= 3 || n == 7 || n == 61)
		return 1;

	if(!(n & 1))
		return 0;

	d = n;

	for(r = 0; !((d >>= 1) & 1); r++);

	for(t = 0; t < 3; t++)
	{
		x = ModPow(a[t], d, n);

		if(x != 1 && x != n - 1)
		{
			for(s = r; ; s--)
			{
				if(!s)
					return 0;

				x = ModPow(x, 2, n);

				if(x == n - 1)
					break;
			}
		}
	}
	return 1;
}
int main()
{
	uint c;

	for(c = 0; c < UINT_MAX; c++)
		if(IsPrime(c))
			printf("%u\n", c);
}
