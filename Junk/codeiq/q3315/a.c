#include <stdio.h>

#define N_MAX 10

static int Conds[N_MAX];
static int Nexts[N_MAX];
static int N;

static int Ps[N_MAX];

static int CheckRing(int from)
{
	int i = from;
	int cond = 0;

	memset(Ps, 0, sizeof(Ps));

	while(!Ps[i])
	{
		cond ^= Conds[i];
		Ps[i] = 1;
		i = Nexts[i];
	}
	return i != from || !cond;
}
int main()
{
	int i;

	for(i = 0; ; i++)
	{
		int next;
		char sCond[6];
		char dummy[4];

		if(scanf("%d %s %s %s", &next, dummy, sCond, dummy) == EOF)
			break;

		Conds[i] = sCond[0] == 'f' ? 1 : 0;
		Nexts[i] = next - 1;
	}
	N = i;

	for(i = 0; i < N; i++)
		if(!CheckRing(i))
			break;

	printf("%d\n", i < N ? -1 : 0);
}
