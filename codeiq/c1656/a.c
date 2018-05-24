#include <stdio.h>

#define N_MAX 20000

int main()
{
	char nn[N_MAX + 1];
	int nEnd;
	int nEndRank;
	int span;
	int n;
	int c;
	char tmp[100];

	scanf("%d %d", &nEnd, &span);

	nEndRank = sprintf(tmp, "%d", nEnd);

	memset(nn, 'P', nEnd + 1);

	nn[0] = '0';
	nn[1] = '1';

	for(n = 2; n * n <= nEnd; n++)
		if(nn[n] == 'P')
			for(c = n * n; c <= nEnd; c += n)
				nn[c] = 'C';

	c = 0;

	for(n = 1; n <= nEnd; n++)
	{
		if(c < n)
		{
			if(c)
				printf("\n");

			sprintf(tmp, "%%0%dd-%%0%dd:", nEndRank, nEndRank);
			printf(tmp, n, c + span);
			c += span;
		}
		if(nn[n] == 'P')
			printf("*");
	}
	printf("\n");
}
