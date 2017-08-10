#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	time_t t = compactStampToTime("20170515000000");

	cout("%I64d\n", t);
	cout("%I64d\n", t % 86400ui64);
}
