#include "C:\Factory\Common\all.h"

#define NUM 200000000

typedef struct
{
	uint A[NUM];
	uint B[NUM];
}
T_t;

T_t *T;

static void Test_A(void)
{
	int c;

	for(c = 0; c < NUM; c++)
	{
		T->A[c] = c;
		T->B[c] = c;
	}
}
static void Test_B(void)
{
	int c;

	for(c = 0; c < NUM; c++)
	{
		T->A[c] = c;
	}
	for(c = 0; c < NUM; c++)
	{
		T->B[c] = c;
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
