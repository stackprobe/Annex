#include "C:\Factory\Common\all.h"

#define N_MAX 1000

static int N;
static int A[N_MAX];
static int T[N_MAX];

static int Teams[N_MAX];
static int TCount;

static __int64 Ans;

static void Found(void)
{
#if 0
	int i;

	for(i = 0; i < N; i++)
		cout("\t%d", T[i]);

	cout("\n");
#endif

	Ans++;
}
static int CanEnterTeam(int i)
{
	return Teams[T[i]] < A[i];
}
static void EnterTeam(int i)
{
	if(!Teams[T[i]]++)
		TCount++;
}
static void LeaveTeam(int i)
{
	if(!--Teams[T[i]])
		TCount--;
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
static int ACompDesc(const void *p1, const void *p2)
{
	int a = *(int *)p1;
	int b = *(int *)p2;

	return b - a; // ~‡
}
int main(int argc, char **argv)
{
	int i;

	scanf("%d", &N);

	for(i = 0; i < N; i++)
		scanf("%d", A + i);

	qsort(A, N, sizeof(A[0]), ACompDesc); // CanEnterTeam()‚ÅŠù‘¶ƒƒ“ƒo[‚ÌA[x]‚ð”»’è‚µ‚Ä‚¢‚È‚¢‚½‚ßB

	Search();

	cout("%I64d\n", Ans % 998244353);
}
