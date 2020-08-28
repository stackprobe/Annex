#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime2.h"

int main(int argc, char **argv)
{
	IsPrime(0);

	while(hasArgs(1))
	{
		uint no;
		uint64 minval;
		uint64 maxval;
		uint64 count;
		char *file;
		FILE *fp;
		uint t1;
		uint t2;
		uint t3;

		no = toValue(nextArg());

		minval = (uint64)(no + 0) * 3936092160 - 0;
		maxval = (uint64)(no + 1) * 3936092160 - 1;

		cout("%I64u-%I64u\n", minval, maxval);

		file = xcout("C:\\temp\\Prime_%I64u-%I64u.txt", minval, maxval);
		fp = fileOpen(file, "wb");

		errorCase(setvbuf(fp, NULL, _IOFBF, 64 * 1024 * 1024));

		minval |= 1;

		t1 = nowTick();

		IsPrime_R(minval);

		t2 = nowTick();

		for(count = minval; count <= maxval; count += 2)
			if(IsPrime_R(count))
				errorCase(fprintf(fp, "%I64u\n", count) < 0);

		fileClose(fp);

		t3 = nowTick();

		cout("¶¬: %u\n", t2 - t1);
		cout("o—Í: %u\n", t3 - t2);
	}

	cout("\\e\n");
	clearGetKey();
}
