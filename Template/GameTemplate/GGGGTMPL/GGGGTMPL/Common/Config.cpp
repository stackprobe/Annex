/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

// Conf_* >

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
/*
	-1 == デフォルト
	0  == 最初のモニタ
	1  == 2番目のモニタ
	2  == 3番目のモニタ
	...
*/
int Conf_DisplayIndex = 1;

// < Conf_*

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ImportConfig(void)
{
	char *confFile = "Config.conf";

	if(accessible(confFile))
	{
		autoList<uchar> *fileData = readFile(confFile);
		int rIndex = 0;

		// Conf_* >

		Conf_DisplayIndex = atoi_x(neReadCfgLine(fileData, rIndex));

		// < Conf_*

		{
			char *line = neReadCfgLine(fileData, rIndex);

			errorCase(strcmp(line, "\\e"));
			memFree(line);
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
char *neReadCfgLine(autoList<uchar> *fileData, int &rIndex)
{
	for(; ; )
	{
		char *line = neReadLine(fileData, rIndex);

		if(*line && *line != ';')
			return line;

		memFree(line);
	}
}
