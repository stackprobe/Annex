#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Performance.h"

int main(int argc, char **argv)
{
	uint64 startTime;
	uint64 endTime;
	uint mtx = mutexOpen("TEST_TEST_TEST");
	double t;
	double tMax = 0.0;

	while(waitKey(0) != 0x1b)
	{
		startTime = GetPerformanceCounter();

		if(handleWaitForMillis(mtx, 0))
			mutexRelease(mtx);

		endTime = GetPerformanceCounter();
		t = (endTime - startTime) * 1000.0 / GetPerformanceFrequency();
		m_maxim(tMax, t);

		cout("max = %f ms, %f ms\n", tMax, t);
	}
	handleClose(mtx);
}
