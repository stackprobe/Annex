/*
	fwrite(, 1, SZ, ) ‚Æ fwrite(, SZ, 1, ) ‚Å‚Í count == 1 ‚Ì•û‚ª‘¬‚¢‚Ì‚Å‚ÍH

	-> ‚Ù‚Æ‚ñ‚Ç“¯‚¶
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#define IMAGE_SIZE 15000000

int main(int argc, char **argv)
{
	uchar *image = (uchar *)memAlloc(IMAGE_SIZE);
	uint index;
	int sz1md;
	FILE *fp;

	if(argIs("/C1"))
		sz1md = 0;
	else if(argIs("/S1"))
		sz1md = 1;
	else
		error();

	for(index = 0; index < IMAGE_SIZE; index++)
		image[index] = mt19937_rnd(256);

	fp = fileOpen("C:\\temp\\1.bin", "wb");

	LOGPOS();

	for(index = 0; index < 100; index++)
	{
		if(sz1md)
			errorCase(fwrite(image, 1, IMAGE_SIZE, fp) != IMAGE_SIZE);
		else
			errorCase(fwrite(image, IMAGE_SIZE, 1, fp) != 1);
	}

	LOGPOS();

	fileClose(fp);

	memFree(image);
}
