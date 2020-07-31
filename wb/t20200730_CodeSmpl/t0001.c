#include "C:\Factory\Common\all.h"

static void Test01(void)
{
	uint count;

	for(count = 0; count < IMAX; count++)
	{
		while(hasKey())
			if(getKey() == 0x1b) // ? ESC_‰Ÿ‰º -> ’†Ž~
				goto endLoop;

		cout("%u\n", count);
	}
endLoop:;
}
int main(int argc, char **argv)
{
	Test01();
}
