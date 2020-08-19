#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint rc;
	uint wc = 1;

	for(rc = 1; rc < 20000; rc++)
	{
		char *rFile = xcout("C:\\temp\\images\\%u.png", rc);

		if(existFile(rFile))
		{
			char *wFile = xcout("%u.png", wc);

			coExecute_x(xcout("REN %s %s", rFile, wFile));

			wc++;
			memFree(wFile);
		}
		memFree(rFile);
	}
}
