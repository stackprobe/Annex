// use int

#include "C:\Factory\Common\all.h"

#define N_MIN 4
#define N_MAX 200000
#define A_MIN 1
#define A_MAX 1000000000

static void CheckSums(__int64 sums[4], __int64 *pAns)
{
	__int64 l = sums[0];
	__int64 h = sums[0];
	int i;

	for(i = 1; i < 4; i++)
	{
		m_minim(l, sums[i]);
		m_maxim(h, sums[i]);
	}
	h -= l;
	m_minim(*pAns, h);
}
static void Cut(int *aa, int n, int sumi, __int64 sums[4], __int64 *pAns)
{
	int i;

	sums[sumi] = 0;

	if(sumi < 3)
	{
		for(i = 0; i < n; i++)
		{
			sums[sumi] += aa[i];
			Cut(aa + (i + 1), n - (i + 1), sumi + 1, sums, pAns);
		}
	}
	else if(n)
	{
		for(i = 0; i < n; i++)
			sums[sumi] += aa[i];

		CheckSums(sums, pAns);
	}
}
static __int64 Search(int *aa, int n)
{
	__int64 sums[4];
	__int64 ans = _I64_MAX;

	Cut(aa, n, 0, sums, &ans);

	return ans;
}
int main(int argc, char **argv)
{
	int n;
	int aa[N_MAX];
	int i;
	__int64 ans;

	scanf("%d", &n);
	errorCase(!m_isRange(n, N_MIN, N_MAX));

	for(i = 0; i < n; i++)
	{
		scanf("%d", aa + i);
		errorCase(!m_isRange(aa[i], A_MIN, A_MAX));
	}
	ans = Search(aa, n);

	cout("%I64d\n", ans);
}
