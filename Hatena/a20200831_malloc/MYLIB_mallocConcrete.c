#include <stddef.h>
#include "MYLIB_mallocConcrete.h"

#define LINEAR_SIZE 500000000 // MEM_ALIGN �̔{���ł��邱�ƁB
#define MEM_ALIGN 4

static char Linear[LINEAR_SIZE];
static size_t NextPos;

void *MYLIB_malloc(size_t size)
{
	void *ptr;

	if(LINEAR_SIZE - NextPos < size)
		return NULL;

	ptr = Linear + NextPos;
	NextPos += ((size + MEM_ALIGN - 1) / MEM_ALIGN) * MEM_ALIGN;
	return ptr;
}
void *MYLIB_realloc(void *ptr, size_t size)
{
	return NULL; // �g�p���Ă��Ȃ������B
}
void MYLIB_free(void *ptr)
{
	// �������Ȃ��B
}
