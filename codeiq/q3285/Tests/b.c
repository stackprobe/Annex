#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static void Test01(uint n)
{
	coExecute_x(xcout("echo %u | ..\\a.exe > 1.txt", n));
	coExecute_x(xcout("a.exe h %u > 2.txt", n));

	coExecute("TYPE 1.txt");
	coExecute("TYPE 2.txt");

	errorCase(!isSameFile("1.txt", "2.txt"));
}
int main(int argc, char **argv)
{
	uint n;

	mt19937_initCRnd();

	for(n = 1; n <= 1000; n++)
//	for(n = 1; n <= 300000; n++)
	{
		Test01(n);
	}
	for(n = 299990; n <= 300000; n++)
	{
		Test01(n);
	}
	while(!waitKey(0))
	{
		cout("ESC-TO-STOP\n");
		Test01(mt19937_range(1, 300000));
	}
}
