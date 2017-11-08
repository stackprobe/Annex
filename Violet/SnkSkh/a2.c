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
static sint Comp_AnsPct(uint v1, uint v2)
{
	double a = *(double *)v1;
	double b = *(double *)v2;

	return m_simpleComp(a, b);
}
static void PrAnsPct(uint y_pct, autoList_t *ansPcts, uint apIndex, char *title)
{
	double ansPct = *(double *)getElement(ansPcts, apIndex);

	cout("%s : Yes %.3f %%, プラス=%.3f, レート=%.3f\n",
		title, ansPct, ansPct - y_pct, ansPct / y_pct
		);
}
static void DoTest_03(uint y_pct, uint bo_num, uint smp_pct)
{
	autoList_t *ansPcts = newList();
	uint i;

	for(i = 100; i; i--)
	{
		double ansPct = DoTest_04_Main(y_pct, bo_num, smp_pct) * 100.0;

		addElement(ansPcts, (uint)memClone(&ansPct, sizeof(double)));
	}
	rapidSort(ansPcts, Comp_AnsPct);

	cout("Yes %u %%, %u 個の母集団 -> サンプル %u %%\n",
		y_pct, bo_num, smp_pct
		);

	PrAnsPct(y_pct, ansPcts, 10, "安全下限"); // 外れ値除去
	PrAnsPct(y_pct, ansPcts, 89, "安全上限"); // 外れ値除去
//	PrAnsPct(y_pct, ansPcts,  0, "最悪下限");
//	PrAnsPct(y_pct, ansPcts, 99, "最悪上限");

	releaseDim(ansPcts, 1);
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
