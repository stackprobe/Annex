#include <stdio.h>
#include <limits.h>

int main()
{
	int c;
	int d;

	for(c = 2; c < INT_MAX; c++)
	{
		for(d = 2; d < c; d++)
			if(c % d == 0)
				goto next;

		printf("%d\n", c);

	next:;
	}
}
