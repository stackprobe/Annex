#include <stdio.h>
#include <stdlib.h>
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

#define TEST_COUNT 1000000
//#define TEST_COUNT 3000000
//#define TEST_COUNT 10000000

//#define ALLOC_NUM_MAX  30000
//#define ALLOC_SIZE_MAX 20000

#define ALLOC_NUM_MAX  20000
#define ALLOC_SIZE_MAX 11000

static void *Ptrs[ALLOC_NUM_MAX];
static size_t P_Sizes[ALLOC_NUM_MAX];
static int P_Chars[ALLOC_NUM_MAX];

main()
{
	int count;

	X = (unsigned int)GetTickCount64(); // windows.h

	for(count = 0; count < TEST_COUNT; count++)
	{
		size_t index = Rand32() % ALLOC_NUM_MAX;
		size_t size;
		unsigned char *ptr;
		int chr;
		size_t ndx;

		if(!Ptrs[index])
		{
			size = Rand32() % ALLOC_SIZE_MAX + 1;
			ptr = (unsigned char *)MYLIB_malloc(size);

			if(!ptr)
				exit(1); // fatal

			chr = Rand32() % 255;

			for(ndx = 0; ndx < size - 1; ndx++)
				ptr[ndx] = chr;

			ptr[size - 1] = chr + 1; // ÅŒã‚¾‚¯ + 1

			Ptrs[index] = ptr;
			P_Sizes[index] = size;
			P_Chars[index] = chr;
		}
		else if(Rand32() % 2)
		{
			ptr  = Ptrs[index];
			size = P_Sizes[index];
			chr  = P_Chars[index];

			for(ndx = 0; ndx < size - 1; ndx++)
				if(ptr[ndx] != chr)
					exit(1); // fatal

			if(ptr[size - 1] != chr + 1) // ÅŒã‚¾‚¯ + 1
				exit(1); // fatal

			size = Rand32() % ALLOC_SIZE_MAX + 1;
			ptr = MYLIB_realloc(ptr, size);

			if(!ptr)
				exit(1); // fatal

			chr = Rand32() % 255;

			for(ndx = 0; ndx < size - 1; ndx++)
				ptr[ndx] = chr;

			ptr[size - 1] = chr + 1; // ÅŒã‚¾‚¯ + 1

			Ptrs[index] = ptr;
			P_Sizes[index] = size;
			P_Chars[index] = chr;
		}
		else
		{
			ptr  = Ptrs[index];
			size = P_Sizes[index];
			chr  = P_Chars[index];

			for(ndx = 0; ndx < size - 1; ndx++)
				if(ptr[ndx] != chr)
					exit(1); // fatal

			if(ptr[size - 1] != chr + 1) // ÅŒã‚¾‚¯ + 1
				exit(1); // fatal

			MYLIB_free(ptr);

			Ptrs[index] = NULL;
			P_Sizes[index] = 0;
			P_Chars[index] = 0;
		}
	}
	printf("OK!\n");

	exit(0); // normal end
}
