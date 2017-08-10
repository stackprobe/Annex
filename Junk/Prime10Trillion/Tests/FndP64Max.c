#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

static void ShowFactors(uint64 value)
{
	uint64 factors[64];
	uint index;

	Factorization(value, factors);

	for(index = 0; factors[index] != 0; index++)
		cout("\t%I64u", factors[index]);

	cout("\n");
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
			ShowFactors(value);
			break;
		}
		ShowFactors(value);
		value--;
	}
}
