#include <stdio.h>
#include <malloc.h>

	#define N 1875000000 // 3.0E+10 / 16
//	#define N 187500000
//	#define N 18750000

#define N_P13 15015

int main()
{
	unsigned char *t = (unsigned char *)malloc(N);
	__int64 c;
	__int64 d;
	int i;

	if(!t)
		return; // fatal

	memset(t, 0x00, N_P13);

	printf("2\n");

	for(c = 3; c <= 13; c += 2)
	{
		if(c != 9)
		{
			printf("%lld\n", c);

			for(d = c; d / 16 < N_P13; d += c * 2)
			{
				t[d / 16] |= 1 << d / 2 % 8;
			}
		}
	}
	for(i = N_P13; i + N_P13 <= N; i += N_P13)
	{
		memcpy(t + i, t, N_P13);
	}
	for(; i < N; i++)
	{
		t[i] = t[i - N_P13];
	}
	for(c = 17; c / 16 < N; c += 2)
	{
		if(!(t[c / 16] & (1 << c / 2 % 8)))
		{
			printf("%lld\n", c);

			for(d = c * c; d / 16 < N; d += c * 2)
			{
				t[d / 16] |= 1 << d / 2 % 8;
			}
		}
	}
	free(t);
}
