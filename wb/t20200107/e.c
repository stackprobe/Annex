#include <stdio.h>

main()
{
	void *a[2];
	void ****p;

	p = (void *)a;

	a[0] = p;
	a[1] = p;

	++*++*p++; --*--*p--;
	++*++*p++; --*--*p--;
	++*++*p++; --*--*p--;
	++*++*p++; --*--*p--;

	printf("%p\n%p\n%p\n%p\n", a, a[0], a[1], p);
}
