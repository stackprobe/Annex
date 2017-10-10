#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint a;
	uint b;
	uint c;

	for(a = 1; a <= 10; a++)
	for(b = 1; b <= 10; b++)
	for(c = 1; c <= 10; c++)
		if(a * b * c == 175)
			cout("%u %u %u\n", a, b, c);
}
