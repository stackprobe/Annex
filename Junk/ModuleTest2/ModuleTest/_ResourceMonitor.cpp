#include "all.h"

ResStatus_t GetResStatus(void)
{
	updateMemory();

//	if(90 <= lastMemoryLoad) // ? 90 % and over
	if(lastMemoryFree < 100000000) // ? under 100MB
		return RS_RED;

	updateDiskSpace(getTempRtDir()[0]);

	if(lastDiskFree < 1000000000) // ? under 1GB
		return RS_RED;

//	if(lastDiskFree < 1500000000 || 80 <= lastMemoryLoad) // ? under 1.5GB || 80 % and over
	if(lastDiskFree < 1500000000 || lastMemoryFree < 150000000) // ? under 1.5GB || under 150MB
		return RS_YELLOW;

	return RS_GREEN;
}
