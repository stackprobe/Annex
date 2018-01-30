#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csv.h"

static autoList_t *GetCellVals(autoList_t *rows, uint colidx)
{
	autoList_t *values = newList();
	autoList_t *row;
	uint rowidx;

	foreach(rows, row, rowidx)
	if(rowidx)
	{
		char *value = strx(getLine(row, colidx));

//		trim(value, ' ');
		line2JToken(value, 1, 0);

		addElement(values, (uint)value);
	}
	distinct2(values, (sint (*)(uint, uint))strcmp, (void (*)(uint))memFree);
	return values;
}
static void Main2(char *dir)
{
	autoList_t *files = lsFiles(dir);
	char *file;
	uint index;

	foreach(files, file, index)
	{
		autoList_t *rows = readCSVFile(file);
		autoList_t *header;
		char *column;
		uint colidx;
		char *table = changeExt(getLocal(file), "");

		header = getList(rows, 0);

		foreach(header, column, colidx)
		{
			autoList_t *values = GetCellVals(rows, colidx);

			cout("%s.%s\n", table, column);

			cout("\t%s\n", refLine(values, 0));
			cout("\t%s\n", refLine(values, 1));
			cout("\t%s\n", refLine(values, 2));
			cout("\t%s\n", refLine(values, 3));
			cout("\t%s\n", refLine(values, 4));

			cout("\n");

			releaseDim(values, 1);
		}
		releaseDim(rows, 2);
		memFree(table);
	}
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	hasArgs(0); // for //x options

	Main2(c_dropDir());
}
