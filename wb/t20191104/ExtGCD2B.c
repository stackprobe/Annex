#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"

static uint EG_A;
static uint EG_B;
static uint EG_AB_MOD;

static sint64 SS_A;
static sint64 SS_B;

static uint ExtGCD(uint x, uint y)
{
	uint ret;

	if(y)
	{
		ret = ExtGCD(y, x % y);

		m_swap(EG_A, EG_B, uint);

		EG_B += EG_AB_MOD - ((x / y) * EG_A) % EG_AB_MOD;
		EG_B %= EG_AB_MOD;

		m_swap(SS_A, SS_B, sint64);

		SS_B -= (x / y) * SS_A;

		cout("%I64d %I64d / %u %u\n", SS_A, SS_B, EG_A, EG_B);

		errorCase((SS_A % EG_AB_MOD + EG_AB_MOD) % EG_AB_MOD != EG_A);
		errorCase((SS_B % EG_AB_MOD + EG_AB_MOD) % EG_AB_MOD != EG_B);
	}
	else
	{
		ret = x;

		EG_A = 1;
		EG_B = 0;

		SS_A = 1;
		SS_B = 0;

		cout("%u\n", EG_AB_MOD);
	}
	return ret;
}
int main(int argc, char **argv)
{
	uint x;
	uint y;

#if 0
	for(x = 1; x < 65535; x++)
	for(y = 1; y < 65535; y++)
	{
		cout("%u %u\n", x, y);

		U_MOD = y;

		ExtGCD(x, y);
	}
#else
	for(; ; )
	{
		x = getCryptoRand() % 65534 + 1;
		y = getCryptoRand() % 65534 + 1;

		EG_AB_MOD = y;

		cout("%u %u\n", x, y);

		ExtGCD(x, y);
	}
#endif
}
