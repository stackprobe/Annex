#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

enum
{
	DIR_N,
	DIR_E,
	DIR_W,
	DIR_S,
};

static int TurnRight(int dir)
{
	switch(dir)
	{
	case DIR_N: return DIR_E;
	case DIR_E: return DIR_S;
	case DIR_S: return DIR_W;
	case DIR_W: return DIR_N;
	}
	return -1; // dummy
}
static int TurnLeft(int dir)
{
	switch(dir)
	{
	case DIR_N: return DIR_W;
	case DIR_E: return DIR_N;
	case DIR_S: return DIR_E;
	case DIR_W: return DIR_S;
	}
	return -1; // dummy
}

static int W;
static int H;
static int SMax;

static int *CurrPtns;
static int *PrevPtns;

static int *GetPtnP(int *tbl, int x, int y, int dir)
{
	return tbl + x * (H * 4) + y * 4 + dir;
}
static int GetPtn(int *tbl, int x, int y, int dir)
{
	return *GetPtnP(tbl, x, y, dir);
}
static void SetPtn(int *tbl, int x, int y, int dir, int ptn)
{
	*GetPtnP(tbl, x, y, dir) = ptn;
}
static void ClearPtns(int *tbl)
{
	int i;

	for(i = 0; i < W * H * 4; i++)
		tbl[i] = 0;
}
static void MakePrevPtns(void)
{
	int x;
	int y;
	int dir;

	for(x = 0; x < W; x++)
	for(y = 0; y < H; y++)
	for(dir = 0; dir < 4; dir++)
	{
		int ptn;
		int ax;
		int ay;

		ptn = GetPtn(CurrPtns, x, y, TurnRight(dir));
		ptn += GetPtn(CurrPtns, x, y, TurnLeft(dir));

		ax = x;
		ay = y;

		switch(dir)
		{
		case DIR_N: ay--; break;
		case DIR_E: ax++; break;
		case DIR_W: ax--; break;
		case DIR_S: ay++; break;
		}
		if(
			0 <= ax && ax < W &&
			0 <= ay && ay < H
			)
			ptn += GetPtn(CurrPtns, ax, ay, dir);

		SetPtn(PrevPtns, x, y, dir, ptn);
	}

	SetPtn(PrevPtns, W - 1, H - 1, DIR_N, 0);
	SetPtn(PrevPtns, W - 1, H - 1, DIR_E, 0);
	SetPtn(PrevPtns, W - 1, H - 1, DIR_W, 0);
	SetPtn(PrevPtns, W - 1, H - 1, DIR_S, 0);
}
int main()
{
	int step;
	int ans;

	scanf("%d %d %d", &W, &H, &SMax);

	CurrPtns = (int *)malloc(W * H * 4 * sizeof(int));
	PrevPtns = (int *)malloc(W * H * 4 * sizeof(int));

	if(!CurrPtns || !PrevPtns)
		exit(1); // fatal

	ClearPtns(CurrPtns);
	SetPtn(CurrPtns, W - 1, H - 1, DIR_N, 1);
	SetPtn(CurrPtns, W - 1, H - 1, DIR_E, 1);
	SetPtn(CurrPtns, W - 1, H - 1, DIR_W, 1);
	SetPtn(CurrPtns, W - 1, H - 1, DIR_S, 1);

	for(step = 0; step < SMax; step++)
	{
		int *tmp;

		MakePrevPtns();

		tmp = CurrPtns;
		CurrPtns = PrevPtns;
		PrevPtns = tmp;
	}

	ans =
		GetPtn(CurrPtns, 0, 0, DIR_N) +
		GetPtn(CurrPtns, 0, 0, DIR_E) +
		GetPtn(CurrPtns, 0, 0, DIR_W) +
		GetPtn(CurrPtns, 0, 0, DIR_S);

	printf("%d\n", ans);

	free(CurrPtns);
	free(PrevPtns);
}
