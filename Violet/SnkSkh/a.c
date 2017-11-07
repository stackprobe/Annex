#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"
#include "C:\Factory\Common\Options\Sequence.h"

static double DoTest_04_Main(uint y_pct, uint bo_num, uint smp_pct)
{
	autoList_t *bo = newList();
	uint y_num;
	uint n_num;
	uint smp_num;
	uint i;

	y_num = (bo_num * y_pct) / 100;
	n_num = bo_num - y_num;

	for(i = y_num; i; i--)
		addElement(bo, 1);

	for(i = n_num; i; i--)
		addElement(bo, 0);

	shuffle(bo);

	y_num = 0;
	n_num = 0;
	smp_num = (bo_num * smp_pct) / 100;

	for(i = 0; i < smp_num; i++)
	{
		if(getElement(bo, i))
			y_num++;
		else
			n_num++;
	}
	releaseAutoList(bo);
	return (double)y_num / smp_num;
}
static void DoTest_04(uint y_pct, uint bo_num, uint smp_pct)
{
	double ansPct = DoTest_04_Main(y_pct, bo_num, smp_pct) * 100.0;

	cout("Yes %u %%, %u 個の母集団 -> サンプル %u %% -> Yes %.3f %%, プラス=%.3f, レート=%.3f\n",
		y_pct, bo_num, smp_pct, ansPct, ansPct - y_pct, ansPct / y_pct
		);
}
static void DoTest_03(uint y_pct, uint bo_num, uint smp_pct)
{
	uint i;

	for(i = 10; i; i--)
	{
		DoTest_04(y_pct, bo_num, smp_pct);
	}
}
static void DoTest_02(uint y_pct, uint bo_num)
{
	DoTest_03(y_pct, bo_num, 10);
	DoTest_03(y_pct, bo_num, 20);
	DoTest_03(y_pct, bo_num, 30);
	DoTest_03(y_pct, bo_num, 40);
	DoTest_03(y_pct, bo_num, 50);
	DoTest_03(y_pct, bo_num, 60);
	DoTest_03(y_pct, bo_num, 70);
	DoTest_03(y_pct, bo_num, 80);
	DoTest_03(y_pct, bo_num, 90);
}
static void DoTest_01(uint y_pct)
{
	DoTest_02(y_pct, 10);
	DoTest_02(y_pct, 30);
	DoTest_02(y_pct, 100);
	DoTest_02(y_pct, 300);
	DoTest_02(y_pct, 1000);
	DoTest_02(y_pct, 3000);
	DoTest_02(y_pct, 10000);
	DoTest_02(y_pct, 30000);
	DoTest_02(y_pct, 100000);
	DoTest_02(y_pct, 300000);
	DoTest_02(y_pct, 1000000);
}
static void DoTest_00(void)
{
	DoTest_01(10);
	DoTest_01(20);
	DoTest_01(30);
	DoTest_01(40);
	DoTest_01(50);
	// 50より上は意味が無い。
}
int main(int argc, char **argv)
{
	argIs("--dummy");

	mt19937_initCRnd();

	DoTest_00();
}
