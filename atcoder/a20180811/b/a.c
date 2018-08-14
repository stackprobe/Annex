#include "C:\Factory\Common\all.h"

int main()
{
	int n;

	scanf("%d", &n);

	for(; 0 <= n && n % 4 != 0; n -= 7);

	cout("%s\n", 0 <= n ? "Yes" : "No");
}
