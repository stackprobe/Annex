#include <stdio.h>
#include <string.h>
#include <math.h>

int main()
{
	char s[21];

	while(scanf("%s", s) != EOF)
	{
		char *p = s;

		while(*p)
		{
			if(abs(*p - p[1]) == 1)
			{
				strcpy(p, p + 2);

				if(s < p)
					p--;
			}
			else
				p++;
		}
		printf("%s\n", s);
	}
	return 0;
}
