// use int

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	double ans;
	int n;
	int m;
	int d;

	scanf("%d", &n);
	scanf("%d", &m);
	scanf("%d", &d);

	// IF 0 <  d : (((n * 2) - (d * 2)) * (n ^ (m - 2)) * (m - 1)) / (n ^ m)
	// IF 0 == d : (  n                 * (n ^ (m - 2)) * (m - 1)) / (n ^ m)

	if(d)
	{
		ans = n - d;
		ans *= 2;
	}
	else
		ans = n;

	ans /= n;
	ans *= m - 1;
	ans /= n;

	cout("%.10f\n", ans);
}
