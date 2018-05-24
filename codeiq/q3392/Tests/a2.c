#include "C:\Factory\Common\all.h"

static char *ToStrBin(uint64 value)
{
	char *ret = strx("");

	while(value != 0)
	{
		ret = addChar(ret, "01"[(uint)(value % 2)]);
		value /= 2;
	}
	reverseLine(ret);
	return ret;
}
static int IsTZR(char *p)
{
	int lastCnt = 0;

	while(*p)
	{
		int c = 0;
		int d = *p;

		do
		{
			c++;
			p++;
		}
		while(d == *p);

		if(c <= lastCnt)
			return 0;

		lastCnt = c;
	}
	return 1;
}
int main(int argc, char **argv)
{
	uint64 value = toValue64(nextArg());

	value++;

	for(; ; )
	{
		char *sVal = ToStrBin(value);

		if(IsTZR(sVal))
			break;

		memFree(sVal);
		value++;
	}
	cout("%I64u\n", value);
}
