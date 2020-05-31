#include <stdio.h>

int main()
{
	int a;
	int b;
	int c;

	scanf("%d %d %d", &a, &b, &c);

	if(a * b * c == 175)
		printf("YES\n");
	else
		printf("NO\n");
}
