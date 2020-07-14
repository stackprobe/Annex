#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static double GetBzSpan(void)
{
	double ans = 0.0;
	double rate = 1.0;
	uint c;

	for(c = 1; c <= 100; c++)
	{
		double o = rate *        c  / 100.0;
		double x = rate * (100 - c) / 100.0;

		ans += c * o;
		rate = x;
	}
	return ans;
}

static double BzSpan;
static uint K;

static uint Test01a(void)
{
	uint ans = 0;
	uint pct = 1;
	uint c;

	for(c = 0; c < K; c++)
	{
		if(mt19937_rnd(100) < pct)
		{
			ans++;
			pct = 1;
		}
		else
			pct++;
	}
	return ans;
}
static double Test01(void)
{
	uint64 n = 0;
	uint c;

	for(c = 0; c < 10000; c++)
//	for(c = 0; c < 100000; c++)
//	for(c = 0; c < 1000000; c++)
	{
		n += Test01a();
	}
	return (double)n / c;
}
static double Test02(void)
{
	return K / BzSpan;
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	BzSpan = GetBzSpan();

	cout("BzSpan: %.10f\n", BzSpan);

//	for(K = 10000; K <= 1000000; K += 10000)
	for(K = 100; K <= 10000; K += 100)
//	for(K = 10;  K <= 10000; K += 10)
//	for(K = 1;   K <= 10000; K += 1)
	{
		double t1 = Test01();
		double t2 = Test02();

		cout("K=%u, %.10f / %.10f = %.10f\n", K, t2, t1, t2 / t1); // t2 / t1 ’ˆÓI
	}
}
