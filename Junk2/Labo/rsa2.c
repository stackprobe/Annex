#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRandom.h"

static uint GCD(uint x, uint y)
{
	while(y)
	{
		x %= y;

		m_swap(x, y, uint);
	}
	return x;
}

// ---- ExtGCD ----

static uint EG_A;
static uint EG_B;
static uint EG_AB_MOD;

//static sint64 SS_A;
//static sint64 SS_B;

static uint ExtGCD(uint x, uint y)
{
	uint ret;

	if(y)
	{
		ret = ExtGCD(y, x % y);

		m_swap(EG_A, EG_B, uint);

		EG_B += EG_AB_MOD - ((uint64)(x / y) * EG_A) % EG_AB_MOD;
		EG_B %= EG_AB_MOD;

//		m_swap(SS_A, SS_B, sint64);

//		SS_B -= (x / y) * SS_A;

//		cout("%I64d %I64d / %u %u\n", SS_A, SS_B, EG_A, EG_B);

//		errorCase((SS_A % EG_AB_MOD + EG_AB_MOD) % EG_AB_MOD != EG_A);
//		errorCase((SS_B % EG_AB_MOD + EG_AB_MOD) % EG_AB_MOD != EG_B);
	}
	else
	{
		ret = x;

		EG_A = 1;
		EG_B = 0;

//		SS_A = 1;
//		SS_B = 0;

//		cout("%u\n", EG_AB_MOD);
	}
	return ret;
}

// ----

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

// ---- GenKey ----

static uint GK_E;
static uint GK_D;
static uint GK_M;

static void GenKey(void)
{
	static uchar ops[0x1000];
	uint c, p, q, m, e, d; // 32 bit

#define op(p) (ops[(p) / 16] & 1 << (p) / 2 % 8) // Šï”‚É‚Â‚¢‚Ä‡¬”‚©‚Ç‚¤‚©”»’è‚·‚éB

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

	e = GCD(p, q);

	p *= q / e;

	EG_AB_MOD = p;

	do e = CRand16() | 1; while(op(e) || ExtGCD(e, p) != 1); // odd prime -> e

	EG_AB_MOD = 0; // clear
	d = EG_A;

	GK_E = e;
	GK_D = d;
	GK_M = m;
}

// ----

static void TestMain(void)
{
	uint e;
	uint d;
	uint m;
	uint testCnt;

	GenKey();

	e = GK_E;
	d = GK_D;
	m = GK_M;

	cout("%u %u %u\n", e, d, m);

	for(testCnt = 100; testCnt; testCnt--)
	{
		uint p = CRand16();
		uint c;
		uint q;

		c = ModPow(p, e, m);
		q = ModPow(c, d, m);

		cout("%u -> %u -> ...\n", p, c);
		cout("%u\n", q);

		errorCase(p != q);
	}
}
int main(int argc, char **argv)
{
	for(; ; )
	{
		TestMain();
	}
}
