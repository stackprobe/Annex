#include <stdio.h>

int main()
{
	int x;

	scanf("%d", &x);

	if(x == 1)
	{
		printf("z\n");
	}
	else if(x % 2)
	{
		int i;
		int c;

		for(c = 0; c < x; c++)
			printf("z");

		printf("\n");

		for(i = x - 2; i; i--)
		{
			for(c = 0; c < x; c++)
				printf("%c", c == i ? 'z' : '.');

			printf("\n");
		}
		for(c = 0; c < x; c++)
			printf("z");

		printf("\n");
	}
	else
		printf("invalid\n");
}
