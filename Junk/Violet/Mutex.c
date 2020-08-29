#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#include <windows.h>

int main(int argc, char **argv)
{
	char *name;
	HANDLE hdl;
	int ret;

	if(argc != 2)
		return;

	name = argv[1];
	printf("name == '%s'\n", name);

	hdl = CreateMutexA(NULL, FALSE, name);
	printf("hdl == %d\n", (int)hdl);

	if(hdl == NULL)
		return;

//	_getch();

	printf("LOCKING...\n");
	ret = WaitForSingleObject((HANDLE)hdl, INFINITE);

	if(ret != WAIT_OBJECT_0)
		return; // fatal

	printf("LOCKED\n");

	_getch();

	ReleaseMutex(hdl);
	CloseHandle(hdl);
	printf("RELEASE & CLOSED\n");
}
