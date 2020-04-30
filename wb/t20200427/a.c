#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

#define ABC_MAX 2500000 // 2 p 64 r 3 == 2642245.9496291330*

static int GLCM_IsLT(uint64 *ap, uint64 *bp)
{
	if(*ap == 0)
		return 0;

	if(*bp == 0)
		return 1;

	return *ap < *bp;
}
static uint64 GLCM_Product(uint64 *p)
{
	uint64 ret = 1;

	for(; *p != 0; p++)
		ret *= *p;

	return ret;
}
static uint64 GetLCM(uint a, uint b, uint c)
{
	uint64 af[64];
	uint64 bf[64];
	uint64 cf[64];
	uint64 *ap;
	uint64 *bp;
	uint64 *cp;

	Factorization((uint64)a, af);
	Factorization((uint64)b, bf);
	Factorization((uint64)c, cf);

	ap = af;
	bp = bf;
	cp = cf;

	while(*ap != 0 && *bp != 0 && *cp != 0)
	{
		if(*ap == *bp && *ap == *cp)
		{
			ap++;
			bp++[0] = 1;
			cp++[0] = 1;
		}
		else if(*ap == *bp && (*ap < *cp || *cp == 0))
		{
			ap++;
			bp++[0] = 1;
		}
		else if(*bp == *cp && (*bp < *ap || *ap == 0))
		{
			bp++;
			cp++[0] = 1;
		}
		else if(*ap == *cp && (*ap < *bp || *bp == 0))
		{
			ap++;
			cp++[0] = 1;
		}
		else
		{
			if(GLCM_IsLT(ap, bp))
			{
				if(GLCM_IsLT(ap, cp))
					ap++;
				else
					cp++;
			}
			else
			{
				if(GLCM_IsLT(bp, cp))
					bp++;
				else
					cp++;
			}
		}
	}

	return
		GLCM_Product(af) *
		GLCM_Product(bf) *
		GLCM_Product(cf);
}
static int Red(uint64 *tp, uint v)
{
	uint64 vv = (uint64)v * v;

	if(*tp < vv)
		return 0;

	*tp -= vv;
	return 1;
}
int main(int argc, char **argv)
{
	uint a;
	uint b;
	uint c;

	for(a = 1; a <= ABC_MAX; a++)
	for(b = 1; b <= a; b++)
	for(c = 1; c <= b; c++)
	{
		uint64 lcm = GetLCM(a, b, c);
		uint64 t;

		t = lcm;

		if(
			Red(&t, a) &&
			Red(&t, b) &&
			Red(&t, c) &&
			t == 0
			)
			cout("%I64u = %u^2 + %u^2 + %u^2\n", lcm, c, b, a);
	}
}
