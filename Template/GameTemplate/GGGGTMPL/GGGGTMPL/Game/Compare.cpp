/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int isFilled(void *block, int fillValue, int size)
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; index < size; index++)
		if(((uchar *)block)[index] != fillValue)
			return 0;

	return 1;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int isSame(autoList<uchar> *binData1, autoList<uchar> *binData2)
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	if(binData1->GetCount() != binData2->GetCount())
		return 0;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; index < binData1->GetCount(); index++)
		if(binData1->GetElement(index) != binData2->GetElement(index))
			return 0;

	return 1;
}
