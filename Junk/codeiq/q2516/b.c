#include <stdio.h>
#include <limits.h>

#define W_MAX 64
#define H_MAX 64

static int W;
static int H;

enum
{
	FROM_2,
	FROM_4,
	FROM_6,
	FROM_8,
};

static struct
{
	int Wall;
	int Turns[4];
}
Map[W_MAX][H_MAX];

static int ReadCell(void)
{
	for(; ; )
	{
		int chr = fgetc(stdin);

		if(chr == '.')
			return 0;

		if(chr == '#')
			return 1;
	}
}
static void SetTurns(int *turns, int v)
{
	turns[FROM_2] = v;
	turns[FROM_4] = v;
	turns[FROM_6] = v;
	turns[FROM_8] = v;
}
static int GetMin(int v1, int v2)
{
	return v1 < v2 ? v1 : v2;
}
int main()
{
	int x;
	int y;
	int mod;
	int minTurn;

	scanf("%d", &H);
	scanf("%d", &W);

	for(y = 0; y < H; y++)
	for(x = 0; x < W; x++)
	{
		Map[x][y].Wall = ReadCell();
		SetTurns(Map[x][y].Turns, INT_MAX);
	}
	SetTurns(Map[0][0].Turns, -1);

	do
	{
		mod = 0;

		/* advance to vertical */
		for(x = 0; x < W; x++)
		for(y = 0; y < H; y++)
		if(!Map[x][y].Wall)
		{
			int turn = GetMin(Map[x][y].Turns[FROM_4], Map[x][y].Turns[FROM_6]);

			if(turn < INT_MAX)
			{
				int sy;

				turn++;

				/* advance to [8] */
				for(sy = y - 1; 0 <= sy && !Map[x][sy].Wall; sy--)
				{
					if(turn < Map[x][sy].Turns[FROM_2])
					{
						Map[x][sy].Turns[FROM_2] = turn;
						mod = 1;
					}
				}

				/* advance to [2] */
				for(sy = y + 1; sy < H && !Map[x][sy].Wall; sy++)
				{
					if(turn < Map[x][sy].Turns[FROM_8])
					{
						Map[x][sy].Turns[FROM_8] = turn;
						mod = 1;
					}
				}
			}
		}

		/* advance to horizon */
		for(x = 0; x < W; x++)
		for(y = 0; y < H; y++)
		if(!Map[x][y].Wall)
		{
			int turn = GetMin(Map[x][y].Turns[FROM_8], Map[x][y].Turns[FROM_2]);

			if(turn < INT_MAX)
			{
				int sx;

				turn++;

				/* advance to [4] */
				for(sx = x - 1; 0 <= sx && !Map[sx][y].Wall; sx--)
				{
					if(turn < Map[sx][y].Turns[FROM_6])
					{
						Map[sx][y].Turns[FROM_6] = turn;
						mod = 1;
					}
				}

				/* advance to [6] */
				for(sx = x + 1; sx < W && !Map[sx][y].Wall; sx++)
				{
					if(turn < Map[sx][y].Turns[FROM_4])
					{
						Map[sx][y].Turns[FROM_4] = turn;
						mod = 1;
					}
				}
			}
		}
	}
	while(mod);

	SetTurns(Map[0][0].Turns, 0);

	{
		int *turns = Map[W - 1][H - 1].Turns;

		minTurn = turns[FROM_2];
		minTurn = GetMin(minTurn, turns[FROM_4]);
		minTurn = GetMin(minTurn, turns[FROM_6]);
		minTurn = GetMin(minTurn, turns[FROM_8]);
	}

	printf("%d\n", minTurn);
	return 0;
}
