#include <stdio.h>
#include <string.h>

int main()
{
	char kw[11];
	char stws[56];
	int kwLen;
	char *p;
	char *q;

	stws[0] = ' ';

	gets(kw);
	gets(stws + 1);

	kwLen = strlen(kw);
	p = stws + 1;

	while(q = strstr(p, kw))
	{
		*q = '\0';
		printf("%s", p);

		if(q[-1] == ' ' && (q[kwLen] == ' ' || !q[kwLen]))
			printf("[%s]", kw);
		else
			printf("=%s=", kw);

		p = q + kwLen;
	}
	printf("%s\n", p);
}
