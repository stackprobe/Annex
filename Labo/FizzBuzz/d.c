#include <stdio.h>

int main()
{
	char *fmts[] =
	{
		"%d\n",
		"Fizz\n",
		"Buzz\n",
		"FizzBuzz\n"
	};
	int n;

	for(n = 1; n <= 100; n++)
	{
		printf(fmts["300102100120100"[n % 15] - '0'], n);
	}
}
