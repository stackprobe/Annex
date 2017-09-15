#include <stdio.h>

static int AnsCnt;

static void NextV(int v, int b, int n, int m)
{
	if(m)
	{
		int w = v | (1 << b);

		if(w <= n)
		{
			NextV(w, b + 1, n, m - 1);
			NextV(v, b + 1, n, m);
		}
	}
	else
		AnsCnt++;
}
int main()
{
	int n;
	int m;

	scanf("%d", &n);
	scanf("%d", &m);

	NextV(0, 0, n, m);

	printf("%d\n", AnsCnt);
	return 0;
}
