#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static double GetRouletteDeathRate(uint starvDay, uint gohanPct)
{
	return pow((100 - gohanPct) / 100.0, starvDay);
}
static double GetStepDay(uint starvDay, uint gohanPct)
{
	double numer;
	double denom;
	double a = 1.0;
	double b;
	uint d;

	/*
		1日後に餌が食える確率は gohanPct / 100.0
	*/
	b = gohanPct / 100.0;
	numer = b;
	denom = b;

	/*
		d日後に餌が食える確率は ((noGhnPct / 100.0) ^ (d - 1)) * (gohanPct / 100.0)

		次に餌が食えるのは何日後か、期待値は、A / B

			A =
				1日後に餌が食える確率 * 1 +
				2日後に餌が食える確率 * 2 +
				3日後に餌が食える確率 * 3 +
				...
				starvDay日後に餌が食える確率 * starvDay

			B =
				1日後に餌が食える確率 +
				2日後に餌が食える確率 +
				3日後に餌が食える確率 +
				...
				starvDay日後に餌が食える確率

			B = 餓死しない確率
	*/
	for(d = 2; d <= starvDay; d++)
	{
		a *= (100 - gohanPct) / 100.0;
		b = a * gohanPct / 100.0;
		numer += b * d;
		denom += b;
	}
	// denom == 1.0 - GetRouletteDeathRate(starvDay, gohanPct)
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
		uint d = azukeDay + 1 - starvDay;
		double rouletteDeathRate;
		double rouletteAliveRate;
		double stepDay;
		double rouletteCount;
		double deathRate;

		rouletteDeathRate = GetRouletteDeathRate(starvDay, gohanPct);
		rouletteAliveRate = 1.0 - rouletteDeathRate;
		stepDay = GetStepDay(starvDay, gohanPct);
		rouletteCount = d / stepDay;
		aliveRate = pow(rouletteAliveRate, rouletteCount);

		cout("--\n");
		cout("d: %u\n", d);
		cout("rouletteDeathRate: %.3f\n", rouletteDeathRate);
		cout("rouletteAliveRate: %.3f\n", rouletteAliveRate);
		cout("stepDay: %.3f\n", stepDay);
		cout("rouletteCount: %.3f\n", rouletteCount);
		cout("aliveRate: %.3f\n", aliveRate);
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
