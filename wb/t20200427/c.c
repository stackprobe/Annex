#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

#define ABC_MAX 2500000 // 2 p 64 r 3 == 2642245.9496291330*

static uint64 FactorTable[ABC_MAX + 1][64];

static uint64 GetLCM(uint a, uint b, uint c)
{
	uint64 *af = FactorTable[a];
	uint64 *bf = FactorTable[b];
	uint64 *cf = FactorTable[c];
	uint64 *ap;
	uint64 *bp;
	uint64 *cp;
	uint64 denom = 1;

	ap = af;
	bp = bf;
	cp = cf;

	while(*ap != UINT64MAX && *bp != UINT64MAX && *cp != UINT64MAX)
	{
		if(*ap == *bp && *ap == *cp)
		{
			denom *= *ap;
			denom *= *ap;

			ap++;
			bp++;
			cp++;
		}
		else if(*ap == *bp && *ap < *cp)
		{
			denom *= *ap;

			ap++;
			bp++;
		}
		else if(*bp == *cp && *bp < *ap)
		{
			denom *= *bp;

			bp++;
			cp++;
		}
		else if(*ap == *cp && *ap < *bp)
		{
			denom *= *ap;

			ap++;
			cp++;
		}
		else
		{
			if(*ap < *bp)
			{
				if(*ap < *cp)
					ap++;
				else
					cp++;
			}
			else
			{
				if(*bp < *cp)
					bp++;
				else
					cp++;
			}
		}
	}
	return ((uint64)a * (uint64)b * (uint64)c) / denom;
}
int main(int argc, char **argv)
{
	uint a;
	uint b;
	uint c;

	for(a = 1; a <= ABC_MAX; a++)
	{
		uint64 av = (uint64)a * a;

		Factorization((uint64)a, FactorTable[a]);

		{
			uint64 *p = FactorTable[a] + 1;

			while(*p != 0)
				p++;

			*p = UINT64MAX;
		}

		for(b = 1; b <= a; b++)
		{
			uint64 bv = av + (uint64)b * b;

			for(c = 1; c <= b; c++)
			{
				uint64 cv = bv + (uint64)c * c;
				uint64 lcm = GetLCM(a, b, c);

				if(cv == lcm)
					cout("%I64u = %u^2 + %u^2 + %u^2\n", lcm, c, b, a);
			}
		}
	}
}
