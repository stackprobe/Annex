#pragma once

#include <malloc.h>

void *MYLIB_malloc(size_t size);
void *MYLIB_realloc(void *ptr, size_t size);
void MYLIB_free(void *ptr);
