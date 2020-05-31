#include <stdio.h>

int main()
{
	int x;
	int r;
	int c;

	scanf("%d", &x);

	if(x % 2)
	{
		for(r = 0; r < x; r++)
		{
			for(c = 0; c < x; c++)
			{
				printf("%c",
					(r + c) == x / 2 ||
					(r - c) == x / 2 ||
					(c - r) == x / 2 ||
					(r + c) == (x / 2) * 3 ?
					'o' : '.'
					);
			}
			printf("\n");
		}
	}
	else
		printf("invalid\n");
}
