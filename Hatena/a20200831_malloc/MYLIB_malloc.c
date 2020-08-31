#include "MYLIB_malloc.h"

void *MYLIB_malloc(size_t size)
{
	return malloc(size);
}
void *MYLIB_realloc(void *ptr, size_t size)
{
	return realloc(ptr, size);
}
void MYLIB_free(void *ptr)
{
	free(ptr);
}
