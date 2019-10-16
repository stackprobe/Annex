#include <stdio.h>
#include <stdlib.h>
#include <limits.h>

	#define K 20
//	#define K 30
//	#define K 40
//	#define K 50

typedef unsigned __int32 uint;
typedef unsigned __int64 uint64;

#if 1

// VC ---->

#include <windows.h>
#include <wincrypt.h>

#pragma comment(lib, "ADVAPI32")

static uint GetRandom(uint minval, uint maxval)
{
	uint64 v;

	{
		HCRYPTPROV hp;

		if(!CryptAcquireContext(&hp, 0, 0, PROV_RSA_FULL, CRYPT_VERIFYCONTEXT))
			exit(1); // fatal

		if(!CryptGenRandom(hp, sizeof(v), (BYTE *)&v))
			exit(1); // fatal

		CryptReleaseContext(hp, 0);
	}

	return v % (maxval - minval + 1) + minval;
}

// <---- VC

#elif 0

static uint GetRandom(uint minval, uint maxval)
{
	uint64 v = rand();

	v ^= (uint64)rand() << 15;
	v ^= (uint64)rand() << 30;
	v ^= (uint64)rand() << 45;
	v ^= (uint64)rand() << 60;

	return v % (maxval - minval + 1) + minval;
}

#else

static uint Xorshift128(void)
{
	static uint x = 1;
	static uint y;
	static uint z;
	static uint a;
	uint t;

	t = x;
	t ^= x << 11;
	t ^= t >> 8;
	t ^= a;
	t ^= a >> 19;
	x = y;
	y = z;
	z = a;
	a = t;

	return t;
}
static uint GetRandom(uint minval, uint maxval)
{
	uint64 v = Xorshift128() | (uint64)Xorshift128() << 32;

	return v % (maxval - minval + 1) + minval;
}

#endif

static uint ModPow(uint b, uint e, uint m)
{
	uint64 a = 1;

	for(; e; e >>= 1)
	{
		if(e & 1)
			a = (a * b) % m;

		b = ((uint64)b * b) % m;
	}
	return a;
}
static int IsPrime(uint n)
{
	uint d;
	uint x;
	uint r;
	uint s;
	uint t;

	if(n <= 1)
		return 0;

	if(n <= 3)
		return 1;

	if(!(n & 1))
		return 0;

	d = n;

	for(r = 0; !((d >>= 1) & 1); r++);

	for(t = K; t; t--)
	{
		x = ModPow(GetRandom(2, n - 2), d, n);

		if(x != 1 && x != n - 1)
		{
			for(s = r; ; s--)
			{
				if(!s)
					return 0;

				x = ModPow(x, 2, n);

				if(x == n - 1)
					break;
			}
		}
	}
	return 1;
}
int main()
{
	uint c;

	for(c = 0; c < UINT_MAX; c++)
		if(IsPrime(c))
			printf("%d\n", c);
}
