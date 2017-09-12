#include <stdio.h>

int main()
{
	int n;

	while(scanf("%d", &n) != EOF)
	{
		printf("%d\n", n % 2 ? (1999999 / n + 1) / 2 : 0);
	}
	return 0;
}
