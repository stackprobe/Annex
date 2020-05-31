#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint sq[1000];
	uint i = 2;
	uint c;

	sq[0] = 1;
	sq[1] = 1;

	for(; ; )
	{
		sq[i] = (sq[i - 1] + sq[i - 2]) % 16;

		for(c = 1; c + 1 < i; c++)
			if(sq[c] == sq[i] && sq[c - 1] == sq[i - 1])
				goto found;

		i++;
	}
found:
	for(c = 0; c <= i; c++)
	{
		cout("%u\n", sq[c]);
	}
}
