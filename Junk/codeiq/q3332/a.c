#include <stdio.h>

#define SQCYC 24

static int Sq[SQCYC];

static void MkSq(void)
{
	int i;

	Sq[0] = 1;
	Sq[1] = 1;

	for(i = 2; i < SQCYC; i++)
	{
		Sq[i] = (Sq[i - 1] + Sq[i - 2]) % 16;
	}
}
int main()
{
	int n;

	MkSq();

	while(scanf("%d", &n) != EOF)
	{
		printf("%d\n", Sq[(n - 1) % SQCYC]);
	}
}
