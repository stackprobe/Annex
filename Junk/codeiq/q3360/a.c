#include <stdio.h>

static int Map[3][3];
static int Ans;

#define IsWonLn(x1, y1, x2, y2, x3, y3) \
	(Map[x1][y1] & Map[x2][y2] & Map[x3][y3])
//	(Map[x1][y1] && Map[x1][y1] == Map[x2][y2] && Map[x1][y1] == Map[x3][y3])

#define IsWonLn2(base, step) \
	(Map[0][base] & Map[0][base + step] & Map[0][base + step * 2])

static int IsWon(void)
{
	return
		/*
		IsWonLn(0, 0, 0, 1, 0, 2) ||
		IsWonLn(1, 0, 1, 1, 1, 2) ||
		IsWonLn(2, 0, 2, 1, 2, 2) ||
		IsWonLn(0, 0, 1, 0, 2, 0) ||
		IsWonLn(0, 1, 1, 1, 2, 1) ||
		IsWonLn(0, 2, 1, 2, 2, 2) ||
		IsWonLn(0, 0, 1, 1, 2, 2) ||
		IsWonLn(0, 2, 1, 1, 2, 0);
		*/
		IsWonLn2(0, 1) ||
		IsWonLn2(3, 1) ||
		IsWonLn2(6, 1) ||
		IsWonLn2(0, 3) ||
		IsWonLn2(1, 3) ||
		IsWonLn2(2, 3) ||
		IsWonLn2(0, 4) ||
		IsWonLn2(2, 2);
}
static void Search(int n, int turn)
{
	int x;
	int y;

	for(x = 0; x < 3; x++)
	for(y = 0; y < 3; y++)
	{
		if(!Map[x][y])
		{
			Map[x][y] = turn;

			if(IsWon())
			{
				if(n == 1)
					Ans++;
			}
			else
			{
				if(2 <= n)
					Search(n - 1, 3 - turn);
			}
			Map[x][y] = 0;
		}
	}
}
int main()
{
	int n;

	scanf("%d", &n);

	Search(n, 1);

	printf("%d\n", Ans);
}
