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
		cout("%c", FirstTurned == i ? 'F' : '-');

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

static int GXN_Print = 0;

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

	if(GXN_Print)
	{
		PrintGame();
		cout("Ç–Ç¡Ç≠ÇËï‘Ç¡ÇƒÇ¢ÇÈêŒÇÃñáêî=%u\n", tNum);
	}
	if(!tNum)
	{
		if(GXN_Print)
			cout("äÑÇÍÇ»Ç¢ÇÃÇ≈é∏îsÅI\n");

		return IMAX;
	}
	if(tNum == 32)
	{
		if(GXN_Print)
			cout("Ç«ÇøÇÁÇ™ëΩÇ¢Ç©ï™Ç©ÇÁÇ»Ç¢ÇÃÇ≈é∏îsÅI\n");

		return IMAX;
	}
	if(GXN_Print)
		cout("Ç–Ç¡Ç≠ÇËï‘Ç¡ÇƒÇ¢%sêŒÇÃñáêîÇÃï˚Ç™ëΩÇ¢ÅB(Ç±ÇÍÇÁÇÃî‘çÜÇÃçáåvÇãÅÇﬂÇÈ)\n", tNum < 32 ? "Ç»Ç¢" : "ÇÈ");

	d = tNum;
	n = 0;

	if(GXN_Print)
		cout("Sum of");

	for(i = 0; i < 64; i++)
	{
		if(Turneds[i] ? 32 < tNum : tNum < 32)
		{
			if(GXN_Print)
				cout(" %u", i + 1);

			n += i + 1;
		}
	}
	if(GXN_Print)
	{
		cout("\n= %u\n", n);
		cout("%u / %u = %u ó]ÇË %u\n", n, d, n / d, n % d);
	}
	x = n % d + 1;

	if(GXN_Print)
		cout("%uî‘ñ⁄ÇÃÇ–Ç¡Ç≠ÇËï‘Ç¡ÇƒÇ¢ÇÈêŒÇÃî‘çÜ=", x);

	for(i = 0; ; i++)
		if(Turneds[i] && !--x)
			break;

	if(GXN_Print)
	{
		cout("%u\n", i + 1);
		cout("ç≈èâÇ…Ç–Ç¡Ç≠ÇËï‘ÇµÇΩêŒÇÃî‘çÜÇ∆àŸÇ»ÇÈÇÃÇ≈é∏îsÅI\n");

		errorCase(i == FirstTurned);
	}
	return i;
}
static int JudgeGame(void)
{
	uint i;

	for(i = 0; i < 64; i++)
	{
		uint xNumb;

		if(GXN_Print)
			cout("1êlñ⁄ÇÃçsìÆ=%uî‘ñ⁄ÇÇ–Ç¡Ç≠ÇËï‘Ç∑ÅB\n", i + 1);

		Turneds[i] ^= 1;
		xNumb = GetXNumber();
		Turneds[i] ^= 1;

		if(GXN_Print)
			cout("\n");

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
	{
		cout("Åyé∏îsÇµÇΩÉPÅ[ÉXÅz\n");
		PrintGame();
		cout("ç≈èâÇ…Ç–Ç¡Ç≠ÇËï‘ÇµÇΩêŒÇÃî‘çÜ=%u\n", FirstTurned + 1);
		cout("\n");

		GXN_Print = 1;
		JudgeGame();
		GXN_Print = 0;

//		cout("\n");
//		cout("\n");

		termination(0);
	}
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	while(!hasKey())
	{
		DoTest01();
	}
}
