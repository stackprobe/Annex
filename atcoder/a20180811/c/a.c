#include "C:\Factory\Common\all.h"

static uint64 Add(uint64 a, uint64 b)
{
	uint64 x = a ^ b;
	uint64 c = a & b;

	if(c != 0ui64)
	{
		x = Add(x, c << 1);
		x = Add(x, c << 2);
	}
	return x;
}
static uint64 ToNeg2(sint64 n)
{
	uint64 a;
	uint64 b;

	if(n < 0)
	{
		a = (uint64)-n;
		b = (a & 0x5555555555555555ui64) << 1;
	}
	else
	{
		a = n;
		b = (a & 0xaaaaaaaaaaaaaaaaui64) << 1;
	}
	return Add(a, b);
}
int main()
{
	sint n;

	scanf("%d", &n);

	cout("%s\n", c_toLineValue64Digits(ToNeg2((sint64)n), binadecimal));
}
