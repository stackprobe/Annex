#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc.h"

static void ToNumericOnly(char *line)
{
	char *p;

	for(p = line; *p; p++)
		if(!m_isdecimal(*p) && *p != '.')
			*p = ' ';
}
int main(int argc, char **argv)
{
	autoList_t *lines = readLines_x(changeExt(getSelfFile(), "txt"));
	uint index;
	autoList_t *outLines = newList();

	for(index = 1; index < getCount(lines); index++)
	{
		char *line = getLine(lines, index);
		autoList_t *tokens;

		ToNumericOnly(line);
		tokens = tokenize(line, ' ');
		trimLines(tokens);

		addElement(outLines, (uint)xcout(
			"%s*%s*%s"
			,getLine(tokens, 0)
			,calcLine(getLine(tokens, 1), '/', getLine(tokens, 0), 10, 10)
			,calcLine(getLine(tokens, 1), '/', "12", 10, 10)
			)); // g

		releaseDim(tokens, 1);
	}
	releaseDim(lines, 1);

	shootingStarLines(outLines);

	{
		char *line;

		foreach(outLines, line, index)
			cout("%s\n", line);
	}

	releaseDim(outLines, 1);
}
