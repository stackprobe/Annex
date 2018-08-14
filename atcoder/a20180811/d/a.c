#include "C:\Factory\Common\all.h"

// zantei
// zantei
// zantei

int main()
{
	int n;
	int m;
	static int a[100000];
	int i;
	int j;
	int c;
	int sum;
	__int64 ans = 0;

	scanf("%d", &n);
	scanf("%d", &m);

	for(i = 0; i < n; i++)
		scanf("%d", a + i);

	for(i = 0; i < n; i++)
	for(j = i; j < n; j++)
	{
		sum = 0;

		for(c = i; c <= j; c++)
		{
			sum += a[c];
			sum %= m;
		}
		if(sum == 0)
			ans++;
	}
	cout("%I64d\n", ans);
}
