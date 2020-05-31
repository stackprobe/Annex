/*

【例】
入力	出力	メモリイメージ(.はパディング )
SDIL	20	SS......DDDDDDDDIIIILLLLLLLL
CM	16	C...............MMMMMMMMMMMMMMMM
CSILDC	24	C.SSIIIILLLLLLLLDDDDDDDDC

*/

#include <stdio.h>

static int AliNext;
static int AliPos;

static Align(int a, int s)
{
	AliNext += a - 1;
	AliNext /= a;
	AliNext *= a;
	AliPos = AliNext;
	AliNext += s;
}
int main()
{
	int c;

	while((c = fgetc(stdin)) != EOF)
	{
		switch(c)
		{
		case 'C': Align( 1,  1); break;
		case 'S': Align( 2,  2); break;
		case 'I': Align( 4,  4); break;
		case 'L': Align( 4,  8); break;
		case 'D': Align( 8,  8); break;
		case 'M': Align(16, 16); break;
		}
	}
	printf("%d\n", AliPos);
}
