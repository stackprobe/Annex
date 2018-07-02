// use int

#include "C:\Factory\Common\all.h"

#define N_MAX 200000

static int Comp(const void *p1, const void *p2)
{
	int a = *(int *)p1;
	int b = *(int *)p2;

	return m_simpleComp(a, b);
}
int main(int argc, char **argv)
{
	int n;
	int aa[N_MAX];
	int i;
	__int64 ans = 0;

	scanf("%d", &n);

	errorCase(n < 1);
	errorCase(N_MAX < n);

	for(i = 0; i < n; i++)
	{
		int a;

		scanf("%d", &a);

		aa[i] = a - (i + 1);
	}
	qsort(aa, n, sizeof(aa[0]), Comp);

	{
		int am = aa[n / 2]; // ’†‰›’l

		for(i = 0; i < n; i++)
			ans += abs(aa[i] - am);
	}

	cout("%I64d\n", ans);
}
