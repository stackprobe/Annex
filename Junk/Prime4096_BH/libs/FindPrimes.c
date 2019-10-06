#include "all.h"

#define DIV_FILE_SIZE_MAX 0x80000000ui64

static UI4096_t PrimeCount;
static FILE *Writer;

static void WrStr(char *str)
{
	uint sLen = strlen(str);

	if(fwrite(str, 1, sLen, Writer) != sLen) // ? Ž¸”s
	{
		if(GetLastError() == 0x299)
		{
			error_m("ERROR_FILE_SYSTEM_LIMITATION");
		}
		else
		{
			error();
		}
	}
}
static void IncPrimeCount(void)
{
	PrimeCount = UI4096_Add(PrimeCount, UI4096_N1, NULL);
}
static void WrStr_x(char *str)
{
	WrStr(str);
	memFree(str);
}
void FindPrimes(UI4096_t minval, UI4096_t maxval, char *outFile, int countMode)
{
	UI4096_t val;

	PrimeCount = UI4096_N0;
	Writer = fileOpen(outFile, "wt");

	errorCase(UI4096_Comp(maxval, minval) < 0);

	if(GetUIArrSize(UI4096ToUIArr(maxval)) <= 2)
	{
		uint64 minValue;
		uint64 maxValue;
		uint64 value;

		{
			uint *arr = UI4096ToUIArr(minval);

			minValue = (uint64)arr[1] << 32 | arr[0];
		}

		{
			uint *arr = UI4096ToUIArr(maxval);

			maxValue = (uint64)arr[1] << 32 | arr[0];
		}

		if(maxValue < 2)
			goto endSearch;

		m_maxim(minValue, 2);

		if(minValue == 2)
		{
			if(countMode)
				IncPrimeCount();
			else
				WrStr("2\n");
		}
		minValue |= 1;

		maxValue--;
		maxValue |= 1;

		if(maxValue < minValue)
			goto endSearch;

		for(value = minValue; ; value += 2)
		{
			if(IsPrime_R(value))
			{
				if(countMode)
					IncPrimeCount();
				else
					WrStr_x(xcout("%I64u\n", value));
			}
			if(value == maxValue)
				break;

			if(IsStopped())
				goto endFunc;
		}
	}
	else
	{
		if(UI4096_Comp(maxval, UI4096_N2) < 0)
			goto endSearch;

		if(UI4096_Comp(minval, UI4096_N2) < 0)
			minval = UI4096_N2;

		if(UI4096_Comp(minval, UI4096_N2) == 0)
		{
			if(countMode)
				IncPrimeCount();
			else
				WrStr("2\n");
		}
		minval.L.L.L.L.L.L.L.Value |= 1;

		maxval = UI4096_Sub(maxval, UI4096_N1);
		maxval.L.L.L.L.L.L.L.Value |= 1;

		if(UI4096_Comp(maxval, minval) < 0)
			goto endSearch;

		for(val = minval; ; val = UI4096_Add(val, UI4096_N2, NULL))
		{
			if(A_IsPrime(val, 1))
			{
				if(countMode)
				{
					IncPrimeCount();
				}
				else
				{
					WrStr(c_UI4096ToA(val));
					WrStr("\n");
				}
			}
			if(!UI4096_Comp(val, maxval))
				break;

			if(IsStopped())
				goto endFunc;
		}
	}

endSearch:
	if(countMode)
	{
		WrStr(c_UI4096ToA(PrimeCount));
		WrStr("\n");
	}

endFunc:
	fileClose(Writer);
	Writer = NULL;
}
