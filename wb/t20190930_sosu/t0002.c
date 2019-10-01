#include <stdio.h>
#include <limits.h>
#include <math.h>

int main()
{
	int c;
	int d;
	int s;

	printf("2\n");

	for(c = 3; c < INT_MAX; c += 2)
	{
		s = (int)sqrt(c);

		for(d = 3; d <= s; d += 2)
			if(c % d == 0)
				goto next;

		printf("%d\n", c);

	next:;
	}
}
