/*
	Language == C
*/

#include <stdio.h>

static void PrnKaza(int size)
{
	int r;
	int c;

	for(r = 0; r < size * 2; r++)
	{
		for(c = 0; c < size * 2; c++)
		{
			int rs = r / size;
			int cs = c / size;
			int ri = r % size;
			int ci = c % size;
			int n;

			for(n = (rs * 3) ^ cs; n; n--)
			{
				ri ^= ci;
				ci ^= ri;
				ri ^= ci;
				ri = size - 1 - ri;
			}
			putc(ci <= ri ? '*' : ' ', stdout);
		}
		putc('\n', stdout);
	}
}
int main(int argc, char **argv)
{
	PrnKaza(5);
	PrnKaza(6);
	PrnKaza(7);
}
