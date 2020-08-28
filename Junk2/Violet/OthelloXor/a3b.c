#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

//#define TURN_NUM 9
#define TURN_NUM 10
//#define TURN_NUM 11
//#define TURN_NUM 12

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
static void IG_Perform(void)
{
	int ret = JudgeGame();

	if(!ret)
		PrintGame();

	if(eqIntPulseSec(2, NULL) && hasKey())
		termination(0);
}
static void IG_Turned(void)
{
	uint index;

	for(index = 0; index < 64; index++)
	{
		if(Turneds[index])
		{
			FirstTurned = index;

			IG_Perform();

			FirstTurned = 0; // 2bs
		}
	}
}

static uint TurnedCnt;

static void IG_Turn(uint index)
{
	if(TURN_NUM <= TurnedCnt)
	{
		IG_Turned();
	}
	else if(index < 64)
	{
		TurnedCnt++;
		Turneds[index] = 1;

		IG_Turn(index + 1);

		Turneds[index] = 0;
		TurnedCnt--;

		IG_Turn(index + 1);
	}
}
static void InitGame(void)
{
	IG_Turn(0);
}
static void DoTest01(void)
{
	InitGame();
}
int main(int argc, char **argv)
{
	hasArgs(0); // for //X options

	mt19937_initCRnd();

	DoTest01();
}
