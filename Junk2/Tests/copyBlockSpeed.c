#include "C:\Factory\Common\all.h"

#define COPY_SIZE 100000000

int main(int argc, char **argv)
{
	char *a = memAlloc(COPY_SIZE);
	char *b = memAlloc(COPY_SIZE);
	int c;

	for(c = 3; c; c--)
	{
		LOGPOS();

		copyBlock(a, a + 1, COPY_SIZE - 1);

		LOGPOS();
	}
	memFree(a);
	memFree(b);
}
