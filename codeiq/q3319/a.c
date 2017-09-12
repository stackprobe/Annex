#include <stdio.h>

#define MAP_SIZE_MAX 24

static int W;
static int H;
static int Map[MAP_SIZE_MAX];
static int Size;
static int Half;

static int Ans;

#define XYToI(x, y) ((x) * H + (y))

/*
	fill 1 to 2
*/
static void Fill(int x, int y)
{
	if(x < 0 || W <= x || y < 0 || H <= y || Map[XYToI(x, y)] != 1)
		return;

	Map[XYToI(x, y)] = 2;

	Fill(x - 1, y    );
	Fill(x + 1, y    );
	Fill(x,     y - 1);
	Fill(x,     y + 1);
}
static int HasEnclave(void)
{
	int i;
	int ret;

	Fill(W - 1, H - 1);

	for(i = 0; i < Size; i++)
		if(Map[i] == 1)
			break;

	ret = i < Size;

	for(i = 0; i < Size; i++)
		if(Map[i])
			Map[i] = 1;

	return ret;
}
static void Search(void)
{
	Size = W * H;

	if(Size % 2 == 1)
		return;

	Half = Size / 2;

	for(; ; )
	{
		int i;

		for(i = 0; i < Half; i++)
			Map[Size - i - 1] = 1 - Map[i];

		if(!HasEnclave())
			Ans++;

		for(i = 1; i < Half; i++)
		{
			if(!Map[i])
			{
				Map[i] = 1;
				break;
			}
			Map[i] = 0;
		}
		if(i == Half)
			break;
	}
}
int main()
{
	scanf("%d %d", &W ,&H);

	Search();

	printf("%d\n", Ans);
}
