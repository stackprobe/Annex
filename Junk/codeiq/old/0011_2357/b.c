#include "C:\Factory\Common\all.h"

static void DoTest(char *line)
{
	error(); // �߂�ǂ���w
}
int main(int argc, char **argv)
{
	autoList_t *lines = readLines("magic_number.txt");
	char *line;
	uint index;

	foreach(lines, line, index)
		DoTest(line);

	releaseDim(lines, 1);
}
