#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Performance.h"

int main(int argc, char **argv)
{
	uint64 t1;
	uint64 t2;

	t1 = GetPerformanceCounter();
	sleep(1234);
	t2 = GetPerformanceCounter();

	cout("1.234s == %f\n", (double)(t2 - t1) / GetPerformanceFrequency());
}
