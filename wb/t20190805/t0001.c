#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Stopwatch.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static void Test01(uint mode, uint count)
{
	autoList_t *lapTimes = newList();
	uint c;

	cout("mode: %u, count: %u\n", mode, count);

	for(c = 0; c < 10; c++)
	{
		autoList_t *lines = newList();
		uint index;

		for(index = 0; index < count; index++)
		{
			addElement(lines, (uint)(mt19937_rnd(100) < 50 ? "ALPHA" : "BETA"));
		}

		SW_Clear(lapTimes);
		SW_Lap(lapTimes);

		if(mode == 1)
		{
			for(index = 0; index < getCount(lines); index++)
			{
				if(*(char *)e_(lines)[index] == 'A')
				{
					desertElement(lines, index);
					index--;
				}
			}
		}
		else // mode == 2
		{
			for(index = 0; index < getCount(lines); index++)
			{
				if(*(char *)e_(lines)[index] == 'A')
				{
					fastDesertElement(lines, index);
					index--;
				}
			}
		}

		SW_Lap(lapTimes);
		SW_ShowLaps(lapTimes);
	}
	releaseAutoList(lapTimes);
}

int main(int argc, char **argv)
{
	mt19937_initCRnd();

	Test01(1, 30000);
	Test01(2, 30000);
	Test01(2, 100000);
	Test01(2, 1000000);
	Test01(2, 10000000);
}
