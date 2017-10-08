#include "C:\Factory\Common\all.h"

#define S_MASK "055b2d160639034d010b"

static void WrPlain(char *rFile)
{
	char *wFile;
	autoBlock_t *fileData;
	autoBlock_t *mask = makeBlockHexLine(S_MASK);
	uint index;

	wFile = getOutFile("plain.txt");
	wFile = toCreatableTildaPath(wFile, IMAX);

	fileData = readBinary(rFile);

	for(index = 0; index < getSize(fileData); index++)
	{
		b(fileData)[index] ^= b(mask)[index % getSize(mask)];
	}
	writeBinary_cx(wFile, fileData);
	memFree(mask);
}
int main(int argc, char **argv)
{
	WrPlain("C:\\var\\koumajouSaveData\\savedata_st8_extra_hiScore=55956.dat");
	WrPlain("C:\\var\\koumajouSaveData\\savedata_st8_extra_hiScore=172177.dat");
	WrPlain("C:\\var\\koumajouSaveData\\savedata_st8_noExtra_hiScore=0.dat");

	openOutDir();
}
