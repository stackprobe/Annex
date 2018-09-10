#include "C:\Factory\Common\all.h"

static int GetGCD(int a, int b)
{
	int ans = 1;
	int d;
//cout("%d %d -> ", a, b); // test

	if(b < a)
	{
		int tmp = a;
		a = b;
		b = tmp;
	}

	// ‚±‚±‚Å a < b

	errorCase(a < 1); // –³‚¢‚Í‚¸B

	while(a % 2 == 0 && b % 2 == 0)
	{
		ans *= 2;
		a /= 2;
		b /= 2;
	}
	for(d = 3; d <= a; d += 2)
	{
		while(a % d == 0 && b % d == 0)
		{
			ans *= d;
			a /= d;
			b /= d;
		}
	}
//cout("%d\n", ans); // test
	return ans;
}
int main(int argc, char **argv)
{
	autoList_t *xs = newList();
	int n;
	int x;
	int i;
	int ans;

	scanf("%d", &n);

	n++;

	while(n--)
	{
		scanf("%d", &x);

		addElement(xs, (uint)x);
	}

	rapidSort(xs, simpleComp);

	ans = getElement(xs, 1) - getElement(xs, 0);

	foreach(xs, x, i)
	if(2 <= i)
		ans = GetGCD(ans, getElement(xs, i) - getElement(xs, i - 1));

	cout("%d\n", ans);
}
