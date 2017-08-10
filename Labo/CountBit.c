#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"

static uint BitCount(uint64 value)
{
	uint64 bit;
	uint count = 0;

	for(bit = 1ui64 << 63; bit; bit >>= 1)
		if(value & bit)
			count++;

	return count;
}
int main(int argc, char **argv)
{
	uint64 value = getCryptoRand64();
	uint count = 0;

	cout("%016I64x\n", value);
	cout("%u\n", BitCount(value));

	while(value != 0)
	{
		value ^= value & (~value + 1);
		count++;
	}
	cout("%u\n", count);
}
