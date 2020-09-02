#include <stdio.h>
#include <windows.h>
#include "MYLIB_malloc.h"

// ---- Rand ----

static unsigned int X;

static unsigned int Rand32(void)
{
	// Xorshift-32

	X ^= X << 13;
	X ^= X >> 17;
	X ^= X << 5;

	return X;
}

// ----

#define TEST_COUNT 10000000 // テスト回数

#define ALLOC_NUM_MAX  14000 // 実際に確保されるメモリブロックの最大数は、この 2 / 3 くらいになる。
#define ALLOC_SIZE_MAX 10000

static void *Ptrs[ALLOC_NUM_MAX];

main()
{
	int count;

	X = (unsigned int)GetTickCount64(); // windows.h

	for(count = 0; count < TEST_COUNT; count++)
	{
		size_t index = Rand32() % ALLOC_NUM_MAX;
		size_t size;

		if(!Ptrs[index])
		{
			void *ptr = MYLIB_malloc(size = Rand32() % ALLOC_SIZE_MAX + 1);

			if(!ptr)
				exit(0); // fatal

			memset(ptr, Rand32() & 0xff, size); // 適当な値でブロック全体を初期化

			Ptrs[index] = ptr;
		}
		else if(Rand32() % 2)
		{
			void *ptr = MYLIB_realloc(Ptrs[index], size = Rand32() % ALLOC_SIZE_MAX + 1);

			if(!ptr)
				exit(0); // fatal

			memset(ptr, Rand32() & 0xff, size); // 適当な値でブロック全体を初期化

			Ptrs[index] = ptr;
		}
		else
		{
			MYLIB_free(Ptrs[index]);
			Ptrs[index] = NULL;
		}
	}
	printf("OK!\n");
}
