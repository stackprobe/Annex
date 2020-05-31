/*
	íxÇ¢ÅI
*/

#include <stdio.h>
#include <limits.h>

#define W_MAX 64
#define H_MAX 64

static int W;
static int H;

static struct
{
	int Wall;
	int Passed;
}
Map[W_MAX][H_MAX];

static struct
{
	int Dir;
	int X;
	int Y;
	int Turn;
}
States[W_MAX * H_MAX];

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
int main()
{
	int x;
	int y;
	int step = 0;
	int minTurn = INT_MAX;

	scanf("%d", &H);
	scanf("%d", &W);

	for(y = 0; y < H; y++)
	for(x = 0; x < W; x++)
	{
		Map[x][y].Wall = ReadCell();
	}
	Map[0][0].Passed = 1;

	if(W == 1 && H == 1 && !Map[0][0].Wall) /* 1 x 1 */
	{
		minTurn = 0;
		goto completed;
	}

	for(; ; )
	{
		x = States[step].X;
		y = States[step].Y;

		if(States[step].Dir < 8)
		{
			States[step].Dir += 2;

			switch(States[step].Dir)
			{
			case 2: y++; break;
			case 4: x--; break;
			case 6: x++; break;
			case 8: y--; break;
			}

			if(
				0 <= x && x < W &&
				0 <= y && y < H &&
				!Map[x][y].Wall &&
				!Map[x][y].Passed
				)
			{
				int turn = States[step].Turn;

				if(1 <= step)
					if(States[step - 1].Dir != States[step].Dir)
						turn++;

				if(turn < minTurn)
				{
					if(x == W - 1 && y == H - 1) /* goal */
					{
						minTurn = turn;
					}
					else
					{
						Map[x][y].Passed = 1;

						step++;

						States[step].Dir = 0;
						States[step].X = x;
						States[step].Y = y;
						States[step].Turn = turn;
					}
				}
			}
		}
		else
		{
			if(step == 0)
				break;

			Map[x][y].Passed = 0;

			step--;
		}
	}
completed:
	printf("%d\n", minTurn);
	return 0;
}
