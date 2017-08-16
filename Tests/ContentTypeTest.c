#include "C:\Factory\Common\all.h"

#define INPUT_DIR \
	/////////////////////////////////////////////////// // $_git:secret

static uchar PRE_TYPE_PTN[] =
{
	0xEC,
	0x61,
	0xD9,
	0x61,
	0xEB,
	0x61,
	0x01,
};

static void Check01(char *file)
{
	autoBlock_t *abFData;
	uchar *fData;
	uint fSize;
	uint index;
	uint typePos;
	uint typeNum = 0;

	cout("%s\n", file);

	abFData = readBinary(file);
	fData = directGetBuffer(abFData);
	fSize = getSize(abFData);

	for(index = 0; index + lengthof(PRE_TYPE_PTN) <= fSize; index++)
	{
		if(!memcmp(fData + index, PRE_TYPE_PTN, lengthof(PRE_TYPE_PTN)))
		{
			typePos = index + lengthof(PRE_TYPE_PTN);
			errorCase(fSize <= typePos);
			errorCase(!m_isRange(fData[typePos], 0x30, 0x33));
			cout("%c\n", fData[typePos]);
			typeNum++;
		}
	}
	errorCase(typeNum != 1);

	cout("OK!\n");

	releaseAutoBlock(abFData);
}
static void Test01(void)
{
	autoList_t *files = lsFiles(INPUT_DIR);
	char *file;
	uint index;

	foreach(files, file, index)
	{
		Check01(file);
	}
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	Test01();
}
