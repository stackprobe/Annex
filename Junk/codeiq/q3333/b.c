#include <stdio.h>
#include <stdlib.h>

#define SQR_MAX 100
#define SZ_MAX 202

static int SqrXs[SQR_MAX];
static int SqrYs[SQR_MAX];
static int SqrWs[SQR_MAX];
static int SqrHs[SQR_MAX];
static int BrdHs[SQR_MAX];
static int SqrNum;
static int Xs[SZ_MAX];
static int Ys[SZ_MAX];
static int XNum;
static int YNum;
static int Board[SZ_MAX][SZ_MAX];

static int GetMin(int a, int b)
{
	return a < b ? a : b;
}
static int GetMax(int a, int b)
{
	return a < b ? b : a;
}
static int CompInt(const void *a, const void *b)
{
	return *(int *)a - *(int *)b;
}
static int Distinct(int *arr, int count)
{
	int r;
	int w = 1;

	for(r = 1; r < count; r++)
		if(arr[r - 1] != arr[r])
			arr[w++] = arr[r];

	return w;
}
static int GetIndex(int *arr, int count, int target)
{
	int l = 0;
	int r = count - 1;

	while(l < r)
	{
		int m = (l + r) / 2;

		if(arr[m] < target)
		{
			l = m + 1;
		}
		else if(target < arr[m])
		{
			r = m - 1;
		}
		else
		{
			return m;
		}
	}
	return l;
}
static void GenBoard(int sz, int hi)
{
	int r;
	int s;
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
	int x;
	int y;

	SqrNum = sz < 100 ? sz : 100;
	r = 1;
	s = 0;

	Xs[s] = 0;
	Ys[s] = 0;
	s++;

	for(i = 0; i < SqrNum; i++)
	{
		r0 = r = (r % 10009) * 99991;
		r1 = r = (r % 10009) * 99991;
		r2 = r = (r % 10009) * 99991;
		r3 = r = (r % 10009) * 99991;
		r4 = r = (r % 10009) * 99991;

		sqrX = r0 % sz;
		sqrY = r1 % sz;
		sqrW = (r2 % (sz - sqrX)) % 100;
		sqrH = (r3 % (sz - sqrY)) % 100;
		brdH = (r4 % hi) + 1;

		Xs[s] = sqrX;
		Ys[s] = sqrY;
		s++;

		Xs[s] = sqrX + sqrW;
		Ys[s] = sqrY + sqrH;
		s++;

		SqrXs[i] = sqrX;
		SqrYs[i] = sqrY;
		SqrWs[i] = sqrW;
		SqrHs[i] = sqrH;
		BrdHs[i] = brdH;
	}

	Xs[s] = sz;
	Ys[s] = sz;
	s++;

	qsort(Xs, s, sizeof(int), CompInt);
	qsort(Ys, s, sizeof(int), CompInt);

	XNum = Distinct(Xs, s);
	YNum = Distinct(Ys, s);

	for(x = 0; x < XNum; x++)
	for(y = 0; y < YNum; y++)
	{
		Board[x][y] = 1;
	}
	for(x = 0; x < XNum; x++) Board[x][YNum - 1] = 0;
	for(y = 0; y < YNum; y++) Board[XNum - 1][y] = 0;

	for(i = 0; i < SqrNum; i++)
	{
		int xs = GetIndex(Xs, XNum, SqrXs[i]);
		int ys = GetIndex(Ys, YNum, SqrYs[i]);
		int xe = GetIndex(Xs, XNum, SqrXs[i] + SqrWs[i]);
		int ye = GetIndex(Ys, YNum, SqrYs[i] + SqrHs[i]);

		brdH = BrdHs[i];

		for(x = xs; x < xe; x++)
		for(y = ys; y < ye; y++)
		{
			Board[x][y] = brdH;
		}
	}
}

static int Ans;

static void CheckRect(int x, int y, int w, int h)
{
	int szX = Xs[x + w] - Xs[x];
	int szY = Ys[y + h] - Ys[y];
	int sz;

	sz = GetMin(szX, szY);

	Ans = GetMax(Ans, sz);
}
static int GetHeight(int x, int y)
{
	int i;

	for(i = 1; Board[x][y] == Board[x][y + i]; i++);

	return i;
}
static int Search(void)
{
	int x;
	int y;

	Ans = 0;

	for(x = 0; x + 1 < XNum; x++)
	for(y = 0; y + 1 < YNum; y++)
	{
		int i;
		int h = GetHeight(x, y);

		CheckRect(x, y, 1, h);

		for(i = 1; Board[x][y] == Board[x + i][y]; i++)
		{
			h = GetMin(h, GetHeight(x + i, y));
			CheckRect(x, y, i + 1, h);
		}
	}
	Ans *= Ans;
	return Ans;
}
int main()
{
	int sz;
	int hi;

	while(scanf("%d,%d", &sz, &hi) != EOF)
	{
		GenBoard(sz, hi);
		printf("%d\n", Search());
	}
}
