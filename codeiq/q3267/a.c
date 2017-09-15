#include <stdio.h>

static int IsUruuY(int y)
{
	return (y % 4 == 0 && y % 100 != 0) || y % 400 == 0;
}
static int GetYMDay(int y, int m)
{
	switch(m)
	{
	case 1: return 31;
	case 2: return IsUruuY(y) ? 29 : 28;
	case 3: return 31;
	case 4: return 30;
	case 5: return 31;
	case 6: return 30;
	case 7: return 31;
	case 8: return 31;
	case 9: return 30;
	case 10: return 31;
	case 11: return 30;
	case 12: return 31;
	}
	return -1; /* dummy */
}
static int IsPrime(int val)
{
	int n;

	if(val % 2 == 0)
		return 0;

	for(n = 3; n * n <= val; n += 2)
		if(val % n == 0)
			return 0;

	return 1;
}

static int Btwns[20000]; /* 1970-2019 near_eq 18300 days */
static int BtwnCnt;

static void MkBtwns(void)
{
	int y;
	int m;
	int d;
	int btwn = 0;

	for(y = 1970; y <= 2019; y++)
	for(m = 1; m <= 12; m++)
	{
		int ymDay = GetYMDay(y, m);

		for(d = 1; d <= ymDay; d++)
		{
			int date = y * 10000 + m * 100 + d;

			if(IsPrime(date))
			{
				Btwns[BtwnCnt] = btwn;
				BtwnCnt++;

				btwn = 0;
			}
			else
				btwn++;
		}
	}
	Btwns[BtwnCnt] = btwn;
	BtwnCnt++;
}
static int GetMax(int v1, int v2)
{
	return v1 < v2 ? v2 : v1;
}
static int GetMaxSumSqBtwns(int n)
{
	int maxSum = 0;
	int i;
	int j;

	for(i = 0; i + n <= BtwnCnt; i++)
	{
		int sum = 0;

		for(j = 0; j < n; j++)
			sum += Btwns[i + j];

		maxSum = GetMax(maxSum, sum);
	}
	return maxSum;
}
int main(int argc, char **argv)
{
	int n;
	int ans;

	scanf("%d", &n);

	MkBtwns();
	ans = GetMaxSumSqBtwns(n + 1) + n;

	printf("%d\n", ans);
	return 0;
}
