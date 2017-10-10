// use int

#include "C:\Factory\Common\all.h"

static int N;
static int K;
static int DMap[10];
static int MinND;

static int NextND(int i)
{
	for(; i < 10; i++)
		if(!DMap[i])
			return i;

	return -1;
}
int main(int argc, char **argv)
{
	char *s;
	int i;
	int d;

	scanf("%d %d", &N, &K);

	for(i = 0; i < K; i++)
	{
		scanf("%d", &d);

		DMap[d] = 1;
	}
	for(i = 0; ; i++)
		if(!DMap[i])
			break;

	MinND = i;
	s = xcout("%d", N);

	for(i = 0; s[i]; i++)
		if(DMap[s[i] - '0'])
			break;

	if(s[i])
	{
		for(; ; )
		{
			int nd = NextND(s[i] - '0');

			if(nd != -1)
			{
				s[i] = nd + '0';

				while(s[++i])
					s[i] = MinND + '0';

				break;
			}
			i--;

			if(i < 0)
			{
				int nz = MinND;

				if(!nz)
					nz = NextND(1);

				cout("%c", nz + '0');

				for(i = 0; s[i]; i++)
					cout("%c", MinND + '0');

				cout("\n");
				goto endFunc;
			}
		}
	}
	cout("%s\n", s);

endFunc:
	memFree(s);
}
