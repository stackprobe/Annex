#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#define TEST_COUNT 1000000

static int TryGohan(uint gohanPct)
{
	return mt19937_rnd32() * 100.0 / UINTMAX < gohanPct;
}
static int Azukeru(uint starvDay, uint azukeDay, uint gohanPct)
{
	uint noGhnDay = 0;
	uint day;

	for(day = 1; day <= azukeDay; day++)
	{
		if(TryGohan(gohanPct))
		{
			noGhnDay = 0;
		}
		else
		{
			noGhnDay++;

			if(starvDay <= noGhnDay)
				return 0;
		}
	}
	return 1;
}
static void Pet2(uint starvDay, uint azukeDay, uint gohanPct)
{
	uint numer = 0;
	uint denom;

	cout("STARV_DAY: %u\n", starvDay);
	cout("AZUKE_DAY: %u\n", azukeDay);
	cout("GOHAN_PCT: %u\n", gohanPct);

	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	for(denom = 0; denom < TEST_COUNT; denom++)
		if(Azukeru(starvDay, azukeDay, gohanPct))
			numer++;

	cout("--\n");
	cout("ALIVE_PCT: %.1f PCT = %u / %u\n", numer * 100.0 / denom, numer, denom);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	Pet2(
		toValue(getArg(0)),
		toValue(getArg(1)),
		toValue(getArg(2))
		);
}
