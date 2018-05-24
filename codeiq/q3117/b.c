/*
	テスト用
*/

#include "C:\Factory\Common\all.h"

static int GetAns_a(int n)
{
	return n % 2 ? (1999999 / n + 1) / 2 : 0;
}
static int GetAns_test(int n)
{
	int i;
	int ans = 0;

	for(i = 1; i <= 999999; i++)
	{
		int sum = i + (i + 1);

		if(sum % n == 0)
			ans++;
	}
	return ans;
}
int main(int argc, char **argv)
{
	int n;

	for(n = 2; n <= 1000; n++)
	{
		cout("%d\n", n);
		errorCase(GetAns_a(n) != GetAns_test(n));
	}
	cout("OK!\n");
}
