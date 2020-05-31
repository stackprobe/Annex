#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static void Test01(uint64 value)
{
	coExecute_x(xcout("ECHO %I64u | ..\\a.exe > 1.txt", value));
	coExecute_x(xcout("a2.exe %I64u > 2.txt", value));

	coExecute("TYPE 1.txt");
	coExecute("TYPE 2.txt");

	errorCase(!isSameFile("1.txt", "2.txt"));
}
int main(int argc, char **argv)
{
	uint64 value;

	mt19937_initCRnd();

#if 1
	for(value = 1; value <= 100; value++)
		Test01(1);

	while(!waitKey(0))
	{
		Test01(mt19937_range64(1, 1000000ui64));
//		Test01(mt19937_range64(1, 100000000ui64));
//		Test01(mt19937_range64(1, 10000000000ui64));
	}
#else
	if(hasArgs(1))
		value = toValue64(nextArg());
	else
		value = 1;

	m_maxim(value, 1);

	for(; value <= 10500000000ui64; value++)
//	for(; value <= 10000000000ui64; value++)
	{
		Test01(value);

		if(waitKey(0))
			break;
	}
#endif
}
