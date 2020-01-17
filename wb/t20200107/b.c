// bug
#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	void *a[2];
	void ****************************************************************************************************p;
	void *q;
	uint i;

	for(i = 0; i < 8; i++)
	{
		p = (void *)(a + 1);

		a[0] = p;
		a[1] = p;

		switch(i)
		{
		case 0: q =           p; break;
		case 1: q =          *p; break;
		case 2: q =        --*p; break;
		case 3: q =       *--*p; break;
		case 4: q =     --*--*p; break;
		case 5: q =    *--*--*p; break;
		case 6: q =  --*--*--*p; break;
		case 7: q = *--*--*--*p; break;
		}

		cout("%u\n%p\n%p\n%p\n%p\n%p\n\n", i, a, a[0], a[1], p, q);
	}
}
