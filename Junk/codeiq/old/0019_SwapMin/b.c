#include "C:\Factory\Common\all.h"

static int CompStrVal(char *s1, char *s2)
{
	return simpleComp(toValue(s1), toValue(s2));
}
int main(int argc, char **argv)
{
	autoList_t *lines = readLines(nextArg());
	char *line;
	uint index;

	rapidSort(lines, (int (*)(uint, uint))CompStrVal);

	foreach(lines, line, index)
	{
		uint val = toValue(line);
		errorCase(val != index + 1);
	}
	releaseDim(lines, 1);
	cout("OK\n");
}
