#include <stdio.h>

#define SZ_MAX 3000

static char Board[SZ_MAX][SZ_MAX];
static int Sz;
static int Hi;

static int GetMin(int a, int b)
{
	return a < b ? a : b;
}
static int GetMax(int a, int b)
{
	return a < b ? b : a;
}
static void GenBoard(void)
{
	int x;
	int y;
	int r;
	int max;
	int i;
	int r0;
	int r1;
	int r2;
	int r3;
	int r4;
	int sqrX;
	int sqrY;
	int sqrW;
	int sqrH;
	int brdH;

	for(x = 0; x < Sz; x++)
	for(y = 0; y < Sz; y++)
	{
		Board[x][y] = 1;
	}
	r = 1;
	max = Sz < 100 ? Sz : 100;

	for(i = 0; i < max; i++)
	{
		r0 = r = (r % 10009) * 99991;
		r1 = r = (r % 10009) * 99991;
		r2 = r = (r % 10009) * 99991;
		r3 = r = (r % 10009) * 99991;
		r4 = r = (r % 10009) * 99991;

		sqrX = r0 % Sz;
		sqrY = r1 % Sz;
		sqrW = (r2 % (Sz - sqrX)) % 100;
		sqrH = (r3 % (Sz - sqrY)) % 100;
		brdH = (r4 % Hi) + 1;

		for(x = 0; x < sqrW; x++)
		for(y = 0; y < sqrH; y++)
		{
			Board[sqrX + x][sqrY + y] = brdH;
		}
	}
}

// スパン == 自分と同じ高さが自分を含めて幾つ続くか
static short XSpns[SZ_MAX][SZ_MAX]; // その位置から右方向へのスパン
static short YSpns[SZ_MAX][SZ_MAX]; // その位置から下方向へのスパン

static int Search(void)
{
	int ans = 0;
	int x;
	int y;

	for(y = 0; y < Sz; y++)
	{
		XSpns[Sz - 1][y] = 1;

		for(x = Sz - 2; 0 <= x; x--)
		{
			if(Board[x][y] == Board[x + 1][y])
				XSpns[x][y] = XSpns[x + 1][y] + 1;
			else
				XSpns[x][y] = 1;
		}
	}
	for(x = 0; x < Sz; x++)
	{
		YSpns[x][Sz - 1] = 1;

		for(y = Sz - 2; 0 <= y; y--)
		{
			if(Board[x][y] == Board[x][y + 1])
				YSpns[x][y] = YSpns[x][y + 1] + 1;
			else
				YSpns[x][y] = 1;
		}
	}
	for(x = 0; x < Sz; x++)
	for(y = 0; y < Sz; y++)
	{
		int h = GetMax(XSpns[x][y], YSpns[x][y]);

		if(ans < h)
		{
			int i;

			for(i = 1; i < h; i++)
			{
				h = GetMin(h, XSpns[x][y + i]);
				h = GetMin(h, YSpns[x + i][y]);

				if(h <= ans)
					break;
			}
			ans = GetMax(ans, i);
		}
	}
	ans *= ans;
	return ans;
}
int main()
{
	while(scanf("%d,%d", &Sz, &Hi) != EOF)
	{
		GenBoard();
		printf("%d\n", Search());
	}
}
