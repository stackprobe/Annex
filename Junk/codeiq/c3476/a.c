#include <stdio.h>
#include <stdlib.h>

#define MAP_W 5
#define MAP_H 5

#define IO_LENMAX (((MAP_W) + 1) * (MAP_H) - 1)

#define START_X 2
#define START_Y 0

typedef struct Cell_st
{
	int Hi;
	int Reached;
	int Reached_Temp;
}
Cell_t;

static Cell_t Map[MAP_W][MAP_H];

static void Search_Pair(int maxHiDiff, int x1, int y1, int x2, int y2)
{
	Cell_t *a = Map[x1] + y1;
	Cell_t *b = Map[x2] + y2;

	if(a->Reached && !b->Reached && abs(a->Hi - b->Hi) <= maxHiDiff)
	{
		b->Reached_Temp = 1;
	}
}
static int Search(int maxHiDiff)
{
	int x;
	int y;
	int ret;

	for(x = 0; x < MAP_W - 1; x++)
	for(y = 0; y < MAP_H; y++)
	{
		Search_Pair(maxHiDiff, x, y, x + 1, y);
		Search_Pair(maxHiDiff, x + 1, y, x, y);
	}
	for(x = 0; x < MAP_W; x++)
	for(y = 0; y < MAP_H - 1; y++)
	{
		Search_Pair(maxHiDiff, x, y, x, y + 1);
		Search_Pair(maxHiDiff, x, y + 1, x, y);
	}
	ret = 0;

	for(x = 0; x < MAP_W; x++)
	for(y = 0; y < MAP_H; y++)
	{
		ret |= Map[x][y].Reached_Temp;
		Map[x][y].Reached |= Map[x][y].Reached_Temp;
		Map[x][y].Reached_Temp = 0;
	}
	return ret;
}
int main()
{
	char line[IO_LENMAX + 1];
	int x;
	int y;

	scanf("%s", line);

	for(x = 0; x < MAP_W; x++)
	for(y = 0; y < MAP_H; y++)
	{
		Map[x][y].Hi = line[x + y * ((MAP_W) + 1)] - '0';
	}
	Map[START_X][START_Y].Reached = 1;

	while(Search(1));
	Search(3);
	while(Search(1));

	for(x = 0; x < MAP_W; x++)
	for(y = 0; y < MAP_H; y++)
	{
		if(Map[x][y].Reached)
			line[x + y * ((MAP_W) + 1)] = '*';
	}
	printf("%s\n", line);
}
