#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint c = 1;
	uint n;
	uint d;

//	cout("2\n");

	for(n = 3; n < 1000000; n += 2)
	{
		for(d = 3; d <= n / 3; d += 2)
			if(n % d == 0)
				goto candiv;

//		cout("%u\n", n);
		c++;
	candiv:;
	}
	cout("%u\n", c);
}
