#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static double GetRouletteDeathRate(uint starvDay, uint gohanPct)
{
	return pow((100 - gohanPct) / 100.0, starvDay);
}
static double GetStepDay(uint starvDay, uint gohanPct)
{
	double numer = gohanPct / 100.0;
	double denom = gohanPct / 100.0;
	double a = 1.0;
	double b;
	uint d;

	for(d = 2; d <= starvDay; d++)
	{
		a *= (100 - gohanPct) / 100.0;
		b = a * gohanPct / 100.0;
		numer += b * d;
		denom += b;
	}
	return numer / denom;
}
static void Pet2(uint starvDay, uint azukeDay, uint gohanPct)
{
	double aliveRate;

	cout("STARV_DAY: %u\n", starvDay);
	cout("AZUKE_DAY: %u\n", azukeDay);
	cout("GOHAN_PCT: %u\n", gohanPct);

	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 1, 100));

	if(azukeDay < starvDay)
	{
		aliveRate = 1.0;
	}
	else
	{
		uint d = azukeDay - starvDay + 1;
		double rouletteDeathRate;
		double stepDay;
		double rouletteCount;
		double deathRate;

		rouletteDeathRate = GetRouletteDeathRate(starvDay, gohanPct);
		stepDay = GetStepDay(starvDay, gohanPct);
		rouletteCount = d / stepDay;
		deathRate = 1.0 - pow(1.0 - rouletteDeathRate, rouletteCount);

		cout("--\n");
		cout("d: %u\n", d);
		cout("rouletteDeathRate: %.3f\n", rouletteDeathRate);
		cout("stepDay: %.3f\n", stepDay);
		cout("rouletteCount: %.3f\n", rouletteCount);
		cout("deathRate: %.3f\n", deathRate);

		aliveRate = 1.0 - deathRate;
	}
	cout("--\n");
	cout("ALIVE_PCT: %.3f PCT\n", aliveRate * 100.0);
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
