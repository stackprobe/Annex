#include <stdio.h>

#define N_MAX 100

static int N;
static int K;

static int F;
static int G;

static void FindF(void)
{
	int grps[N_MAX];
	int ahead = 1;
	int i = 1;
	int rem = N - K;

	grps[0] = K;

	for(; ; )
	{
		if(ahead)
		{
			if(!rem)
			{
				F++;
				ahead = 0;
				goto goBack;
			}
			grps[i] = 1;
		}
		else
			grps[i]++;

		ahead = grps[i] <= grps[i - 1] && grps[i] <= rem;

		if(ahead)
		{
			rem -= grps[i];
			i++;
		}
		else
		{
		goBack:
			if(i <= 1)
				break;

			i--;
			rem += grps[i];
		}
	}
}
static void FindG(void)
{
	int grps[N_MAX - 1];
	int ahead = 1;
	int i = 0;
	int rem = N;

	for(; ; )
	{
		if(ahead)
		{
			if(K - 1 <= i)
			{
				G++;
				ahead = 0;
				goto goBack;
			}
			grps[i] = i ? grps[i - 1] : 1;
		}
		else
			grps[i]++;

		ahead = grps[i] * (K - i) <= rem;

		if(ahead)
		{
			rem -= grps[i];
			i++;
		}
		else
		{
		goBack:
			if(!i)
				break;

			i--;
			rem += grps[i];
		}
	}
}
int main()
{
	scanf("%d", &N);
	scanf("%d", &K);

	FindF();
	FindG();

	printf("%d\n", F + G);
}
