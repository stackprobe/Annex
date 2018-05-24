/*
	íxÇ¢ÅI
*/

#include <stdio.h>

typedef long long i64_t;

#define N_MAX 300000

static int N;
static int GCache[N_MAX + 1];
static int Footps[N_MAX + 1];

static int F(int x)
{
	return (4 * (i64_t)x * (N - x)) / N;
}
static void GMain(int k)
{
	int x;
	int i;
	int r;

	for(x = 0; x <= N; x++)
		Footps[x] = -1;

	x = k;
	i = 0;

	do
	{
		Footps[x] = i;
		x = F(x);
		i++;

		if(GCache[x])
		{
			i += GCache[x];
			goto tail;
		}
	}
	while(Footps[x] == -1);

	r = i - Footps[x];

	do
	{
		GCache[x] = r;
		x = F(x);
	}
	while(!GCache[x]);

	i = r + Footps[x];
tail:
	x = k;

	while(!GCache[x])
	{
		GCache[x] = i;
		x = F(x);
		i--;
	}
}
static int G(int k)
{
	if(!GCache[k])
		GMain(k);

	return GCache[k];
}
static int H(void)
{
	int sum = 0;
	int k;

	for(k = 0; k <= N; k++)
		sum += G(k);

	return sum;
}
int main()
{
	scanf("%d", &N);

	printf("%d\n", H());
}
