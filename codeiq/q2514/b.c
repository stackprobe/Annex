#include "C:\Factory\Common\all.h"

static int GetBitCount(int v)
{
	int ret = 0;
	int b;

	for(b = 0; b < 31; b++)
		if(v & (1 << b))
			ret++;

	return ret;
}
int main(int argc, char **argv)
{
	int n;
	int m;
	int v;
	int anscnt = 0;

	scanf("%d", &n);
	scanf("%d", &m);

	for(v = 0; v <= n; v++)
		if(GetBitCount(v) == m)
			anscnt++;

	printf("%d\n", anscnt);
}
