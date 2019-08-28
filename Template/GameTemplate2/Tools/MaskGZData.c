#include "C:\Factory\SubTools\libs\MakeGZData.h"

void MaskGZData(autoBlock_t *fileData)
{
	if(getSize(fileData))
	{
		errorCase(getByte(fileData, 0) != 0x1f);
		errorCase(getByte(fileData, 1) != 0x8b);

		setByte(fileData, 0, 'D');
		setByte(fileData, 1, 'D');
	}
}
