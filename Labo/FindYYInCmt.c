/*
	FindYYInCmt.exe ROOT-DIR

	C系ソース内のコメント中の "\\" を探す。
	判定はいいかげん。
*/

#include "C:\Factory\Common\all.h"

static void SearchFile(char *file)
{
	FILE *fp = fileOpen(file, "rb");
	uint lineCnt = 1;
	int inComment = 0;
	int inCppComment = 0;
	char buff[] = "__";

	for(; ; )
	{	
		int chr = readChar(fp);

		if(chr == EOF)
			break;

		buff[0] = buff[1];
		buff[1] = chr;

		if(chr == '\n')
		{
			lineCnt++;
			inCppComment = 0;
		}
		else if(!strcmp(buff, "//"))
		{
			inCppComment = 1;
		}
		else if(!strcmp(buff, "/*"))
		{
			inComment = 1;
		}
		else if(!strcmp(buff, "*/"))
		{
			inComment = 0;
		}
		else if((inComment || inCppComment) && !strcmp(buff, "\\\\")) // ? founds
		{
			cout("%s %u\n", file, lineCnt);
		}
	}
	fileClose(fp);
}
static void Main2(void)
{
	autoList_t *files = lssFiles(".");
	char *file;
	uint index;

	foreach(files, file, index)
	{
		char *ext = getExt(file);

		if(
			!_stricmp(ext, "C") ||
			!_stricmp(ext, "CPP") ||
			!_stricmp(ext, "H")
			)
		{
			SearchFile(file);
		}
	}
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	addCwd(nextArg());
	Main2();
	unaddCwd();
}
