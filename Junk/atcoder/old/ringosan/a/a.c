#include <stdio.h>

int main()
{
	char s[51];
	scanf("%s", s);
	s[strlen(s) - 8] = '\0';
	printf("%s\n", s);
}
