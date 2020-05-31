#include <stdio.h>

static int N;
static int T;
static int IP_Ret;

static void CkPair(int a, int b)
{
	IP_Ret = IP_Ret || a == N && b == T || a == T && b == N;
}
static int IsPair(void)
{
	int i;

	IP_Ret = 0;

	for(i = 1; i <= 15; i++)
		CkPair(i, i + 5);

	for(i = 1; i <= 4; i++)
	{
		CkPair(i, i + 1);
		CkPair(i + 6, i + 10);
		CkPair(i + 15, i + 16);
	}
	CkPair(1, 5);
	CkPair(6, 15);
	CkPair(16, 20);

	return IP_Ret;
}
int main()
{
	int f = 0;

	scanf("%d", &N);

	for(T = 1; T <= 20; T++)
	{
		if(IsPair())
		{
			if(f)
				printf(",");
			else
				f = 1;

			printf("%d", T);
		}
	}
	printf("\n");
}
