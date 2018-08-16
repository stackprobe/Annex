#include "C:\Factory\Common\all.h"

static uint Func01(uint c, uint n)
{
	uint ans;
	uint i;

	if(!n)
		return 1;

	ans = 0;

	for(i = 0; i <= c; i++)
		ans += Func01(c - i, n - 1);

	return ans;
}
int main(int argc, char **argv)
{
	uint c;

	for(c = 1; c <= 100; c++)
	{
		cout("%u => %u\n", c, Func01(c, 3));
	}
}
