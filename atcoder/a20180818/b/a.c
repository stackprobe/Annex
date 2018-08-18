#include "C:\Factory\Common\all.h"

static int GetFactorKindNum(int n)
{
	int ret = 0;
	int c;

	for(c = 3; c <= n; c += 2)
	{
		if(n % c == 0)
		{
			do
			{
				n /= c;
			}
			while(n % c == 0);

			ret++;
		}
	}
	errorCase(n != 1);
	return ret;
}
int main(int argc, char **argv)
{
	int ans = 0;
	int n;

	scanf("%d", &n);

	for(; n; n--)
		if(n % 2 && GetFactorKindNum(n) == 3)
			ans++;

	cout("%d\n", ans);
}
