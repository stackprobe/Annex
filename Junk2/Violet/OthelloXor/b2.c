#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#define TEST_COUNT 10000

static uint TurnNum;
static uint Turneds[64];
static uint FirstTurned; // 0 Å` 63

static void InitGame(void)
{
	uint tNum = TurnNum;
	uint c;

	zeroclear(Turneds);

	for(c = 0; c < tNum; c++)
	{
		uint i;

		do
		{
			i = mt19937_rnd(64);
		}
		while(Turneds[i]);

		Turneds[i] = 1;

		if(!c)
			FirstTurned = i;
	}
}
static uint GetXNumber(void)
{
	uint i;
	uint tNum = 0;
	uint d;
	uint n;
	uint x;

	for(i = 0; i < 64; i++)
		if(Turneds[i])
			tNum++;

//	if(!tNum)
//		return IMAX;

//	if(tNum == 32)
//		return IMAX;

	d = tNum;
	n = 0;

	for(i = 0; i < 64; i++)
		if(Turneds[i])
			n ^= i;

	x = n % 64;

	return x;
}
static int JudgeGame(void)
{
	uint i;

	for(i = 0; i < 64; i++)
	{
		uint xNumb;

		Turneds[i] ^= 1;
		xNumb = GetXNumber();
		Turneds[i] ^= 1;

		if(xNumb == FirstTurned)
			goto CLEAR_GAME;
	}
	return 0;

CLEAR_GAME:
	return 1;
}
static void DoTest01(void)
{
	uint n = 0;
	uint d;

	for(d = 0; d < TEST_COUNT; d++)
	{
		int ret;

		InitGame();
		ret = JudgeGame();

		if(ret)
			n++;
	}
	cout("%u -> %.2f = %u / %u\n", TurnNum, (double)n / TEST_COUNT, n, TEST_COUNT);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	for(TurnNum = 1; TurnNum <= 64; TurnNum++)
	{
		DoTest01();
	}
}
