#include <stdio.h>
#include <string.h>

typedef long long i64_t;

static void RevStr(char *p, char *q)
{
	while(p < q)
	{
		int tmp = *p;

		*p++ = *q;
		*q-- = tmp;
	}
}
static int IsTZR(char *p, char *q)
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
		while(*p == d);

		if(q[1] && q < p)
			break;

		if(c <= lastCnt)
			return 0;

		lastCnt = c;
	}
	return 1;
}

static char Bin[35];

static void I64ToBin(i64_t value)
{
	char *p = Bin;

	while(value != 0)
	{
		*p++ = "01"[value % 2];
		value /= 2;
	}
	RevStr(Bin, p - 1);
	*p = '\0';
}
static i64_t BinToI64(void)
{
	i64_t value = 0;
	char *p;

	for(p = Bin; *p; p++)
	{
		value *= 2;
		value += *p - '0';
	}
	return value;
}
static void MakeTZR(void)
{
	char *p = strchr(Bin, '\0') - 1;

	for(; ; )
	{
		if(IsTZR(Bin, p))
		{
			p++;

			if(!*p)
				break;

			*p = '0';
		}
		else
		{
			while(*p == '1')
				p--;

			*p = '1';
		}
	}
}
int main()
{
	i64_t value;

	scanf("%lld", &value);

	I64ToBin(value + 1);
	MakeTZR();
	value = BinToI64();

	printf("%lld\n", value);
}
