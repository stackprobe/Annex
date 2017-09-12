#include <stdio.h>
#include <stdlib.h>
#include <limits.h>
#include <malloc.h>

static int GetMin(int a, int b)
{
	return a < b ? a : b;
}

static int N;
static int M;

static int *Ps;
static int *Is;
static int *Ss;

static int Search(void)
{
	int ans = INT_MAX;
	int p = 0;
	int n = 0;
	int ahead = 1;
	int i = 0;

	for(; ; )
	{
		if(ahead)
		{
			if(M <= p)
			{
				ans = GetMin(ans, n);
				goto goBack;
			}
			if(ans < n || N <= i)
				goto goBack;

			Ss[i] = 0;
		}
		else
			Ss[i]++;

		if(Ss[i] == 0)
		{
			p += Ps[i];
			n += Is[i];
			ahead = 1;
			i++;
		}
		else if(Ss[i] == 1)
		{
			ahead = 1;
			i++;
		}
		else
		{
		goBack:
			if(!i)
				break;

			ahead = 0;
			i--;

			if(!Ss[i])
			{
				p -= Ps[i];
				n -= Is[i];
			}
		}
	}
	return ans;
}
int CompPI(const void *a, const void *b)
{
	int aP = ((int *)a)[0];
	int aI = ((int *)a)[1];
	int bP = ((int *)b)[0];
	int bI = ((int *)b)[1];

	return bP * aI - aP * bI;
}
int main()
{
	int i;
	int ans;

	scanf("%d", &N);
	scanf("%d", &M);

	Ps = (int *)malloc(N * 4 * sizeof(int));
	Is = Ps + N;
	Ss = Ps + N * 2;

	for(i = 0; i < N; i++)
	{
		scanf("%d", Ss + i * 2 + 0); // P
		scanf("%d", Ss + i * 2 + 1); // I
	}
	qsort(Ss, N, 2 * sizeof(int), CompPI);

	for(i = 0; i < N; i++)
	{
		Ps[i] = Ss[i * 2 + 0];
		Is[i] = Ss[i * 2 + 1];

printf("P=%d I=%d, %f\n", Ps[i], Is[i], (double)Ps[i] / Is[i]); // test

	}
	ans = Search();

	printf("%d\n", ans);

	free(Ps);
}
