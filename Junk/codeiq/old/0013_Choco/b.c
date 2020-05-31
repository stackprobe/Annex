#include "C:\Factory\Common\all.h"

#define N_MAX 6000

static int N;
static int M_Num;
static uchar Members[N_MAX * 2];
static int I;

static void M_Remove(void)
{
	Members[I] = 0;
}
static void M_Next(void)
{
	do
	{
		I++;
		I %= M_Num;
	}
	while(!Members[I]);
}
static int GetRemain(void)
{
	int count = 0;
	int index;

	for(index = 0; index < M_Num; index++)
		if(Members[index])
			count++;

	return count;
}
static int GetRemainIndex(void)
{
	int index;

	for(index = 0; index < M_Num; index++)
		if(Members[index])
			break;

	return index;
}
static void TryGame(void)
{
	M_Num = N * 2;
	memset(&Members, 0xff, sizeof(Members));
	I = 1;

	for(; ; )
	{
		M_Remove();

		errorCase(GetRemain() < 1);

		if(GetRemain() == 1)
			break;

		M_Next();
		M_Next();
	}

	{
		int truth = N - 1;
		int answer = GetRemainIndex();

		cout("N=%d, %d, %d\n", N, truth, answer);

		if(truth == answer)
			cout("š\n");
	}
}
int main(int argc, char **argv)
{
	for(N = 2; N <= N_MAX; N++)
	{
		TryGame();
	}
}
