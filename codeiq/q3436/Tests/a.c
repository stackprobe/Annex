#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Prime.h"

static uint Cs[10];

static uint Mul(uint m)
{
	uint ret = 1;
	uint i;

	for(i = 0; i < m; i++)
		ret *= Cs[i];

	return ret;
}

static uint GN_M;
static uint GN_C;
static uint GN_Fnd;

static void GetN_Main(uint i)
{
	if(i < GN_M)
	{
		for(Cs[i] = i ? Cs[i - 1] + 1 : 1; Cs[i] <= 9; Cs[i]++)
		{
			GetN_Main(i + 1);
		}
	}
	else
	{
		if(Mul(GN_M) == GN_C)
		{
			GN_Fnd++;
		}
	}
}
static uint GetN(uint m, uint c)
{
	GN_Fnd = 0;
	GN_M = m;
	GN_C = c;

	GetN_Main(0);

	return GN_Fnd;
}
int main(int argc, char **argv)
{
	uint m; // ¡”
	uint n; // ƒpƒ^[ƒ“”
	uint c;

	m = toValue(nextArg());
	n = toValue(nextArg());

	errorCase(!m_isRange(m, 1, 9));
	errorCase(!m_isRange(n, 1, 9));

	for(c = 1; c <= 32768; c++)
	if(GetN(m, c) == n)
	{
		uint64 f[64];
		uint i;

		Factorization((uint64)c, f);

		cout("%5u", c);

		for(i = 0; f[i] != 0; i++)
			cout(" %I64u", f[i]);

		cout("\n");
	}
}
