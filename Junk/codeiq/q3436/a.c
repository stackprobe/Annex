#include <stdio.h>

#define PTN_MAX 32768

static int Masu;
static int Ptn;
static int Ptns[PTN_MAX + 1];

static void Search(int i, int val, int m)
{
	if(i < Masu)
	{
		for(; val <= 9; val++)
		{
			int mm = m * val;

			if(PTN_MAX < mm)
				break;

			Search(i + 1, val + 1, mm);
		}
	}
	else
		Ptns[m]++;
}
int main()
{
	int ans = 0;
	int i;

	scanf("%d %d", &Masu, &Ptn);

	Search(0, 1, 1);

	for(i = 1; i <= PTN_MAX; i++)
		if(Ptns[i] == Ptn)
			ans++;

	printf("%d\n", ans);
}
