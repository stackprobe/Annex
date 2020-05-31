#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static uint F(uint n, uint x)
{
	uint64 ret = 4;

	ret *= x;
	ret *= n - x;
	ret /= n;

	errorCase((uint64)n < ret); // –³‚¢‚ÆŽv‚¤B

	return (uint)ret;
}
static uint G(uint n, uint x)
{
	uchar knowns[300000 + 1];
	uint count = 0;

	zeroclear(knowns);

	while(!knowns[x])
	{
		knowns[x] = 1;
		x = F(n, x);
		count++;
	}
	return count;
}
static uint H(uint n)
{
	uint sum = 0;
	uint x;

	for(x = 0; x <= n; x++)
	{
		uint ret = G(n, x);

		errorCase(UINTMAX - sum < ret);

		sum += ret;
	}
	return sum;
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

readArgs:
	if(argIs("F"))
	{
		uint n;
		uint x;

		n = toValue(nextArg());
		x = toValue(nextArg());

		errorCase(!m_isRange(n, 1, 300000));
		errorCase(!m_isRange(x, 0, n));

		cout("%u\n", F(n, x));
		goto readArgs;
	}
	if(argIs("G"))
	{
		uint n;
		uint x;

		n = toValue(nextArg());
		x = toValue(nextArg());

		errorCase(!m_isRange(n, 1, 300000));
		errorCase(!m_isRange(x, 0, n));

		cout("%u\n", G(n, x));
		goto readArgs;
	}
	if(argIs("H"))
	{
		uint n = toValue(nextArg());

		errorCase(!m_isRange(n, 1, 300000));

		cout("%u\n", H(n));
		goto readArgs;
	}
	if(argIs("HRnd"))
	{
		while(!waitKey(0))
		{
			uint n = mt19937_rnd(300000) + 1;

			cout("%u -> ", n);
			cout("%u\n", H(n));
		}
		goto readArgs;
	}
	if(argIs("GMax"))
	{
		uint n = toValue(nextArg());
		uint x;
		uint maxRet = 0;

		errorCase(!m_isRange(n, 1, 300000));

		for(x = 0; x <= n; x++)
		{
			uint ret = G(n, x);

			m_maxim(maxRet, ret);
		}
		cout("%u\n", maxRet);
		goto readArgs;
	}
}
