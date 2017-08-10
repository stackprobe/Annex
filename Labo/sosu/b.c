#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

int main(int argc, char **argv)
{
	uint c = 0;
	uint n;

	for(n = 2; n < 1000000000; n++)
//	for(n = 2; n < 100000000; n++)
//	for(n = 2; n < 1000000; n++)
		if(IsPrime(n))
			c++;

	cout("%u\n", c);
}
