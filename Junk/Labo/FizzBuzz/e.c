#include <stdio.h>

int main()
{
	int n;

	for(n = 1; n <= 100; n++)
	{
		printf("%d\n\0Fizz\n\0______FizzBuzz\n" + (n % 3 + 1 >> 1 ^ 1 ^ ((n % 5 + 3 >> 2 ^ 1) * 5)) * 4, n);
	}
}
