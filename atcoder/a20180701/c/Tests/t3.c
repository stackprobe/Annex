/*
	a, a2, b, t2 ‚ÌŒ‹‰Ê”äŠrƒeƒXƒg
*/

// use int

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#if 1
#define N_MIN 1
#define N_MAX 200000
#define A_MIN 1
#define A_MAX 1000000000
#else
#define N_MIN 1
#define N_MAX 100
#define A_MIN 1
#define A_MAX 30000
#endif

static void MakeTestDataFile(char *file)
{
	int n = mt19937_range(N_MIN, N_MAX);
	int i;
	FILE *fp = fileOpen(file, "wt");

	cout("n: %d\n", n);

	writeLine_x(fp, xcout("%d", n));

	for(i = 0; i < n; i++)
		writeLine_x(fp, xcout("%d", mt19937_range(A_MIN, A_MAX)));

	fileClose(fp);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	while(!waitKey(0))
	{
		char *testDataFile = makeTempPath("testData");
		char *ans_a;
		char *ans_a2;
		char *ans_b;
		char *ans_t2;
		char *outFile_a;
		char *outFile_a2;
		char *outFile_b;
		char *outFile_t2;

		MakeTestDataFile(testDataFile);

		// a
		{
			char *outFile = makeTempPath("out_a");
			autoList_t *outLines;

			coExecute_x(xcout("..\\a.exe < \"%s\" > \"%s\"", testDataFile, outFile));

			outLines = readLines(outFile);
			ans_a = (char *)unaddElement(outLines);
			releaseDim(outLines, 1);
			outFile_a = outFile;
		}

		// a2
		{
			char *outFile = makeTempPath("out_a2");
			autoList_t *outLines;

			coExecute_x(xcout("..\\a2.exe < \"%s\" > \"%s\"", testDataFile, outFile));

			outLines = readLines(outFile);
			ans_a2 = (char *)unaddElement(outLines);
			releaseDim(outLines, 1);
			outFile_a2 = outFile;
		}

		// b
		{
			char *outFile = makeTempPath("out_b");
			autoList_t *outLines;

			coExecute_x(xcout("..\\b.exe < \"%s\" > \"%s\"", testDataFile, outFile));

			outLines = readLines(outFile);
			ans_b = (char *)unaddElement(outLines);
			releaseDim(outLines, 1);
			outFile_b = outFile;
		}

		// t2
		{
			char *outFile = makeTempPath("out_t2");
			autoList_t *outLines;

			coExecute_x(xcout("t2.exe < \"%s\" > \"%s\"", testDataFile, outFile));

			outLines = readLines(outFile);
			ans_t2 = (char *)unaddElement(outLines);
			releaseDim(outLines, 1);
			outFile_t2 = outFile;
		}

		cout("ans_a:  %s\n", ans_a);
		cout("ans_a2: %s\n", ans_a2);
		cout("ans_b:  %s\n", ans_b);
		cout("ans_t2: %s\n", ans_t2);

		errorCase(strcmp(ans_a, ans_a2));
		errorCase(strcmp(ans_a, ans_b));
		errorCase(strcmp(ans_a, ans_t2));

		removeFile_x(testDataFile);
		memFree(ans_a);
		memFree(ans_a2);
		memFree(ans_b);
		memFree(ans_t2);
		removeFile_x(outFile_a);
		removeFile_x(outFile_a2);
		removeFile_x(outFile_b);
		removeFile_x(outFile_t2);
	}
}
