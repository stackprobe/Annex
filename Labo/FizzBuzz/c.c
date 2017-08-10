#include <stdio.h>

#define FIZZU 3
#define BUZZU 5

int main()
{
	int n;

	for(n = 1; n <= 100; n++)
	{
		if(n % FIZZU == 0)
		{
			if(n % BUZZU == 0)
				printf("FizzBuzz\n");
			else
				printf("Fizz\n");
		}
		else if(n % BUZZU == 0)
			printf("Buzz\n");
		else
			printf("%d\n", n);
	}
}
