// bug
#include <stdio.h>

main()
{
	void *a[2];
	void ****************************************************************************************************p;
	void *q;

	p = (void *)a;

	a[0] = p;
	a[1] = p;

	// 何で３回ずつ？

	q = ++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*
		++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*
		++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*
		++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*++*++*++*--*--*--*++*++*p++;

	printf("%p\n%p\n%p\n%p\n%p\n", a + 1, a[0], a[1], p, q);
}
