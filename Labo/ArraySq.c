/*
	ArraySq a ��� ArraySq b �̕���������� L1,L2,L3 �L���b�V���̂����B�����B

	���ɂ���Ă� ArraySq a �̕��������Ȃ�B�H�H�H
*/

#include "C:\Factory\Common\all.h"

#define NUM 200000000

uint A[NUM];
uint B[NUM];

static uint Test_A(void)
{
	uint t = 0;
	uint c;
	uint i;

	for(c = 11; c; c--)
	{
		for(i = 0; i < NUM; i++)
		{
			t ^= A[i];
			t ^= B[i];
		}
	}
	return t;
}
static uint Test_B(void)
{
	uint t = 0;
	uint c;
	uint i;

	for(c = 11; c; c--)
	{
		for(i = 0; i < NUM; i++)
		{
			t ^= A[i];
		}
		for(i = 0; i < NUM; i++)
		{
			t ^= B[i];
		}
	}
	return t;
}
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
int main(int argc, char **argv)
{
	uint64 stTm;
	uint64 edTm;

	// init A, B
	{
		uint i;

		for(i = 0; i < NUM; i++)
		{
			A[i] = Xorshift128();
			B[i] = Xorshift128();
		}
	}

	if(argIs("T"))
	{
		cout("%08x\n", Test_A());
		cout("%08x\n", Test_B());
	}

writeBinary_cx("C:\\temp\\ArraySq_001.tmp", readBinary("C:\\Factory\\Resource\\CP932.txt"));

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
