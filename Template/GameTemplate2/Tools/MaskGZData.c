#include "C:\Factory\Common\all.h"

static void MaskGZData(autoBlock_t *fileData)
{
	if(getSize(fileData))
	{
		errorCase(getByte(fileData, 0) != 0x1f);
		errorCase(getByte(fileData, 1) != 0x8b);

		setByte(fileData, 0, 'D');
		setByte(fileData, 1, 'D');
	}
}
int main(int argc, char **argv)
{
	// sync > @ MaskGZData_main

	char *file;
	autoBlock_t *fileData;

	errorCase(!argIs("MASK-GZ-DATA"));

	file = nextArg();
	cout("MaskGZData_file: %s\n", file);
	fileData = readBinary(file);
	LOGPOS();
	MaskGZData(fileData);
	LOGPOS();
	writeBinary(file, fileData);
	LOGPOS();
	releaseAutoBlock(fileData);
	LOGPOS();

	// < sync
}
