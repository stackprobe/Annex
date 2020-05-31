#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

static void *MemAlloc(size_t size)
{
	void *ret = malloc(size);

	if(!ret) // ? malloc error
	{
		printf("malloc error\n");
		exit(1);
	}
	return ret;
}

typedef struct State_st
{
	int X;
	int Y;
	int Dir;
	int Step;
	struct State_st *Next;
}
State_t;

static State_t *StockHead;
static State_t *StockTail;

static State_t *TakeState(void)
{
	State_t *ret;

	if(StockHead == StockTail)
		return (State_t *)MemAlloc(sizeof(State_t));

	ret = StockHead;
	StockHead = ret->Next;
	return ret;
}
static void GiveState(State_t *i)
{
	StockTail->Next = i;
	StockTail = i;
}

static State_t *Head;
static State_t *Tail;

static void AddState(int x, int y, int dir, int step)
{
	Tail->X = x;
	Tail->Y = y;
	Tail->Dir = dir;
	Tail->Step = step;
	Tail->Next = TakeState();
	Tail = Tail->Next;
}

static int W;
static int H;
static int SMax;

static int Ans;

static int GetMinStep(State_t *i)
{
	int xs = (W - i->X) - 1;
	int ys = (H - i->Y) - 1;
	int ret;

	if(xs && ys)
	{
		if(i->Dir == 4 || i->Dir == 8)
			ret = 2;
		else
			ret = 1;
	}
	else if(xs)
	{
		if(i->Dir == 4)
			ret = 2;
		else if(i->Dir == 6)
			ret = 0;
		else
			ret = 1;
	}
	else
	{
		if(i->Dir == 8)
			ret = 2;
		else if(i->Dir == 2)
			ret = 0;
		else
			ret = 1;
	}
	return ret + xs + ys;
}
static int GetMinStepPtns(State_t *i)
{
	int xs = (W - i->X) - 1;
	int ys = (H - i->Y) - 1;
	int ret;

	if(xs && ys)
	{
		ret = 1;
	}
	else if(xs)
	{
		if(i->Dir == 4)
			ret = 2;
		else
			ret = 1;
	}
	else
	{
		if(i->Dir == 8)
			ret = 2;
		else
			ret = 1;
	}
	return ret;
}
static void Operation(State_t *i)
{
	int minStepNeed;
	int dir;
	int x;
	int y;

	if(i->X == W - 1 && i->Y == H - 1)
	{
		if(i->Step == SMax)
			Ans++;

		return;
	}
	minStepNeed = i->Step + GetMinStep(i);

	if(SMax < minStepNeed)
		return;

	if(SMax == minStepNeed)
	{
		Ans += GetMinStepPtns(i);
		return;
	}
	switch(i->Dir) // turn right
	{
	case 2: dir = 4; break;
	case 4: dir = 8; break;
	case 8: dir = 6; break;
	case 6: dir = 2; break;
	}
	AddState(i->X, i->Y, dir, i->Step + 1);

	switch(i->Dir) // turn left
	{
	case 2: dir = 6; break;
	case 4: dir = 2; break;
	case 8: dir = 4; break;
	case 6: dir = 8; break;
	}
	AddState(i->X, i->Y, dir, i->Step + 1);

	x = i->X;
	y = i->Y;

	switch(i->Dir)
	{
	case 2: y++; break;
	case 4: x--; break;
	case 8: y--; break;
	case 6: x++; break;
	}
	if(0 <= x && x < W && 0 <= y && y < H)
	{
		AddState(x, y, i->Dir, i->Step + 1);
	}
}
int main()
{
	scanf("%d %d %d", &W, &H, &SMax);

	StockHead = (State_t *)MemAlloc(sizeof(State_t));
	StockTail = StockHead;

	Head = TakeState();
	Tail = Head;

	AddState(0, 0, 2, 0);
	AddState(0, 0, 4, 0);
	AddState(0, 0, 6, 0);
	AddState(0, 0, 8, 0);

	do
	{
		State_t *i = Head;

		Operation(i);

		Head = i->Next;
		GiveState(i);
	}
	while(Head != Tail);

	GiveState(Head);

	while(StockHead != StockTail)
	{
		State_t *i = StockHead;

		StockHead = i->Next;
		free(i);
	}
	free(StockHead);

	printf("%d\n", Ans);
}
