#include <stdio.h>

int main()
{
	int n;

	while(scanf("%d", &n) != EOF)
	{
		int i;
		int m = 9;
		int x = 1;
		int z = 0;

		for(i = 0; i < 4; i++)
		{
			int v = n % 10;

			n /= 10;

			if(v)
			{
				m = m < v ? m : v;
				x = x < v ? v : x;
			}
			else
				z = 1;
		}
		printf("%d\n", z ? m : x);
	}
	return 0;
}
