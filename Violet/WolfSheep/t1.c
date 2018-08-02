/*
	WolfSheep > 1.txt
	t1 1.txt

	Colorful > 1.txt
	t1 1.txt
*/

#include "C:\Factory\Common\all.h"

static void Main2(char *file)
{
	autoList_t *orgLines = readLines(file);
	autoList_t *lines;
	char *line;
	uint index;

	lines = copyLines(orgLines);

	foreach(lines, line, index)
		reverseLine(line);

	rapidSortLines(orgLines);
	rapidSortLines(lines);

	errorCase(!isSameLines(orgLines, lines, 0));

	cout("OK!\n");

	// g
}
int main(int argc, char **argv)
{
	Main2(nextArg());
}
