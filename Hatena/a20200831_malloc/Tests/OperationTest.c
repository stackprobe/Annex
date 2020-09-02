#include "C:\Factory\Common\all.h"

static void Test01_ns(uint n, uint s)
{
	coExecute_x(xcout("OperationTestMain %u %u", n, s));
}
static void Test01_n(uint n)
{
	Test01_ns(n, 1);
	Test01_ns(n, 15000);
	Test01_ns(n, 30000);
	Test01_ns(n, 45000);
}
static void Test01(void)
{
	Test01_n(1);
	Test01_n(15000);
	Test01_n(30000);
	Test01_n(45000);
}
int main(int argc, char **argv)
{
	Test01();
}
