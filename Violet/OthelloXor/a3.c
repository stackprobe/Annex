#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

//#define TURN_NUM 11 // ñ≥Ç¢Ç¡Ç€Ç¢ÅB
#define TURN_NUM 12

static uint Turneds[64];
static uint FirstTurned; // 0 Å` 63

static void PrintGame(void)
{
	uint i;

	for(i = 0; i < 64; i++)
		cout("%c", Turneds[i] ? 'T' : '-');

	cout("\n");

	for(i = 0; i < 64; i++)
		cout("%c", FirstTurned == i ? 'F' : ' ');

	cout("\n");
}
static void InitGame(void)
{
	uint tNum = TURN_NUM;
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

	if(!tNum)
		return IMAX;

	if(tNum == 32)
		return IMAX;

	d = tNum;
	n = 0;

	for(i = 0; i < 64; i++)
		if(Turneds[i] ? 32 < tNum : tNum < 32)
			n += i + 1;

	x = n % d + 1;

	for(i = 0; ; i++)
		if(Turneds[i] && !--x)
			break;

	return i;
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
	int ret;

	InitGame();
	ret = JudgeGame();

	if(!ret)
		PrintGame();
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	while(!hasKey())
	{
		DoTest01();
	}
}
