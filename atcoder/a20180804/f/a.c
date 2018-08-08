#include "C:\Factory\Common\all.h"

#define N_MAX 1000

static int N;
static int A[N_MAX];
static int T[N_MAX];

static int Teams[N_MAX][N_MAX];
static int TAmounts[N_MAX];
static int TCount;

static __int64 Ans;

static void Found(void)
{
	Ans++;
}
static int CanEnterTeam(int i)
{
	return 0; // TODO
}
static void EnterTeam(int i)
{
	// TODO
}
static void LeaveTeam(int i)
{
	// TODO
}
static void Search(void)
{
	int ahead = 1;
	int i = 0;

	for(; ; )
	{
		if(ahead)
		{
			if(N <= i)
			{
				Found();
				goto retreat;
			}
			T[i] = 0;
		}
		else
		{
			if(i < 0)
				break;

			LeaveTeam(i);

			T[i]++;
		}

		for(; ; )
		{
			if(TCount < T[i])
			{
			retreat:
				ahead = 0;
				i--;
				break;
			}
			if(CanEnterTeam(i))
			{
				EnterTeam(i);

				ahead = 1;
				i++;
				break;
			}
			T[i]++;
		}
	}
}
int main(int argc, char **argv)
{
	int i;

	scanf("%d", &N);

	for(i = 0; i < N; i++)
		scanf("%d", A + i);

	Search();

	cout("%I64d\n", Ans % 998244353);
}
