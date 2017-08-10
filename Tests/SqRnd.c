#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

#define N 10

static uint Result[N][N]; // [X][Y] == X ”Ô–Ú‚É Y ’l‚ª—ˆ‚½‰ñ”
static int Mode;
static uint Sq[N];

static void MkSq(void)
{
	uint i;

	for(i = 0; i < N; i++)
	{
		Sq[i] = i;
	}
	for(i = 0; i < N; i++)
	{
		uint tmp;
		uint j;

		if(Mode)
			j = mt19937_rnd(N);
		else
			j = mt19937_range(i, N - 1);

		tmp = Sq[i];
		Sq[i] = Sq[j];
		Sq[j] = tmp;
	}
}
static void DoTest(void)
{
	uint c;
	uint i;
	uint j;

	cout("%u\n", Mode);

	zeroclear(Result);
	mt19937_initRnd(time(NULL));

	for(c = 0; c < 1000000; c++)
	{
		MkSq();

		for(i = 0; i < N; i++)
		{
			Result[i][Sq[i]]++;
		}
	}
	for(i = 0; i < N; i++)
	{
		for(j = 0; j < N; j++)
		{
			cout("%6u,", Result[i][j]);
		}
		cout("\n");
	}
}
int main(int argc, char **argv)
{
	mt19937_initRnd(time(NULL));

	Mode = 1;
	DoTest();
	Mode = 0;
	DoTest();
}
