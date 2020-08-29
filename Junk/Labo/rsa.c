#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"

// ==== 拡張ユークリッド互除法 ====

static sint EG_A;
static sint EG_B;
static sint EG_AB_MOD;

/*
	g_ret: EG_A * x + EG_B * y == GCD(x, y)

	ret: GCD(x, y)
*/
static uint ExtGCD(uint x, uint y)
{
	uint ret;

	if(y)
	{
		ret = ExtGCD(y, x % y);

		m_swap(EG_A, EG_B, sint);

		EG_B -= (x / y) * EG_A;
//		EG_B %= EG_AB_MOD;
	}
	else
	{
		ret = x;

		EG_A = 1;
		EG_B = 0;
	}
	return ret;
}

// ====

static uint16 CRand16(void)
{
	return (uint16)getCryptoRand16();
}
static uint ModPow(uint v, uint e, uint m)
{
	uint64 r;

	if(!e)
		return 1;

	r = ModPow(v, e / 2, m);
	r *= r;
	r %= m;

	if(e & 1)
	{
		r *= v;
		r %= m;
	}
	return r;
}
static void TestMain(void)
{
	static uchar ops[0x1000];
	uint c, p, q, m, e, d; // 32 bit

#define op(p) (ops[(p) / 16] & 1 << (p) / 2 % 8) // 奇数について合成数かどうか判定する。

	if(!ops[0])
	{
		ops[0] = 1;

		for(c = 3; c < 0x100; c += 2)
			if(!op(c))
				for(d = c * c / 2; d < 0x10000 / 2; d += c)
					ops[d / 8] |= 1 << d % 8;
	}

	do
	{
		do p = CRand16() | 1; while(op(p)); // odd prime -> p
		do q = CRand16() | 1; while(op(q)); // odd prime -> q
	}
	while(p == q || p * q < 0x10000);

	m = p * q;
	p--;
	q--;

#if 1
	for(e = p, d = q; d; e %= d, c = e, e = d, d = c); // GCD(p, q) -> e
#else // old
	for(e = p, d = q; e != d; e < d ? (d -= e) : (e -= d)); // GCD(p, q) -> e
#endif

	p *= q / e;

	EG_AB_MOD = p;

	do e = CRand16() | 1; while(op(e) || ExtGCD(e, p) != 1); // odd prime -> e

	EG_AB_MOD = -1; // clear

//	cout("%d %u\n", EG_A, p);

	{
		sint sd = EG_A;

#if 1
		sd %= (sint)p;
		sd += p;
		sd %= p;
#else // smpl_same
		while(sd < 0)
			sd += p;
#endif

		d = (uint)sd;
	}

	// ----

	cout("%u %u %u\n", e, d, m);

	{
		uint i;

		for(i = 0; i < 100; i++)
		{
			p = CRand16();
			c = ModPow(p, e, m);
			q = ModPow(c, d, m);

//			cout("%u %u %u\n", p, c, q);

			errorCase(p != q);
		}
	}
}
int main(int argc, char **argv)
{
	for(; ; )
	{
		TestMain();
	}
}
