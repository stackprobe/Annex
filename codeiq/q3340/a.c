#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define N_MAX 9
#define MAP_SZ (N_MAX * 2 + 1)
#define START_XY N_MAX

static int Map[MAP_SZ][MAP_SZ];

#define PTN_MAX 30000 // 4*3^8 == 26244

static char Ptns[PTN_MAX][(1 + N_MAX) * 2];
static int PtnCnt;

static void AddPtn(void)
{
	int i = 0;
	int x;
	int y;

	for(x = 0; x < MAP_SZ; x++)
	for(y = 0; y < MAP_SZ; y++)
	{
		if(Map[x][y])
		{
			Ptns[PtnCnt][i++] = x;
			Ptns[PtnCnt][i++] = y;
		}
	}
	PtnCnt++;
}
static void Search(int x, int y, int n)
{
	if(Map[x][y])
		return;

	Map[x][y] = 1;

	if(n)
	{
		Search(x - 1, y, n - 1);
		Search(x + 1, y, n - 1);
		Search(x, y - 1, n - 1);
		Search(x, y + 1, n - 1);
	}
	else
		AddPtn();

	Map[x][y] = 0;
}
static int CompPtn(const void *a, const void *b)
{
	return memcmp(a, b, sizeof(Ptns[0]));
}
static int GetDistPtnCnt(void)
{
	int ans = 1;
	int i;

	for(i = 1; i < PtnCnt; i++)
		if(CompPtn(Ptns[i], Ptns[i - 1]))
			ans++;

	return ans;
}
int main()
{
	int n;
	int ans;

	scanf("%d", &n);

	Search(START_XY, START_XY, n);
	qsort(Ptns, PtnCnt, sizeof(Ptns[0]), CompPtn);
	ans = GetDistPtnCnt();

	printf("%d\n", ans);
}
