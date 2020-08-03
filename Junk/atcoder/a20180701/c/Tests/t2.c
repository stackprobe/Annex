/*
	テスト版
*/

// use int

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Progress.h"

#define N_MIN 1
#define N_MAX 200000
#define A_MIN 1
#define A_MAX 1000000000

static __int64 GetSadness(int *aa, int n, int b)
{
	__int64 ret = 0;
	int i;

	for(i = 0; i < n; i++)
		ret += abs(aa[i] - (b + (i + 1)));

	return ret;
}
static int Search_BBS(int *aa, int n, int b, int bStep)
{
	for(; ; )
	{
		__int64 ans1 = GetSadness(aa, n, b + bStep * 2); // HACK -- 先の方
		__int64 ans2 = GetSadness(aa, n, b + bStep * 3); // HACK -- 先の方の先の方

		if(ans1 <= ans2) // ? ! 下降している。
			return b;

		b += bStep;
	}
}
static __int64 Search(int *aa, int n)
{
	int b = 0;
	int c;
	__int64 ans = _I64_MAX;

	b = Search_BBS(aa, n, b, 30000000);
	b = Search_BBS(aa, n, b, 1000000);
	b = Search_BBS(aa, n, b, 30000);
	b = Search_BBS(aa, n, b, 1000);
	b = Search_BBS(aa, n, b, 30);
	b = Search_BBS(aa, n, b, 5);

	b -= 10;

	for(c = 0; c < 100; c++)
	{
		__int64 d = GetSadness(aa, n, b);

		m_minim(ans, d);

		b++;
	}
	return ans;
}
int main(int argc, char **argv)
{
	int n;
	int aa[N_MAX];
	int i;
	__int64 ans;

	scanf("%d", &n);

	errorCase(n < N_MIN);
	errorCase(N_MAX < n);

	for(i = 0; i < n; i++)
	{
		scanf("%d", aa + i);

		errorCase(aa[i] < A_MIN);
		errorCase(A_MAX < aa[i]);
	}
	ans = Search(aa, n);

	cout("%I64d\n", ans);
}
