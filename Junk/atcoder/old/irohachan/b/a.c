#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define N_MAX 100
#define L_MAX 100

static Si[N_MAX][L_MAX + 1];

int main()
{
	int n;
	int i;

	scanf("%d %d", &n, &i);

	for(i = 0; i < n; i++)
		scanf("%s", Si + i);

	qsort(Si, n, sizeof(Si[0]), strcmp);

	for(i = 0; i < n; i++)
		printf("%s", Si[i]);

	printf("\n");
}
