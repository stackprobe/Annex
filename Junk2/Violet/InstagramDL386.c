#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *lines = readLines("InstagramDL386.txt");
	char *line;
	uint index;

	foreach(lines, line, index)
	{
		cout("C:\\app\\Kit\\HGet\\HGet.exe /RBF C:\\temp\\%04u.jpg \"%s\"\n", 386 - index, line);
	}
}
