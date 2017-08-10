#include <stdio.h>
#include <memory.h>

#define N 8

static int Result[N][N]; // [X][Y] ... X ”Ô–Ú‚ª Y ’l‚É‚È‚Á‚½‰ñ”
static int Rnd[N];
static int Sq[N];

static void DoTest(int full)
{
	int c;
	int d;
	int tmp;

	printf("%d\n", full);

	memset(&Result, 0x00, sizeof(Result));
	memset(&Rnd, 0x00, sizeof(Rnd));

	Rnd[0] = -1;

	for(; ; )
	{
		for(; ; )
		{
			for(c = 0; c < N; c++)
			{
				if(Rnd[c] < N - 1)
				{
					Rnd[c]++;
					break;
				}
				Rnd[c] = 0;
			}
			if(c == N)
			{
				c = -1;
				break;
			}
			if(full)
				break;

			for(c = 0; c < N; c++)
				if(Rnd[c] < c)
					break;

			if(c == N)
				break;
		}
		if(c == -1)
			break;

		for(c = 0; c < N; c++)
			Sq[c] = c;

		for(c = 0; c < N; c++)
		{
			d = Rnd[c];
			tmp = Sq[c];
			Sq[c] = Sq[d];
			Sq[d] = tmp;
		}
		for(c = 0; c < N; c++)
			Result[c][Sq[c]]++;
	}
	for(c = 0; c < N; c++)
	{
		for(d = 0; d < N; d++)
		{
			if(d)
				printf(", ");

			printf("%d", Result[c][d]);
		}
		printf("\n");
	}
}
int main(int argc, char **argv)
{
	DoTest(1);
	DoTest(0);
}
