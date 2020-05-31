#include <stdio.h>

int main()
{
	int a, r, m, t;
	int v = 0;

	scanf("%d,%d,%d,%d", &a, &r, &m, &t);

	for(; m; m--)
	{
		v += a;

		if(t < v)
			v -= r;

		if(75 < v)
			break;
	}
	if(!m)
	{
		if(v < 0)
			v = 0;

		printf("%d\n", v);
	}
	else
		printf("good bye\n");
}
