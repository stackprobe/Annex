#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static uint64 TestAdd(uint64 a, uint64 b)
{
	for(; ; )
	{
		uint64 t;

		cout("> %016I64x\n", a);
		cout("< %016I64x\n", b);

		if(b == 0)
			break;

		t = a & b;
		a = a ^ b;
		b = t << 1;
	}
	return a;
}
static void DoTest(uint64 a, uint64 b)
{
	uint64 ans1;
	uint64 ans2;

	errorCase(UINT64MAX - a < b);

	ans1 = a + b;
	ans2 = TestAdd(a, b);

	cout("%I64u\n", ans1);
	cout("%I64u\n", ans2);

	errorCase(ans1 != ans2);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	if(hasArgs(2))
	{
		uint64 a = toValue64(getArg(0));
		uint64 b = toValue64(getArg(1));

		skipArg(2);

		DoTest(a, b);
		return;
	}

	while(!waitKey(0))
	{
		uint64 a = mt19937_rnd64();
		uint64 b = mt19937_rnd64();

		if(UINT64MAX - a < b)
		{
			a &= SINT64MAX;
			b &= SINT64MAX;
		}
		DoTest(a, b);
		cout("\n");
	}
}
