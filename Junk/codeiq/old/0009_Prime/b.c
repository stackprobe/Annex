#include "C:\Factory\Common\all.h"

#define PRIME_FILE "C:\\tmp\\primtlist10000000000\\primtlist10000000000.txt"

static FILE *FP;

static uint GetNextPrime(void)
{
	uint64 no = toValue64(readLine(FP));

	cout("NextPrime: %I64u\n", no);

	if(UINTMAX < no)
	{
		cout("OK\n");
		termination(0);
	}
	return (uint)no;
}
int main(int argc, char **argv)
{
	uint nextPrime;
	uint currNo;
	uint currCount = 0;
	uint readCount;

	FP = fileOpen(PRIME_FILE, "rt");
	nextPrime = GetNextPrime();

	for(currNo = 1; currNo; currNo++)
	{
		cout("%u %u\n", currCount, currNo);

		writeOneLine_cx("1.tmp", xcout("%u", currNo));
		execute("a.exe 1.tmp > 2.tmp");
		readCount = toValue(readFirstLine("2.tmp"));

		cout("%u\n", readCount);

		errorCase(readCount != currCount);

		if(currNo == nextPrime)
		{
			currCount++;
			nextPrime = GetNextPrime();
		}
	}
}
