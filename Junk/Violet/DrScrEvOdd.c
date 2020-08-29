#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csv.h"

int main(int argc, char **argv)
{
	autoList_t *csv = readCSVFileTR(nextArg());
	uint x;
	uint y;

	for(x = 0; x < 9; x++)
	for(y = 0; y < 9; y++)
	{
		uint value = toValue(refLine(refList(csv, y), x));
		uint eo;

		switch(value)
		{
		case 0: eo = 0; break;
		case 1: eo = 7; break;
		case 2: eo = 8; break;

		default:
			error();
		}
		if(eo)
			cout("%u: (%u, %u) (%u, %u)\n", eo, x, y, x + 1, y + 1);
	}
}
