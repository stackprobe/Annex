#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int n;
	int r = 0;

	scanf("%d", &n);

	for(; n; n--)
	{
		int p;

		scanf("%d", &p);

		if(p % 2 == r)
			break;

		r ^= 1;
	}
	cout("%s\n", n ? "No" : "Yes");
}
