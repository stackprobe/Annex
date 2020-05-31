#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int a;
	char s[101];
	char *p;

	scanf("%d", &a);
	scanf("%s", s);

	for(p = s; *p && a; p++)
	{
		if(*p == '+')
			a++;
		else
			a--;
	}
	if(a)
		cout("No\n");
	else
		cout("Yes\n");
}
