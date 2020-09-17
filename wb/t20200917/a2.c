#include "C:\Factory\Common\all.h"

#define PI 3.14159265358979323846

static void Test01(double isou_zure)
{
	double v00 = 0.0;
	double v90 = 0.0;
	double v;
	uint numer;
//	uint denom = 1000000;
//	uint denom = 1000003;
	uint denom = 1000033;

	for(numer = 0; numer < denom; numer++)
	{
		double a = ((double)numer / denom) * PI * 2.0;
		double a00;
		double a90;
		double ax;

		a00 = a;
		a90 = a + PI * 0.5;
		ax = a + isou_zure;

		v00 += sin(a00) * sin(ax);
		v90 += sin(a90) * sin(ax);
	}

	v = v00 * v00 + v90 * v90;

	cout("%.9f\n", v);
}
int main(int argc, char **argv)
{
	uint isou_zure_pct;

	for(isou_zure_pct = 0; isou_zure_pct < 100; isou_zure_pct++)
	{
		double isou_zure = (isou_zure_pct / 100.0) * PI * 2.0;

		Test01(isou_zure);
	}
}
