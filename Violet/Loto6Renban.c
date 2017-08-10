#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"
#include "C:\Factory\Common\Options\Progress.h"

#if 1 // mini
#define NUM_RANGE 31
#define NUM_NUM 5
#else // Loto-6
#define NUM_RANGE 43
#define NUM_NUM 6
#endif

#define TEST_MAX 1000000

static uint Counts[NUM_NUM];

static void DoTest(void)
{
	static uint m[NUM_RANGE];
	uint c;
	uint count = 0;

	zeroclear(m);

	for(c = 0; c < NUM_NUM; c++)
	{
		for(; ; )
		{
			uint num = (uint)(getCryptoRand64() % NUM_RANGE);

			if(!m[num])
			{
				m[num] = 1;
				break;
			}
		}
	}
	for(c = 0; c < NUM_RANGE - 1; c++)
		if(m[c] && m[c + 1])
			count++;

	Counts[count]++;
}
static void TestMain(void)
{
	uint c;

	zeroclear(Counts);

	ProgressBegin();

	for(c = 0; c < TEST_MAX; c++)
	{
		if(eqIntPulseSec(1, NULL))
			ProgressRate((double)c / TEST_MAX);

		DoTest();
	}
	ProgressEnd(0);

	for(c = 0; c < NUM_NUM; c++)
	{
		cout("[%u] %.10f\n", c, (double)Counts[c] / TEST_MAX);
	}
}
int main(int argc, char **argv)
{
	uint c;

	for(c = 0; c < 5; c++)
	{
		TestMain();
	}
}
