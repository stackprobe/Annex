#include <stdio.h>

#define PLAYER_MAX 12
#define AVG_GOSA_MARGIN 0.0000000001 // 1 / 10^10

#define IsDigit(c) \
	('0' <= (c) && (c) <= '9')

static int Ch = ' ';

static enum RB_Ret_t
{
	RB_BLANKS,
	RB_EOF,
	RB_NEW_LINE,
}
ReadBlanks(void)
{
	int ret = RB_BLANKS;

	while(!IsDigit(Ch))
	{
		if(Ch == EOF)
			return RB_EOF;

		if(Ch == '\r' || Ch == '\n')
			ret = RB_NEW_LINE;

		Ch = fgetc(stdin);
	}
	return ret;
}
static int ReadValue(void)
{
	int ret = 0;

	while(IsDigit(Ch))
	{
		ret *= 10;
		ret += Ch - '0';

		Ch = fgetc(stdin);
	}
	return ret;
}
int main()
{
	double avgs[PLAYER_MAX];
	int c = 0;
	int n = 0;
	double d = 0.0;
	int i;
	int j;

	for(; ; )
	{
		int ret = ReadBlanks();

		if(ret != RB_BLANKS)
		{
			if(n)
			{
				avgs[c++] = n / d;
				n = 0;
				d = 0.0;
			}
			if(ret == RB_EOF)
				break;
		}
		n++;
		d += 1.0 / ReadValue();
	}
	for(i = 0; i < c; i++)
	{
		int rank = 0;

		for(j = 0; j < c; j++)
			if(avgs[j] <= avgs[i] + AVG_GOSA_MARGIN)
				rank++;

		printf("%d\n", rank);
	}
}
