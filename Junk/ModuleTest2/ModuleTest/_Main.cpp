#include "all.h"

int main(int argc, char **argv)
{
#if 0 // test test test test test
	updateDiskSpace('C');
	cout("%I64d %I64d %I64d\n", lastDiskFree_User, lastDiskFree, lastDiskSize);
	updateDiskSpace_Dir("C:");
	cout("%I64d %I64d %I64d\n", lastDiskFree_User, lastDiskFree, lastDiskSize);
	updateDiskSpace_Dir("C:\\temp");
	cout("%I64d %I64d %I64d\n", lastDiskFree_User, lastDiskFree, lastDiskSize);
//	updateDiskSpace_Dir("xxx");
	updateDiskSpace_Dir(".");
	cout("%I64d %I64d %I64d\n", lastDiskFree_User, lastDiskFree, lastDiskSize);
#else
	initRnd((int)time(NULL));

	Test_IPCounter();
#endif
}
