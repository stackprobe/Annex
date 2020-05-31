#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static void DoTest01_b(int n, int m)
{
	coExecute_x(xcout("ECHO %d %d | a.exe > 1.txt", n, m));
	coExecute_x(xcout("ECHO %d %d | b.exe > 2.txt", n, m));

	execute("TYPE 1.txt");
	execute("TYPE 2.txt");

	errorCase(!isSameFile("1.txt", "2.txt"));
}
static void DoTest01(int n)
{
	int m;

	for(m = 0; m <= 17; m++)
	{
		DoTest01_b(n, m);
	}
}
int main(int argc, char **argv)
{
	DoTest01(0);
	DoTest01(1);
	DoTest01(2);
	DoTest01(3);
	DoTest01(100);
	DoTest01(200);
	DoTest01(300);
	DoTest01(10000);
	DoTest01(20000);
	DoTest01(30000);
	DoTest01(100000);

	while(waitKey(0) != 0x1b)
	{
		DoTest01_b(
			mt19937_rnd(100000) + 1,
			mt19937_rnd(17) + 1
			);
	}
}
