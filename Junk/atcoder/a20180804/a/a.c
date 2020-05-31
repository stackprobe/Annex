#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	char s[11];

	scanf("%s", s);

	if(startsWith(s, "MUJIN"))
		cout("Yes\n");
	else
		cout("No\n");
}
