#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int a;
	int b;

	scanf("%d", &a);
	scanf("%d", &b);

	printf("%s\n", a & b & 1 ? "Yes" : "No");
}
