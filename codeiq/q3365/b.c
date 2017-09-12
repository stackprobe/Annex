#include <stdio.h>

int main()
{
	int wh;

	scanf("%d", &wh);

	if(wh % 2)
	{
		int x;
		int y;

		for(y = 0; y < wh; y++)
		{
			for(x = 0; x < wh; x++)
				printf("%c", x + y == wh - 1 || !y || y == wh - 1 ? 'z' : '.');

			printf("\n");
		}
	}
	else
		printf("invalid\n");
}
