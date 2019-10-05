#include "all.h"

static UI4096_t Sqrt(UI4096_t val)
{
	UI4096_t bVal = UI4096_N2P2048;
	UI4096_t rVal = UI4096_N0;
	UI4096_t tVal;
	UI4096_t uVal;

	while(!UI4096_IsZero(bVal = UI4096_DivTwo(bVal)))
	{
		tVal = UI4096_Add(rVal, bVal, NULL);
		uVal = UI4096_Mul(tVal, tVal, NULL);

		if(UI4096_Comp(uVal, val) <= 0)
		{
			rVal = tVal;
		}
	}
	return rVal;
}
void A_Factorization(UI4096_t val, char *outFile)
{
	FILE *fp = fileOpen(outFile, "wt");
	UI4096_t dVal;
	UI4096_t tmp[2];
	uint64 d;

	if(UI4096_Comp(val, UI4096_N2) < 0)
		goto endSearch;

	while(UI4096_Comp(UI4096_N2, val) < 0 && !(UI4096_y(val) & 1))
	{
LOGPOS();
		writeLine(fp, "2");
		val = UI4096_DivTwo(val);
	}
	if(A_IsPrime(val, 0))
		goto endSearch;

	dVal = UI4096_N0;

	for(d = 3; ; d += 2)
	{
		if(A_IsPrime64(d))
		{
			dVal.L.L.L.L.L.L.H.Value = (uint)(d >> 32 & 0xffffffffui64);
			dVal.L.L.L.L.L.L.L.Value = (uint)(d >>  0 & 0xffffffffui64);

			while(UI4096_IsZero(UI4096_Mod(val, dVal, tmp)))
			{
LOGPOS();
				writeLine_x(fp, xcout("%I64u", d));
				val = tmp[0];

				if(A_IsPrime(val, 0))
					goto endSearch;

				if(IsStopped())
					break;
			}
			if(IsStopped())
				break;
		}
	}

	dVal.L.L.L.L.L.H.L.Value = 1;
	dVal.L.L.L.L.L.L.H.Value = 0;
	dVal.L.L.L.L.L.L.L.Value = 1;

	for(; ; )
	{
		if(UI4096_IsZero(UI4096_Mod(val, dVal, tmp)))
		{
			writeLine(fp, c_UI4096ToA(dVal));
			val = tmp[0];

			if(A_IsPrime(val, 0))
				break;
		}
		else
			dVal = UI4096_Add(dVal, UI4096_N2, NULL);

		if(IsStopped())
			break;
	}

endSearch:
	writeLine(fp, c_UI4096ToA(val));

	fileClose(fp);
}
