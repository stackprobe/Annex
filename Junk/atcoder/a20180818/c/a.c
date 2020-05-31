#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	char s[101];
	__int64 k;
	int i;

	scanf("%s", s);
	scanf("%I64d", &k);

	for(i = 0; s[i]; i++)
		if(s[i] != '1')
			break;

	cout("%c\n", i < k ? s[i] : '1');
}
