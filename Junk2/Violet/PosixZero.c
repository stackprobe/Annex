#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\TimeData.h"

int main(int argc, char **argv)
{
	cout("%I64u\n", TimeData2TSec(Stamp2TimeData(19700101000000)));

	cout("%I64u\n", GetNowEpoch());
	cout("%I64u\n", time(NULL));
}
