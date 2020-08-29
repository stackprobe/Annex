#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *lines = readLines(c_dropFile());
	char *line;
	uint index;

	foreach(lines, line, index)
	{
		errorCase(!lineExp("<1,,>\t<1,,>", line));
	}
	rapidSortJLinesICase(lines);

	foreach(lines, line, index)
	if(index)
	{
		char *prev = getLine(lines, index - 1);
		char *ext;
		char *prevExt;

		ext     = strx(line);
		prevExt = strx(prev);

		ne_strchr(ext,     '\t')[0] = '\0';
		ne_strchr(prevExt, '\t')[0] = '\0';

		if(!_stricmp(ext, prevExt))
		{
			cout("o %s\n", line);
			cout("x %s\n", prev);

			prev[0] = '\0';
		}
	}
	trimLines(lines);

	writeLines_b_cx(getOutFile("MimeType.tsv"), lines);

	openOutDir();
}
