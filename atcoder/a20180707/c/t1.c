// use int

#include "C:\Factory\Common\all.h"

#define M_MAX 1000

int main(int argc, char **argv)
{
	int n;
	int m;
	int d;
	int aa[M_MAX];
	int ai;
	__int64 numer;
	__int64 denom;
	int i;

	scanf("%d", &n);
	scanf("%d", &m);
	scanf("%d", &d);

	errorCase(d < 0 || 1000000000 < d);
	errorCase(n < 0 || 1000000000 < n);
	errorCase(n <= d);
	errorCase(m < 2 || 1000000000 < m);

	// ----

	errorCase(M_MAX < m);

	ai = 0;
	numer = 0;
	denom = 0;

ahead:
	if(ai < m)
	{
		aa[ai] = 1;
		ai++;
		goto ahead;
	}
	ai = m - 1; // ai--;

	for(i = 1; i < m; i++)
		if(abs(aa[i - 1] - aa[i]) == d)
			numer++;

	denom++;

back:
	if(aa[ai] < n)
	{
		aa[ai]++;
		ai++;
		goto ahead;
	}
	if(0 < ai)
	{
		ai--;
		goto back;
	}

	cout("%.10f\n", (double)numer / denom);
}
