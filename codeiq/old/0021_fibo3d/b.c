#include "C:\Factory\Common\all.h"

#define N 1000000

static uint List[N];

int main(int argc, char **argv)
{
	uint c;
	uint d;

	List[0] = 0;
	List[1] = 1;

	for(c = 2; c < N; c++)
	{
		List[c] = (List[c - 2] + List[c - 1]) % 1000;
	}

	for(c = 0; ; c++)
	{
		if(
			List[c + 0] == List[N - 2] &&
			List[c + 1] == List[N - 1]
			)
		{
			cout("%u\n", c);
			break;
		}
	}

	d = N - 2;

	while(0 < c)
	{
		if(
			List[c + 0] != List[d + 0] ||
			List[c + 1] != List[d + 1]
			)
		{
			c++;
			break;
		}
		c--;
		d--;
	}
	cout("%u\n", c);

	for(d = c + 1; ; d++)
	{
		if(
			List[c + 0] == List[d + 0] &&
			List[c + 1] == List[d + 1]
			)
		{
			cout("%u\n", d);
			break;
		}
	}

/*
	for(c = 0; c < N; c++)
	{
		cout("%u\n", List[c]);
	}
*/

}
