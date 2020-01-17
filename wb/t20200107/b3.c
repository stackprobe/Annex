// bug
#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	static void *a[2];
	static void ****************************************************************************************************p;
	static void *q;
	static uint i;

	for(i = 0; i < 12; i++)
	{
		p = (void *)a;

		a[0] = p;
		a[1] = p + 1;

		switch(i)
		{
		case  0: q =                 p; break; //  0 : 0 4 0 0
		case  1: q =                *p; break; //  1 : 0 4 0 0
		case  2: q =              ++*p; break; //  2 : 4 4 0 4
		case  3: q =             *++*p; break; //  3 : 4 4 0 4
		case  4: q =           --*++*p; break; //  4 : 4 0 0 0
		case  5: q =          *--*++*p; break; //  5 : 4 0 0 4
		case  6: q =        --*--*++*p; break; //  6 : 0 0 0 0
		case  7: q =       *--*--*++*p; break; //  7 : 0 0 0 0
		case  8: q =     ++*--*--*++*p; break; //  8 : 4 0 0 4
		case  9: q =    *++*--*--*++*p; break; //  9 : 4 0 0 0
		case 10: q =  ++*++*--*--*++*p; break; // 10 : 4 0 0 4   <--- 4 4 0 4 ‚¶‚á‚ËH
		case 11: q = *++*++*--*--*++*p; break; // 11 : 4 0 0 0
		}

//		cout("%u\n%p\n%p\n%p\n%p\n%p\n\n", i, a, a[0], a[1], p, q);
		cout("%2u : %u %u %u %u\n"
			,i
			,(uint)a[0] - (uint)a
			,(uint)a[1] - (uint)a
			,(uint)p - (uint)a
			,(uint)q - (uint)a
			);
	}
}
