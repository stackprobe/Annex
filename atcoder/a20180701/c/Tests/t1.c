/*
	テスト版

	超重い。
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
static __int64 Search(int *aa, int n)
{
	__int64 ans = _I64_MAX;
	int b;
	const int B_MIN = A_MIN - 100;
	const int B_MAX = A_MAX + 100;

	ProgressBegin();
	for(b = B_MIN; b <= B_MAX; b++)
	{
		__int64 d = GetSadness(aa, n, b);

		m_minim(ans, d);
		ProgressRate((b - B_MIN) * 1.0 / (B_MAX - B_MIN));
	}
	ProgressEnd(0);
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
