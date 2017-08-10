#include <stdio.h>

int main()
{
	int n;

	for(n = 1; n <= 100; n++)
	{
		int f = 1;

		if(n % 3 == 0)
		{
			printf("Fizz");
			f = 0;
		}
		if(n % 5 == 0)
		{
			printf("Buzz");
			f = 0;
		}
		if(f)
			printf("%d", n);

		printf("\n");
	}
}
