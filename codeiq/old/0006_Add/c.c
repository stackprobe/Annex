#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

static char *MakeNum(void)
{
	autoBlock_t *buff = newBlock();
	uint keta = mt19937_rnd(30) + 1;

	if(mt19937_rnd(2))
		addByte(buff, '-');

	while(keta)
	{
		addByte(buff, mt19937_range('0', '9'));
		keta--;
	}
	return unbindBlock2Line(buff);
}
int main(int argc, char **argv)
{
	mt19937_init();

	for(; ; )
	{
		char *num1 = MakeNum();
		char *num2 = MakeNum();

		coExecute_x(xcout("a.exe %s %s > 1.tmp", num1, num2));
		execute("TYPE 1.tmp");

		coExecute_x(xcout("b.exe %s %s > 2.tmp", num1, num2));
		execute("TYPE 2.tmp");

		errorCase(!isSameFile("1.tmp", "2.tmp"));

		memFree(num1);
		memFree(num2);
	}
}
