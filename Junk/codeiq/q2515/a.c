#include <stdio.h>

#define COIN_NUM 6
static int Coins[COIN_NUM] = { 500, 100, 50, 10, 5, 1 };

static int AnsCnt;

static void NextCoin(int n, int coin)
{
	if(COIN_NUM <= coin)
		return;

	while(0 < n)
	{
		NextCoin(n, coin + 1);
		n -= Coins[coin];
	}
	if(n == 0)
		AnsCnt++;
}
int main()
{
	int n;

	scanf("%d", &n);

	NextCoin(n, 0);

	printf("%d\n", AnsCnt);
	return 0;
}
