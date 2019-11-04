#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"

static sint64 EG_A;
static sint64 EG_B;
static uint U_A;
static uint U_B;
static uint U_MOD;

static uint ExtGCD(uint x, uint y)
{
	uint ret;

	if(y)
	{
		ret = ExtGCD(y, x % y);

		m_swap(EG_A, EG_B, sint64);

		EG_B -= (x / y) * EG_A;

		m_swap(U_A, U_B, uint);

		U_B += U_MOD - ((x / y) * U_A) % U_MOD;
		U_B %= U_MOD;

		cout("%I64d %I64d / %u %u\n", EG_A, EG_B, U_A, U_B);

		errorCase((EG_A % U_MOD + U_MOD) % U_MOD != U_A);
		errorCase((EG_B % U_MOD + U_MOD) % U_MOD != U_B);
	}
	else
	{
		ret = x;

		EG_A = 1;
		EG_B = 0;

		U_A = 1;
		U_B = 0;
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

		U_MOD = y;

		cout("%u %u\n", x, y);

		ExtGCD(x, y);
	}
#endif
}
