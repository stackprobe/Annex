#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Performance.h"

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

#define TEST_COUNT 30000

#define HEAP_MAX 1250000000 // 1.25 GB

//#define ALLOC_NUM_MAX 30000
//#define ALLOC_NUM_MAX 10000
#define ALLOC_NUM_MAX 3000
//#define ALLOC_NUM_MAX 1000

#define ALLOC_SIZE_MAX ((uint)(HEAP_MAX * 2.0 / (ALLOC_NUM_MAX * 0.666)))

static void *Ptrs[ALLOC_NUM_MAX];

main()
{
	uint64 tmFreq = GetPerformanceFrequency();
	uint64 stTm;
	uint64 edTm;
	int count;

	X = (unsigned int)GetTickCount64();

	for(count = 0; count < TEST_COUNT; count++)
	{
		size_t index = Rand32() % ALLOC_NUM_MAX;
		size_t size;
		void *ptr;

		if(!Ptrs[index])
		{
			stTm = GetPerformanceCounter();
			ptr = malloc(size = Rand32() % ALLOC_SIZE_MAX + 1);
			edTm = GetPerformanceCounter();

			cout("M %.3f\n", (edTm - stTm) / (double)tmFreq);

			errorCase(!ptr);

			memset(ptr, Rand32() & 0xff, size);

			Ptrs[index] = ptr;
		}
		else if(Rand32() % 2)
		{
			stTm = GetPerformanceCounter();
			ptr = realloc(Ptrs[index], size = Rand32() % ALLOC_SIZE_MAX + 1);
			edTm = GetPerformanceCounter();

			cout("R %.3f\n", (edTm - stTm) / (double)tmFreq);

			errorCase(!ptr);

			memset(ptr, Rand32() & 0xff, size);

			Ptrs[index] = ptr;
		}
		else
		{
			stTm = GetPerformanceCounter();
			free(Ptrs[index]);
			edTm = GetPerformanceCounter();

			cout("F %.3f\n", (edTm - stTm) / (double)tmFreq);

			Ptrs[index] = NULL;
		}
	}
}
