#include "C:\Factory\Common\all.h"

static uint GetFibo(uint index)
{
	uint b = 1;
	uint c = 0;

	cout("index: %u\n", index);

	while(index)
	{
		uint next = (b + c) % 1000;

		b = c;
		c = next;

		index--;
	}
	cout("fibo_: %u\n", c);
	return c;
}
int main(int argc, char **argv)
{
	if(argIs("/F"))
	{
		FILE *rfp;
		FILE *wfp;
		uint num;

		rfp = fileOpen(nextArg(), "rt");
		wfp = fileOpen(nextArg(), "wt");

		num = toValue(readLine(rfp));

		while(num)
		{
			writeLine_x(
				wfp,
				xcout(
					"%u",
					GetFibo(
						toValue(readLine(rfp))
						)
					)
				);

			num--;
		}

		fileClose(rfp);
		fileClose(wfp);
		return;
	}
	GetFibo(toValue(nextArg()));
}
