#include "C:\Factory\Common\all.h"

#define NUM 200000000

typedef struct
{
	uint A[NUM];
	uint B[NUM];
}
T_t;

T_t *T;

static uint Test_A(void)
{
	uint t = 0;
	uint c;

	for(c = 0; c < NUM; c++)
	{
		t ^= T->A[c];
		t ^= T->B[c];
	}
	return t;
}
static uint Test_B(void)
{
	uint t = 0;
	uint c;

	for(c = 0; c < NUM; c++)
	{
		t ^= T->A[c];
	}
	for(c = 0; c < NUM; c++)
	{
		t ^= T->B[c];
	}
	return t;
}
int main(int argc, char **argv)
{
	uint64 stTm;
	uint64 edTm;

	T = nb(T_t);

	{
		uint c;

		for(c = 0; c < sizeof(T_t); c++)
		{
			((uchar *)T)[c] = (c / 3) & 0xff;
		}
	}

	if(argIs("T"))
	{
		cout("%08x\n", Test_A());
		cout("%08x\n", Test_B());
	}

	stTm = nowTick();

	if(argIs("A"))
	{
		noop_u(Test_A());
	}
	if(argIs("B"))
	{
		noop_u(Test_B());
	}

	edTm = nowTick();

	cout("%I64u\n", edTm - stTm);
}
