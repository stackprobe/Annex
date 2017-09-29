#include "C:\Factory\Common\all.h"

#define NUM 100000000

static A[NUM];
static B[NUM];

static void Test_A(void)
{
	int c;

	for(c = 0; c < NUM; c++)
	{
		A[c] = c;
		B[c] = c;
	}
}
static void Test_B(void)
{
	int c;

	for(c = 0; c < NUM; c++)
	{
		A[c] = c;
	}
	for(c = 0; c < NUM; c++)
	{
		B[c] = c;
	}
}
int main(int argc, char **argv)
{
	uint64 stTm = nowTick();
	uint64 edTm;

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

/*

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq a
234

C:\Dev\Annex\Labo>ArraySq b
219

C:\Dev\Annex\Labo>ArraySq b
219

C:\Dev\Annex\Labo>ArraySq b
203

C:\Dev\Annex\Labo>ArraySq b
234

C:\Dev\Annex\Labo>ArraySq b
218

C:\Dev\Annex\Labo>ArraySq b
219

C:\Dev\Annex\Labo>ArraySq b
218

C:\Dev\Annex\Labo>ArraySq b
203

C:\Dev\Annex\Labo>ArraySq b
203

C:\Dev\Annex\Labo>ArraySq b
203

Bの方が速いのは、たぶんL1,L2キャッシュのせい。

*/
