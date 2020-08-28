/*
	ミューテックスをリリースした直後、シビアなタイミングでロックできないことがある？
	無いっぽい。@ 2016.3.24
*/

#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	if(argIs("/T2-CALL"))
	{
		uint mtx = mutexOpen("MTX_TEST_02");

		errorCase(!handleWaitForMillis(mtx, 0));

		mutexUnlock(mtx);
		return;
	}
	if(argIs("/T2"))
	{
		while(waitKey(0) != 0x1b)
		{
			uint c;

			LOGPOS();

			for(c = 100; c; c--)
			{
				execute_x(xcout("\"%s\" /T2-CALL", getSelfFile()));
			}
		}
		return;
	}

	{
		while(waitKey(0) != 0x1b)
		{
			uint c;

			LOGPOS();

			for(c = 1000; c; c--)
			{
				uint mtx = mutexOpen("MTX_TEST_01");

				errorCase(!handleWaitForMillis(mtx, 0));

				mutexUnlock(mtx);
			}
		}
	}
}
