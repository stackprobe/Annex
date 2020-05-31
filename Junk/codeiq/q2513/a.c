#include <stdio.h>

int main()
{
	int n;
	int a[100];
	int i;
	int j;

	scanf("%d", &n);

	for(i = 0; i < n; i++)
		scanf("%d", a + i);

	for(j = 1; j < n; j++)
	for(i = 0; i < j; i++)
	{
		if(a[i] + a[j] == 256)
		{
			printf("yes\n");
			return 0;
		}
	}
	printf("no\n");
	return 0;
}
