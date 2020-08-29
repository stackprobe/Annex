#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	ULARGE_INTEGER a;
	ULARGE_INTEGER f;
	ULARGE_INTEGER t;
	uint64 aa;
	uint64 ff;
	uint64 tt;

	GetDiskFreeSpaceEx(nextArg(), &a, &f, &t);

	aa = (uint64)a.LowPart | (uint64)a.HighPart << 32;
	ff = (uint64)f.LowPart | (uint64)f.HighPart << 32;
	tt = (uint64)t.LowPart | (uint64)t.HighPart << 32;

	cout("%I64u\n", aa);
	cout("%I64u\n", ff);
	cout("%I64u\n", tt);
}
