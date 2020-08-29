#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csvStream.h"

static void Main2(char *dir)
{
	autoList_t *files = lsFiles(dir);
	char *file;
	uint index;

	foreach(files, file, index)
	{
		FILE *fp = fileOpen(file, "rt");
		autoList_t *header;
		char *column;
		uint colidx;
		char *table = changeExt(getLocal(file), "");

		header = readCSVRow(fp);

		foreach(header, column, colidx)
		{
			cout("%s.%s\n", table, column);
		}
		releaseDim(header, 1);
		memFree(table);
		fileClose(fp);
	}
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	hasArgs(0); // for //x options

	Main2(c_dropDir());
}
