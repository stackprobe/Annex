#include <stdio.h>

int main()
{
	int c;
	int d;

	for(c = 1; c <= 100; c++)
	{
		d = 1;

		if(c % 2 == 0) d += 2;
		if(c % 3 == 0) d += 3;
		if(c % 5 == 0) d += 5;

		while(d--)
			printf("]");

		printf("\n");
	}
}
