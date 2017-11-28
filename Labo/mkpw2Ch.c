#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	char *outFile = makeTempPath(NULL);
	char *outLine;
	autoList_t *renCnts = newList();
	uint index;

	LOGPOS();

	while(!waitKey(0))
	{
		coExecute_x(xcout("C:\\Factory\\Tools\\mkpw.exe /-V > \"%s\"", outFile));
		outLine = readFirstLine(outFile);

		for(index = 0; outLine[index]; index++)
			if(outLine[index] == outLine[index + 1])
				putElement(renCnts, index, refElement(renCnts, index) + 1);

		memFree(outLine);
		removeFile(outFile);

		for(index = 0; index < getCount(renCnts); index++)
		{
			if(index)
				cout(" ");

			cout("%u", getElement(renCnts, index));
		}
		cout("\n");
	}
	memFree(outFile);
	releaseAutoList(renCnts);
}
