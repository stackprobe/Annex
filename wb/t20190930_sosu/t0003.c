#include <stdio.h>
#include <malloc.h>

#define N 100000001 // 10^8 + 1

int main()
{
	char *t = (char *)malloc(N);
	int c;
	int d;

	if(!t)
		return; // fatal

	memset(t, 0x00, N);

	for(c = 2; c < N; c++)
	{
		if(!t[c])
		{
			printf("%d\n", c);

			for(d = c * 2; d < N; d += c)
			{
				t[d] = 1;
			}
		}
	}
	free(t);
}
