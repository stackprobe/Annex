#include <stdio.h>
#include <ctype.h>

static __int64 NextNum(FILE *fp)
{
	__int64 num = 0;

	for(; ; )
	{
		int chr = fgetc(fp);

		if(chr == '\n' || chr == EOF)
			break;

		if(isdigit(chr))
		{
			num *= 10;
			num += chr - '0';
		}
	}
	return num;
}
static int GetFigure(__int64 num)
{
	int bound = 1;
	__int64 sVal = 1;

	num += 3;

	while(bound * sVal * 9 <= num)
	{
		num -= bound * sVal * 9;
		bound++;
		sVal *= 10;
	}
	sVal += num / bound;
	num %= bound;

	while(num < bound - 1)
	{
		sVal /= 10;
		num++;
	}
	return (int)sVal % 10;
}
int main(int argc, char **argv)
{
	FILE *rfp;
	FILE *wfp;
	__int64 num;

	if(argc != 3)
		return 1;

	rfp = fopen(argv[1], "rt");

	if(!rfp)
		return 1;

	wfp = fopen(argv[2], "wt");

	if(!wfp)
	{
		fclose(wfp);
		return 1;
	}
	while(1 <= (num = NextNum(rfp)))
	{
		fprintf(wfp, "%d\n", GetFigure(num));
	}
	fclose(rfp);
	fclose(wfp);
	return 0;
}
