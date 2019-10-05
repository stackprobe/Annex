#include "all.h"

#define D_IS_PRIME(bit, dblBit) \
	static UI ## bit ## _t ModMul ## bit(UI ## bit ## _t a, UI ## bit ## _t b, UI ## bit ## _t mod) { \
		UI ## dblBit ## _t aa; \
		UI ## dblBit ## _t bb; \
		UI ## dblBit ## _t cc; \
		UI ## dblBit ## _t mm; \
		aa.L = a; \
		aa.H = UI ## bit ## _N0; \
		bb.L = b; \
		bb.H = UI ## bit ## _N0; \
		mm.L = mod; \
		mm.H = UI ## bit ## _N0; \
		cc = UI ## dblBit ## _Mul(aa, bb, NULL); \
		cc = UI ## dblBit ## _Mod(cc, mm, NULL); \
		return cc.L; \
	} \
	static UI ## bit ## _t ModPow ## bit(UI ## bit ## _t val, UI ## bit ## _t exp, UI ## bit ## _t mod) { \
		UI ## bit ## _t ret = UI ## bit ## _N1; \
		for(; ; ) { \
			if(UI ## bit ## _y(exp) & 1) { \
				ret = ModMul ## bit(ret, val, mod); \
			} \
			exp = UI ## bit ## _DivTwo(exp); \
			if(UI ## bit ## _IsZero(exp)) { \
				break; \
			} \
			val = ModMul ## bit(val, val, mod); \
		} \
		return ret; \
	} \
	static int S_IsPrime ## bit(uint *arr) { \
		UI ## bit ## _t val; \
		UI ## bit ## _t d; \
		UI ## bit ## _t x; \
		UI ## bit ## _t val_1; \
		UI ## dblBit ## _t val_3; \
		UI ## dblBit ## _t valRand; \
		uint r; \
		uint c; \
		uint k; \
/* cout("S_IP.1 %s bit=" # bit "\n", LOGPOS_Time(0)); */ \
		d = val = ToUI ## bit(arr); \
		for(r = 0; (d = UI ## bit ## _DivTwo(d)), !(UI ## bit ## _y(d) & 1); r++); \
		val_1   = UI ## bit ## _Sub(val, UI ## bit ## _N1); \
		val_3.L = UI ## bit ## _Sub(val, UI ## bit ## _N3); \
		val_3.H = UI ## bit ## _N0; \
/* cout("S_IP.2 %s\n", LOGPOS_Time(0)); */ \
		for(k = MillerRabin_K; k; k--) { \
/* cout("S_IP.3 %s\n", LOGPOS_Time(0)); */ \
			RandBytes(&valRand, sizeof(valRand)); \
/* cout("S_IP.4 %s\n", LOGPOS_Time(0)); */ \
			x = UI ## dblBit ## _Mod(valRand, val_3, NULL).L; \
/* cout("S_IP.5 %s\n", LOGPOS_Time(0)); */ \
			x = UI ## bit ## _Add(x, UI ## bit ## _N2, NULL); \
/* cout("S_IP.6 %s\n", LOGPOS_Time(0)); */ \
			x = ModPow ## bit(x, d, val); \
/* cout("S_IP.7 %s\n", LOGPOS_Time(0)); */ \
			if(UI ## bit ## _Comp(x, UI ## bit ## _N1) && UI ## bit ## _Comp(x, val_1)) { \
				for(c = r; ; c--) { \
					if(!c) return 0; \
					x = ModPow ## bit(x, UI ## bit ## _N2, val); \
					if(!UI ## bit ## _Comp(x, val_1)) break; \
				} \
			} \
/* cout("S_IP.8 %s\n", LOGPOS_Time(0)); */ \
		} \
/* cout("S_IP.9 %s\n", LOGPOS_Time(0)); */ \
		return 1; \
	}

D_IS_PRIME(  64,  128)
D_IS_PRIME( 128,  256)
D_IS_PRIME( 256,  512)
D_IS_PRIME( 512, 1024)
D_IS_PRIME(1024, 2048)
D_IS_PRIME(2048, 4096)
D_IS_PRIME(4096, 8192)

static int S_IsPrime(UI4096_t val)
{
	uint *arr;
	uint size;

	errorCase(UI4096_Comp(val, UI4096_N3) <= 0);

	if(!(UI4096_y(val) & 1)) // ? ‹ô”
		return 0;

	arr = UI4096ToUIArr(val);
	size = GetUIArrSize(arr);

	if(size <=  2) return S_IsPrime64(arr);
	if(size <=  4) return S_IsPrime128(arr);
	if(size <=  8) return S_IsPrime256(arr);
	if(size <= 16) return S_IsPrime512(arr);
	if(size <= 32) return S_IsPrime1024(arr);
	if(size <= 64) return S_IsPrime2048(arr);
	               return S_IsPrime4096(arr);
}

#define MILLER_RABIN_LMT 10000000000000000ui64 // 10^16

int A_IsPrime(UI4096_t val, int r_flg)
{
	uint *arr = UI4096ToUIArr(val);

	if(GetUIArrSize(arr) <= 2)
	{
		uint64 value = (uint64)arr[1] << 32 | arr[0];

		if(r_flg)
			return IsPrime_R(value);

		if(value < MILLER_RABIN_LMT)
			return IsPrime(value);

		return IsPrime_M_K(value, MillerRabin_K);
	}
	return S_IsPrime(val);
}
int A_IsPrime64(uint64 value)
{
	if(value < MILLER_RABIN_LMT)
		return IsPrime(value);

	return IsPrime_M_K(value, MillerRabin_K);
}
