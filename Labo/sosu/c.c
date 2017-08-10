#include "C:\Factory\Common\all.h"

#define NS_SZ 1000000000
//#define NS_SZ 100000000
//#define NS_SZ 1000000

int main(int argc, char **argv)
{
	static uchar ns[NS_SZ];
	uint c = 1;
	uint n;
	uint i;

	for(n = 3; n * n < NS_SZ; n += 2)
		if(ns[n] == 0)
			for(i = n * n; i < NS_SZ; i += n * 2)
				ns[i] = 1;

	for(n = 3; n < NS_SZ; n += 2)
		if(ns[n] == 0)
			c++;

	cout("%u\n", c);
}
