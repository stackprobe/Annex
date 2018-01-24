#include "C:\Factory\Common\all.h"

#define NUM 200000000

typedef union
{
	uint A[2][NUM];
	uint B[NUM][2];
}
T_t;

T_t *T;

static void Test_A(void)
{
	int c;
	int d;

	for(c = 0; c < NUM; c++)
	for(d = 0; d < 2; d++)
	{
		T->A[d][c] = c;
	}
}
static void Test_B(void)
{
	int c;
	int d;

	for(c = 0; c < NUM; c++)
	for(d = 0; d < 2; d++)
	{
		T->B[c][d] = c;
	}
}
int main(int argc, char **argv)
{
	uint64 stTm;
	uint64 edTm;

	T = nb(T_t);

	stTm = nowTick();

	if(argIs("A"))
	{
		Test_A();
	}
	if(argIs("B"))
	{
		Test_B();
	}

	edTm = nowTick();

	cout("%I64u\n", edTm - stTm);
}
