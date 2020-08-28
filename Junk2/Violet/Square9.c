#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *lines = readLines(
//		"Square9.txt"
//		"Square9_2.txt"
		"Square9_3.txt"
		);
//	uint l = 6, t = 0;
	uint l = 0, t = 0;
	uint g;
	uint x;
	uint y;

	cout("%u\n", 9);

	for(g = 1; g <= 9; g++)
	{
		cout("%u\n", 9);
//		cout("%u\n", g);

		for(y = 0; y < getCount(lines); y++)
		for(x = 0; x < strlen(getLine(lines, y)); x++)
		{
			if(getLine(lines, y)[x] == '0' + g)
			{
				cout("%u,%u\n", l + x, t + y);
			}
		}
	}
}
