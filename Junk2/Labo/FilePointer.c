#include "C:\Factory\Common\all.h"

static int DoRead(FILE *fp)
{
	int chr;

	cout("%I64u ", getSeekPos(fp));
	cout("%02x ", chr = readChar(fp));
	cout("%I64u\n", getSeekPos(fp));

	return chr;
}
int main(int argc, char **argv)
{
	FILE *fp = fileOpen("FilePointer.c", "rb");

	while(DoRead(fp) != EOF);

	DoRead(fp);
	DoRead(fp);
	DoRead(fp);

	fileSeek(fp, SEEK_SET, getSeekPos(fp) - 3);

	DoRead(fp);
	DoRead(fp);
	DoRead(fp);

	DoRead(fp);
	DoRead(fp);
	DoRead(fp);

	fileSeek(fp, SEEK_CUR, -3);

	DoRead(fp);
	DoRead(fp);
	DoRead(fp);

	DoRead(fp);
	DoRead(fp);
	DoRead(fp);

	fileClose(fp);
}
