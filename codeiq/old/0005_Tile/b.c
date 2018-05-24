#include <stdio.h>

// ’¸“_‚Ì”
#define H 4 // c
#define W 5 // ‰¡

static int Tbl[H][W];

#define m_01(cond) \
	((cond) ? 1 : 0)

static int InitTbl(int index)
{
	int r;
	int c;

	for(r = 0; r < H; r++)
	for(c = 0; c < W; c++)
	{
		Tbl[r][c] =
			m_01(r == 0 || r == H - 1) ^
			m_01(c == 0 || c == W - 1);
	}
	for(r = 0; r < H - 1; r++)
	for(c = 0; c < W - 1; c++)
	{
		int caseval = index % 4;

		if(caseval & 1)
		{
			Tbl[r][c] ^= 1;
			Tbl[r + 1][c + 1] ^= 1;
		}
		if(caseval & 2)
		{
			Tbl[r][c + 1] ^= 1;
			Tbl[r + 1][c] ^= 1;
		}
		index /= 4;
	}
	return index;
}
static int GetOddCount(void)
{
	int count = 0;
	int r;
	int c;

	for(r = 0; r < H; r++)
	for(c = 0; c < W; c++)
		if(Tbl[r][c])
			count++;

	return count;
}
static void Main2(void)
{
	int count = 0;
	int index;

	for(index = 0; ; index++)
	{
		if(InitTbl(index))
			break;

		if(GetOddCount() <= 2)
			count++;
	}
	printf("%d\n", count);
}
int main(int argc, char **argv)
{
	Main2();
}
