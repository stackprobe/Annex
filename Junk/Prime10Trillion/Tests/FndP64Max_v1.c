#include "C:\Factory\Common\all.h"

static int IsPrime(uint64 value)
{
	uint denom;
	uint maxDenom = iSqrt64(value);

	if(value < 2)
		return 0;

	if(value == 2)
		return 1;

	if(value % 2 == 0)
	{
		cout("DIVIDABLE: 2\n");
		return 0;
	}
	for(denom = 3; denom <= maxDenom; denom += 2)
	{
		if(value % denom == 0)
		{
			cout("DIVIDABLE: %u\n", denom);
			return 0;
		}
	}
	return 1;
}
int main(int argc, char **argv)
{
	uint64 value = UINT64MAX;

	for(; ; )
	{
		cout("%I64u\n", value);

		if(IsPrime(value))
		{
			cout("FOUND!\n");
			break;
		}
		value--;
	}
}
