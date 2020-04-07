#include "C:\Factory\Common\all.h"
#include "C:\Factory\SubTools\libs\wavFile.h"

int main(int argc, char **argv)
{
	char *rWavFile = getArg(0);
	char *wCsvFile = getArg(1);
	char *wHzFile  = getArg(2);

	readWAVFileToCSVFile(rWavFile, wCsvFile);

	writeOneLineNoRet_b_cx(wHzFile, xcout("%u", lastWAV_Hz));
}
