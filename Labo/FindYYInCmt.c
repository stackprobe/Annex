#include "C:\Factory\Common\all.h"

static void Main2(void)
{
	autoList_t *files = lssFiles(".");
	char *file;
	uint index;

	// todo ???

	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	addCwd(nextArg());
	Main2();
	unaddCwd();
}
