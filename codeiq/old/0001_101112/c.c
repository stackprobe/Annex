#include "C:\Factory\Common\all.h"

static void Test(uint num, uint ans)
{
	uint aAns;

	cout("%u\n", num);

	writeOneLine_cx("1.tmp", xcout("%u", num));
	execute("a.exe 1.tmp 2.tmp");
	aAns = toValue_x(readFirstLine("2.tmp"));

	cout("%u == %u\n", ans, aAns);

	errorCase(ans != aAns);
}
int main(int argc, char **argv)
{
	uint count = 5;
	uint num = 1;

	for(; ; )
	{
		char *line = xcout("%u", count);
		char *p;

		cout("[%s]\n", line);

		for(p = line; *p; p++)
		{
			Test(num, *p - '0');
			num++;
		}
		count++;
		memFree(line);
	}
}
