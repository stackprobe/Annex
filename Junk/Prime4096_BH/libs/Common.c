#include "all.h"

static uint UIArr[128];

uint *UI4096ToUIArr(UI4096_t val)
{
	FromUI4096(val, UIArr);
	return UIArr;
}
uint GetUIArrSize(uint *arr)
{
	uint i;

	for(i = 128; i; i--)
		if(arr[i - 1])
			break;

	return i;
}
void RandBytes(void *buff, uint size)
{
	getCryptoBytes((uchar *)buff, size);
}
