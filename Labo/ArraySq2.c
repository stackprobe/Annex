#include "C:\Factory\Common\all.h"

#define NUM 200000000

typedef union
{
	uint A[2][NUM];
	uint B[NUM][2];
}
T_t;

T_t *T;

static uint Test_A(void)
{
	uint t = 0;
	uint c;
	uint d;

	for(c = 0; c < NUM; c++)
	for(d = 0; d < 2; d++)
	{
		t ^= T->A[d][c];
	}
	return t;
}
static uint Test_B(void)
{
	uint t = 0;
	uint c;
	uint d;

	for(c = 0; c < NUM; c++)
	for(d = 0; d < 2; d++)
	{
		t ^= T->B[c][d];
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
