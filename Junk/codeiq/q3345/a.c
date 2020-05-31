#include <stdio.h>

#define PrintChr(zc) \
	printf("%c", 'a' + (zc) - 1)

int main()
{
	int ch;
	int fndOne = 0;
	int zc = 0;

	while((ch = fgetc(stdin)) != EOF)
	{
		if(!fndOne)
		{
			fndOne = ch == '1';
			zc = 0;
			continue;
		}
		switch(ch)
		{
		case '0':
			zc++;
			break;

		case '1':
			PrintChr(zc);
			zc = 0;
			break;

		case '\n':
			PrintChr(zc);
			fndOne = 0;
			printf("\n");
			break;
		}
	}
	if(fndOne)
		PrintChr(zc);
}
