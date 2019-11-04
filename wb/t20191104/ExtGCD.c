#include "C:\Factory\Common\all.h"

static sint64 EG_A;
static sint64 EG_B;

static uint ExtGCD(uint x, uint y)
{
	uint ret;

	if(y)
	{
		ret = ExtGCD(y, x % y);

		m_swap(EG_A, EG_B, sint64);

		EG_B -= (x / y) * EG_A;
	}
	else
	{
		ret = x;

		EG_A = 1;
		EG_B = 0;
	}
	return ret;
}
int main(int argc, char **argv)
{
	sint64 egAMin = 0;
	sint64 egAMax = 0;
	sint64 egBMin = 0;
	sint64 egBMax = 0;
	uint x;
	uint y;

	for(x = 1; x < 65535; x++)
	for(y = 1; y < 65535; y++)
	{
		if(eqIntPulseSec(5, NULL))
			cout("x, y: %u, %u (%I64d, %I64d, %I64d, %I64d)\n", x, y, egAMin, egAMax, egBMin, egBMax);

		ExtGCD(x, y);

		if(
			EG_A < egAMin || egAMax < EG_A ||
			EG_B < egBMin || egBMax < EG_B
			)
		{
			m_minim(egAMin, EG_A);
			m_maxim(egAMax, EG_A);
			m_minim(egBMin, EG_B);
			m_maxim(egBMax, EG_B);

			cout("x, y: %u, %u (%I64d, %I64d, %I64d, %I64d) %I64d, %I64d UPDT\n", x, y, egAMin, egAMax, egBMin, egBMax, EG_A, EG_B);
		}
	}
	cout("egAMin: %I64d\n", egAMin);
	cout("egAMax: %I64d\n", egAMax);
	cout("egBMin: %I64d\n", egBMin);
	cout("egBMax: %I64d\n", egBMax);
}
