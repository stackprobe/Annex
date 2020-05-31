#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static void DoTest_2(int sz, int hi)
{
	char *vectFile = makeTempPath(NULL);
	char *ansFile1 = makeTempPath(NULL);
	char *ansFile2 = makeTempPath(NULL);

	cout("%d,%d\n", sz, hi);

	writeOneLine_cx(vectFile, xcout("%d,%d", sz, hi));

	coExecute_x(xcout("a.exe < \"%s\" > \"%s\"", vectFile, ansFile1));
	coExecute_x(xcout("b.exe < \"%s\" > \"%s\"", vectFile, ansFile2));

	execute_x(xcout("TYPE \"%s\"", ansFile1));
	execute_x(xcout("TYPE \"%s\"", ansFile2));

	errorCase(!isSameFile(ansFile1, ansFile2));

	removeFile(vectFile);
	removeFile(ansFile1);
	removeFile(ansFile2);
	memFree(vectFile);
	memFree(ansFile1);
	memFree(ansFile2);

	cout("Ok\n");
}
static void DoTest(void)
{
	int sz = mt19937_rnd(3000) + 1;
	int hi = mt19937_rnd(9) + 1;

	DoTest_2(sz, hi);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	{
		int hi;

		for(hi = 1; hi <= 9; hi++)
		{
			DoTest_2(1, hi);
			DoTest_2(2, hi);
			DoTest_2(3, hi);
			DoTest_2(3000, hi);
		}
	}

	while(!hasKey())
	{
		DoTest();
	}
}
