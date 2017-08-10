#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	double lastLdf = 0.0;
	double currLdf = 0.0;

	for(; ; )
	{
		updateDiskSpace('C');
		currLdf = lastDiskFree / 1000000.0;
		cout("DISK_FREE = %.6f MB, DIFF = %.6f MB\n", currLdf, currLdf - lastLdf);
		lastLdf = currLdf;
		sleep(1000);
	}
}
