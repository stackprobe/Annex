#include "libs\all.h"

static UI4096_t GetLowerPrime(UI4096_t value)
{
	while(!UI4096_IsZero(value))
	{
LOGPOS();
		value = UI4096_Sub(value, UI4096_N1);

		if(A_IsPrime(value, 0))
			return value;

		if(IsStopped())
			break;
	}
	return UI4096_N0;
}
static UI4096_t GetHigherPrime(UI4096_t value)
{
	while(!UI4096_IsFill(value))
	{
LOGPOS();
		value = UI4096_Add(value, UI4096_N1, NULL);

		if(A_IsPrime(value, 0))
			return value;

		if(IsStopped())
			break;
	}
	return UI4096_N0;
}
static void Main2(void)
{
	Consts_INIT();
	Stop_INIT();
	LoadConfig();

	if(argIs("/S"))
	{
		Stop();
		return;
	}
	if(argIs("/P"))
	{
		char *sn;
		char *outFile;

		sn      = nextArg();
		outFile = nextArg();

		writeOneLineNoRet_b(outFile, A_IsPrime(AToUI4096(sn), 0) ? "P" : "N");
		return;
	}
	if(argIs("/F"))
	{
		char *sn;
		char *outFile;

		sn      = nextArg();
		outFile = nextArg();

		A_Factorization(AToUI4096(sn), outFile);
		return;
	}
	if(argIs("/L"))
	{
		char *sn;
		char *outFile;

		sn      = nextArg();
		outFile = nextArg();

		writeOneLineNoRet_b(outFile, c_UI4096ToA(GetLowerPrime(AToUI4096(sn))));
		return;
	}
	if(argIs("/H"))
	{
		char *sn;
		char *outFile;

		sn      = nextArg();
		outFile = nextArg();

		writeOneLineNoRet_b(outFile, c_UI4096ToA(GetHigherPrime(AToUI4096(sn))));
		return;
	}
	if(argIs("/R"))
	{
		char *sminval;
		char *smaxval;
		char *outFile;

		sminval = nextArg();
		smaxval = nextArg();
		outFile = nextArg();

		FindPrimes(AToUI4096(sminval), AToUI4096(smaxval), outFile, 0);
		return;
	}
	if(argIs("/C"))
	{
		char *sminval;
		char *smaxval;
		char *outFile;

		sminval = nextArg();
		smaxval = nextArg();
		outFile = nextArg();

		FindPrimes(AToUI4096(sminval), AToUI4096(smaxval), outFile, 1);
		return;
	}
	error_m("Unknown Arg");
}
int main(int argc, char **argv)
{
	Main2();
	termination(0);
}
