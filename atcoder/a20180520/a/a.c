#include <stdio.h>

main()
{
	int a;
	int b;
	int c;
	__int64 k;
	int ans;

	scanf("%d", &a);
	scanf("%d", &b);
	scanf("%d", &c);
	scanf("%I64d", &k);

	ans = a - b;

	if(k & 1)
		ans *= -1;

	printf("%d\n", ans);
}
