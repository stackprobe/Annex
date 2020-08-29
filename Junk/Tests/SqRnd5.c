#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

#define N 6
#define TEST_NUM 1000000

static uint Sq[N];
static uint SumTbl[N][N];

static void DoTest(uint t_max)
{
	uint t;
	uint c;
	uint d;
	uint tmp;

	for(c = 0; c < N; c++)
	{
		Sq[c] = c;
	}
	for(t = 0; t < t_max; t++)
	{
		for(c = 0; c < N; c++)
		{
			d = mt19937_rnd(N);

			tmp = Sq[c];
			Sq[c] = Sq[d];
			Sq[d] = tmp;
		}
		/*
		tmp = Sq[0];
		Sq[0] = Sq[1];
		Sq[1] = tmp;
		*/
	}
	for(c = 0; c < N; c++)
	{
		SumTbl[c][Sq[c]]++;
	}
}
static void DoMain(uint t_max)
{
	uint c;
	uint d;

	zeroclear(SumTbl);

	cout("%u\n", t_max);

	for(c = 0; c < TEST_NUM; c++)
	{
		DoTest(t_max);
	}
	for(c = 0; c < N; c++)
	{
		for(d = 0; d < N; d++)
		{
			if(d)
				cout(", ");

			cout("%f", (double)SumTbl[c][d] / TEST_NUM);
		}
		cout("\n");
	}
}
int main(int argc, char **argv)
{
	uint t_max;

	mt19937_initRnd(time(NULL));

	for(t_max = 1; t_max <= 5; t_max++)
	{
		DoMain(t_max);
	}
}
