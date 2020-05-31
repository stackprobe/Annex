// use int

#include "C:\Factory\Common\all.h"

#define N_MIN 1
#define N_MAX 18
#define A_MIN 1
#define A_MAX 1000000000

static int Search(int *aa, int k)
{
	int i;
	int j;
	int ans = 0;

	for(i = 0; i < k; i++)
	for(j = i + 1; j <= k; j++)
	if((i | j) <= k)
	{
		int tmp = aa[i] + aa[j];

		m_maxim(ans, tmp);
	}
	return ans;
}
int main(int argc, char **argv)
{
	int n;
	int an;
	int *aa;
	int ai;
	int k;

	scanf("%d", &n);
	errorCase(!m_isRange(n, N_MIN, N_MAX));

	an = 2;

	while(--n)
		an *= 2;

	aa = (int *)memAlloc(an * sizeof(int));

	for(ai = 0; ai < an; ai++)
	{
		scanf("%d", aa + ai);
		errorCase(!m_isRange(aa[ai], A_MIN, A_MAX));
	}
	for(k = 1; k < an; k++)
	{
		int ans = Search(aa, k);

		cout("%d\n", ans);
	}
	memFree(aa);
}
