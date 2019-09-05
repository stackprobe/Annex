#include "C:\Factory\Common\all.h"

static char *DestFile;

static void ToListFiles(char *rootDir)
{
	autoList_t *files;
	char *file;
	uint index;

	rootDir = makeFullPath(rootDir);
	errorCase_m(!existDir(rootDir), "そんなディレクトリありません。");
	files = lssFiles(rootDir);
	changeRoots(files, rootDir, NULL);
	sortJLinesICase(files);

	foreach(files, file, index)
		cout("%s\n", file);

	if(DestFile)
		writeLines(DestFile, files);

	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	if(argIs("/D"))
	{
		DestFile = nextArg();
	}

	if(hasArgs(1))
	{
		ToListFiles(nextArg());
	}
	else
	{
		ToListFiles(".");
	}
}
