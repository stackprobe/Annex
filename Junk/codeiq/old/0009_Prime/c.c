#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

static void MakeInput(char *wFile, uint inputNum)
{
	FILE *fp = fileOpen(wFile, "wt");
	uint count;

	for(count = 0; count < inputNum; count++)
		writeLine_x(fp, xcout("%u", mt19937_range(1, 10000)));

	fileClose(fp);
}
static int IsPrime(uint no)
{
	uint count;

	if(no < 2)
		return 0;

	for(count = 2; count < no; count++)
		if(no % count == 0)
			return 0;

	return 1;
}
static uint GetPrimeCount(uint maxNo)
{
	uint count = 0;
	uint no;

	for(no = 2; no < maxNo; no++)
		if(IsPrime(no))
			count++;

	return count;
}
static void MakeAnswer(char *rFile, char *wFile)
{
	FILE *rfp = fileOpen(rFile, "rt");
	FILE *wfp = fileOpen(wFile, "wt");

	LOGPOS();

	for(; ; )
	{
		char *line = readLine(rfp);

		if(!line)
			break;

		writeLine_x(wfp, xcout("%u", GetPrimeCount(toValue(line))));
		memFree(line);
	}
	fileClose(rfp);
	fileClose(wfp);

	LOGPOS();
}
static void DoTest(uint inputNumMin, uint inputNumMax)
{
	MakeInput("input.tmp", mt19937_range(inputNumMin, inputNumMax));

	coExecute("a.exe input.tmp > output_1.tmp");
	coExecute("TYPE output_1.tmp");

	MakeAnswer("input.tmp", "output_2.tmp");
	coExecute("TYPE output_2.tmp");

	errorCase(!isSameFile("output_1.tmp", "output_2.tmp"));
}
int main(int argc, char **argv)
{
	if(hasArgs(2))
	{
		MakeAnswer(getArg(0), getArg(1));
		return;
	}
	mt19937_init();

	DoTest(0, 0);

	for(; ; )
	{
		DoTest(1, 30);
	}
}
