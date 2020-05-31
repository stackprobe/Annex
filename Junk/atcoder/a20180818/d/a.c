#include "C:\Factory\Common\all.h"

main()
{
	static int m1[501][501]; // [1Å`500][1Å`500]
	static int m2[501][501]; // [1Å`500][1Å`500]
	int n;
	int m;
	int q;
	int c;
	int l;
	int r;
	int ll;
	int rr;

	scanf("%d", &n);
	scanf("%d", &m);
	scanf("%d", &q);

	for(c = 0; c < m; c++)
	{
		scanf("%d", &l);
		scanf("%d", &r);

		m1[l][r]++;
	}
	for(l = 1; l <= n; l++)
	for(r = l; r <= n; r++)
	{
		int sum = 0;

		for(ll = l;  ll <= r; ll++)
		for(rr = ll; rr <= r; rr++)
		{
			sum += m1[ll][rr];
		}
		m2[l][r] = sum;
	}
	for(c = 0; c < q; c++)
	{
		scanf("%d", &l);
		scanf("%d", &r);

		cout("%d\n", m2[l][r]);
	}
}
