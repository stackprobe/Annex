/*
	TextFileComp2.exe テキストファイル 余計な行が入っているテキストファイル
*/

#include "C:\Factory\Common\all.h"

static void PrintFactors(uint64 value)
{
	uint64 d;

	for(d = 2; d <= value; )
	{
		if(value % d == 0)
		{
			cout("\t%I64u", d);
			value /= d;
		}
		else
			d++;
	}
	cout("\n");
	errorCase(value != 1); // 2bs
}
static void TextFileComp(char *file1, char *file2)
{
	FILE *fp1 = fileOpen(file1, "rt");
	FILE *fp2 = fileOpen(file2, "rt");
	uint linenum = 0;

	for(; ; )
	{
		char *line1 = readLine(fp1);

		if(!line1)
			break;

//		line2JLine(line1, 1, 0, 1, 1);

		for(; ; )
		{
			char *line2 = readLine(fp2);

			errorCase(!line2);

//			line2JLine(line2, 1, 0, 1, 1);
			linenum++;

			if(!strcmp(line1, line2))
			{
				memFree(line2);
				break;
			}
			cout("%u: %s\n", linenum, line2);
PrintFactors(toValue64(line2));
			memFree(line2);
		}
		memFree(line1);
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
