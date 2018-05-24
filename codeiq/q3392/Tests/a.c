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
//cout("c: %d\n", c); // test

		if(c <= lastCnt)
			return 0;

		lastCnt = c;
	}
	return 1;
}
int main(int argc, char **argv)
{
	uint64 value = argIs("/B") ? toValue64Digits(nextArg(), binadecimal) : toValue64(nextArg());

//	value++; // ‚æ‚è‘å‚«‚È

#if 1
	{
		char *sVal = ToStrBin(value);

		cout("%I64u %s -> %s\n", value, sVal, IsTZR(sVal) ? "TZR" : "not TZR");

		while(!IsTZR(sVal))
		{
			value++;
			memFree(sVal);
			sVal = ToStrBin(value);
		}
		cout("%I64u %s -> TZR\n", value, sVal, IsTZR(sVal));
	}
#else
	for(; ; )
	{
		char *sVal = ToStrBin(value);

		cout("%I64u %s -> ", value, sVal);

		if(IsTZR(sVal))
		{
			cout("TZR\n");
			break;
		}
		cout("not TZR\n");

		memFree(sVal);
		value++;
	}
#endif
}
