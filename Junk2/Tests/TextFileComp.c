#include "C:\Factory\Common\all.h"

static char *ToPrint(char *line)
{
	if(!line)
		return "<EOF>";

	line2JLine(line, 1, 0, 1, 1);
	return line;
}
static void TextFileComp(char *file1, char *file2)
{
	FILE *fp1 = fileOpen(file1, "rt");
	FILE *fp2 = fileOpen(file2, "rt");
	uint64 linenum = 1;

	for(; ; )
	{
		char *line1 = readLine(fp1);
		char *line2 = readLine(fp2);

		if(!line1 && !line2)
			break;

		if(!line1 || !line2 || strcmp(line1, line2))
		{
			cout("%I64u\n", linenum);
			cout("1: %s\n", ToPrint(line1));
			cout("2: %s\n", ToPrint(line2));
getKey();
		}
		memFree(line1);
		memFree(line2);
		linenum++;
	}
}
int main(int argc, char **argv)
{
	char *file1;
	char *file2;

	file1 = nextArg();
	file2 = nextArg();

	TextFileComp(file1, file2);
}
