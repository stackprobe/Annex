#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *lines = newList();
	char *line;
	uint index;
	uint numb;

	for(numb = 0; numb <= 100; numb++)
	{
		addElement(lines, (uint)xcout("%c%c%c%c %u"
			,numb % 2 ? '-' : '*'
			,numb % 3 ? '-' : '*'
			,numb % 5 ? '-' : '*'
			,numb % 7 ? '-' : '*'
			,numb
			));
	}
	rapidSortLines(lines);

	foreach(lines, line, index)
	{
		cout("%s\n", line);
	}

	// g
}
