#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

#define TEST_NUM 5
#define TEST_COUNT 10000

static autoList_t *Report;

static void Kachikoshi(double shouritsu, uint kaisuu)
{
	uint testNo;
	double kachikoshiRateSum = 0.0;

	cout("Ÿ—¦ = %f, ‰ñ” = %u\n", shouritsu, kaisuu);

	for(testNo = 0; testNo < TEST_NUM; testNo++)
	{
		uint kachikoshi = 0;
		uint testcnt;
		double kachikoshiRate;

		for(testcnt = 0; testcnt < TEST_COUNT; testcnt++)
		{
			uint kachi = 0;
			uint make = 0;
			uint count;

			for(count = 0; count < kaisuu; count++)
			{
				int result = ((double)mt19937_rnd32() / UINT_MAX) < shouritsu;

				if(result)
					kachi++;
				else
					make++;
			}
			if(make < kachi)
				kachikoshi++;
		}
		kachikoshiRate = (double)kachikoshi / TEST_COUNT;
		kachikoshiRateSum += kachikoshiRate;

		cout("Ÿ‚¿‰z‚µ—¦ = %f\n", kachikoshiRate);
	}
	cout("\n");

	addElement(Report, (uint)xcout("Ÿ—¦ = %f, ‰ñ” = %u, Ÿ‚¿‰z‚µ—¦(•½‹Ï) = %f", shouritsu, kaisuu, kachikoshiRateSum / TEST_NUM));
}
static void Main2(double shouritsu)
{
	Kachikoshi(shouritsu, 1);
	Kachikoshi(shouritsu, 2);
	Kachikoshi(shouritsu, 3);
	Kachikoshi(shouritsu, 4);
	Kachikoshi(shouritsu, 5);
	Kachikoshi(shouritsu, 6);
	Kachikoshi(shouritsu, 7);
	Kachikoshi(shouritsu, 8);
	Kachikoshi(shouritsu, 9);
	Kachikoshi(shouritsu, 10);
	Kachikoshi(shouritsu, 30);
	Kachikoshi(shouritsu, 100);
	Kachikoshi(shouritsu, 300);
	Kachikoshi(shouritsu, 1000);
	Kachikoshi(shouritsu, 3000);
	Kachikoshi(shouritsu, 10000);
}
int main(int argc, char **argv)
{
	Report = newList();

	Main2(0.4);
	Main2(0.49);
	Main2(0.5);
	Main2(0.51);
	Main2(0.6);

	{
		char *line;
		uint index;

		foreach(Report, line, index)
			cout("%s\n", line);
	}
}
