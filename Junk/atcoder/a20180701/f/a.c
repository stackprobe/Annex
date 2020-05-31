// use int

#include "C:\Factory\Common\all.h"

static int N; // 1 Å` 25000
static int K; // 1 Å` 400
static int M; // 1 Å` N
static int AA[400];   // [0 Å` (M-1)] == 1 Å` K
static int CC[25000]; // [0 Å` (N-1)] == 1 Å` K
static int Ans;

static int IsColorful(int *cc)
{
	int fs[400];
	int i;

	zeroclear(fs);

	for(i = 0; i < K; i++)
		fs[cc[i] - 1] = 1;

	for(i = 0; i < K; i++)
		if(!fs[i])
			return 0;

	return 1;
}
static int IsCC_Colorful(void)
{
	int i;

	for(i = 0; i + K <= N; i++)
		if(IsColorful(CC + i))
			return 1;

	return 0;
}
static int IsAA_Match(int *cc)
{
	int i;

	for(i = 0; i < M; i++)
		if(cc[i] != AA[i])
			return 0;

	return 1;
}
static void FindAA(void)
{
	int i;

	for(i = 0; i + M <= N; i++)
		if(IsAA_Match(CC + i))
			Ans = (Ans + 1) % 1000000007;
}
static void Check(void)
{
	if(IsCC_Colorful())
	{
		FindAA();
	}
}
static void Search(void)
{
	int i = 0;
	int ahead = 1;

	for(; ; )
	{
		if(ahead)
		{
			if(i == N)
			{
				Check();
				i--;
				ahead = 0;
			}
			else
			{
				CC[i] = 1;
				i++;
			}
		}
		else // !ahead
		{
			if(CC[i] == K)
			{
				if(!i)
					break;

				i--;
			}
			else
			{
				CC[i]++;
				i++;
				ahead = 1;
			}
		}
	}
}
int main(int argc, char **argv)
{
	int i;

	scanf("%d", &N);
	scanf("%d", &K);
	scanf("%d", &M);

	errorCase(!m_isRange(N, 1, 25000));
	errorCase(!m_isRange(K, 1, 400));
	errorCase(!m_isRange(M, 1, N));

	for(i = 0; i < M; i++)
	{
		scanf("%d", AA + i);

		errorCase(!m_isRange(AA[i], 1, K));
	}

	Search();

	cout("%d\n", Ans);
}
