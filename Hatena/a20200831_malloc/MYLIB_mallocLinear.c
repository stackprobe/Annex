#include "MYLIB_mallocLinear.h"

#define BLOCK_NUM  10000
#define BLOCK_SIZE 10000

static char Linear[BLOCK_NUM * BLOCK_SIZE];
static void *Blocks[BLOCK_NUM];
static size_t BlockCount = BLOCK_NUM;

#define IsLinearBlock(ptr) \
	(Linear <= (ptr) && (ptr) < Linear + sizeof(Linear))

static void InitBlocks(void)
{
	size_t index;

	for(index = 0; index < BLOCK_NUM; index++)
		Blocks[index] = Linear + index * BLOCK_SIZE;
}
void *MYLIB_malloc(size_t size)
{
	if(size <= BLOCK_SIZE && BlockCount)
	{
		if(!Blocks[0])
			InitBlocks();

		return Blocks[--BlockCount];
	}
	else
		return malloc(size);
}
void *MYLIB_realloc(void *ptr, size_t size)
{
	void *ptrNew;

	if(!IsLinearBlock(ptr))
		return realloc(ptr, size);

	if(size <= BLOCK_SIZE)
		return ptr;

	ptrNew = malloc(size);

	if(!ptrNew)
		return NULL;

	memcpy(ptrNew, ptr, size);
	return ptrNew;
}
void MYLIB_free(void *ptr)
{
	if(IsLinearBlock(ptr))
		Blocks[BlockCount++] = ptr;
	else
		free(ptr);
}
