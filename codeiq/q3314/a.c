#include <stdio.h>

#define WOOD_MAX 60

typedef long long I64t;

static I64t Cache[WOOD_MAX][WOOD_MAX];

static int GetMin(int a, int b)
{
	return a < b ? a : b;
}
static void CacheClear(void)
{
	int i;
	int j;

	for(i = 0; i < WOOD_MAX; i++)
	for(j = 0; j < WOOD_MAX; j++)
	{
		Cache[i][j] = -1;
	}
}

I64t Search(int hi, int rem);

static I64t Search_NC(int hi, int rem)
{
	I64t ans = 0;
	int nextHiMax = GetMin(hi + 1, rem);
	int nextHi;

	for(nextHi = 1; nextHi <= nextHiMax; nextHi++)
		ans += Search(nextHi, rem - nextHi);

	return ans;
}
static I64t Search(int hi, int rem)
{
	if(!rem)
		return 1;

	if(Cache[hi][rem] == -1)
		Cache[hi][rem] = Search_NC(hi, rem);

	return Cache[hi][rem];
}
static I64t GetAns(int wood)
{
	return Search(1, wood - 1);
}
int main()
{
	int wood;
	I64t ans;

	CacheClear();

	scanf("%d", &wood);

	ans = GetAns(wood);

	printf("%lld\n", ans);
}
