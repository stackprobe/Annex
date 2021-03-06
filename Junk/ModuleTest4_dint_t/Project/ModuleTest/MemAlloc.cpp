#include "all.h"

void *memAlloc(int size)
{
	errorCase(size < 0);
	void *block = malloc(size);
	errorCase(!block);

	return block;
}
void *memRealloc(void *block, int size)
{
	errorCase(size < 0);
	block = realloc(block, size);
	errorCase(!block);

	return block;
}
void memFree(void *block)
{
	free(block);
}
