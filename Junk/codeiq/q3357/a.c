#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

#define N_OUT INT_MAX

static void GetXYByN(int n, int *px, int *py)
{
	if(n < 8)
	{
		switch(n)
		{
		case 0: *px = 0; *py = 0; break;
		case 1: *px = 1; *py = 0; break;
		case 2: *px = 2; *py = 0; break;
		case 3: *px = 2; *py = 1; break;
		case 4: *px = 2; *py = 2; break;
		case 5: *px = 1; *py = 2; break;
		case 6: *px = 0; *py = 2; break;
		case 7: *px = 0; *py = 1; break;
		}
	}
	else
	{
		int x;
		int y;

		GetXYByN(n / 8, px, py);

		*px *= 3;
		*py *= 3;

		GetXYByN(n % 8, &x, &y);

		*px += x;
		*py += y;
	}
}
static int GetNByXY(int x, int y)
{
	int n;

	if(x < 0 || y < 0)
	{
		n = N_OUT;
	}
	else if(x < 3 && y < 3)
	{
		switch(x + y * 3)
		{
		case 0: n = 0; break;
		case 1: n = 1; break;
		case 2: n = 2; break;
		case 3: n = 7; break;
		case 4: n = N_OUT; break;
		case 5: n = 3; break;
		case 6: n = 6; break;
		case 7: n = 5; break;
		case 8: n = 4; break;
		}
	}
	else
	{
		n = GetNByXY(x / 3, y / 3);

		if(n != N_OUT)
		{
			int t = GetNByXY(x % 3, y % 3);

			if(t != N_OUT)
			{
				n *= 8;
				n += t;
			}
			else
				n = N_OUT;
		}
	}
	return n;
}
static int CompInt(const int *a, const int *b)
{
	return *a - *b;
}
int main()
{
	int n;
	int x;
	int y;
	int vs[5];
	int i;

	scanf("%d", &n);

	n--;

	GetXYByN(n, &x, &y);

	vs[0] = GetNByXY(x + 1, y);
	vs[1] = GetNByXY(x - 1, y);
	vs[2] = GetNByXY(x, y + 1);
	vs[3] = GetNByXY(x, y - 1);
	vs[4] = N_OUT;

	qsort(vs, 4, sizeof(int), CompInt);

	for(i = 0; vs[i] != N_OUT; i++)
	{
		if(i)
			printf(",");

		printf("%d", vs[i] + 1);
	}
	printf("\n");
}
