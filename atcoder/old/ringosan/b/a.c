#include <stdio.h>
#include <stdlib.h>

#define P_MAX 200000

static int N;
static int Ds[P_MAX];
static int M;
static int Ts[P_MAX];

static int IntComp(const void *a, const void *b)
{
	return *(int *)a - *(int *)b;
}
static int NextLv(int *vs, int vc, int i, int *pv, int *pc)
{
	int b = i;

	while(vs[b] == vs[++i] && i < vc);

	*pv = vs[b];
	*pc = i - b;

	return i;
}
int main()
{
	char *ans = "YES";
	int i;
	int di;
	int dv;
	int dc;
	int ti;
	int tv;
	int tc;

	scanf("%d", &N);

	for(i = 0; i < N; i++)
		scanf("%d", Ds + i);

	scanf("%d", &M);

	for(i = 0; i < M; i++)
		scanf("%d", Ts + i);

	qsort(Ds, N, sizeof(int), IntComp);
	qsort(Ts, M, sizeof(int), IntComp);

	di = 0;
	ti = 0;

	while(di < N && ti < M)
	{
		di = NextLv(Ds, N, di, &dv, &dc);

		if(dv < Ts[ti])
			continue;

		ti = NextLv(Ts, M, ti, &tv, &tc);

		if(dv != tv || dc < tc)
		{
			ans = "NO";
			break;
		}
	}
	printf("%s\n", ans);
}
