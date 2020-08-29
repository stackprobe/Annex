#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"
#include "C:\Factory\Common\Options\csv.h"

#if 0 // full

	#define STARV_DAY_MIN 1
	#define STARV_DAY_MAX 30
	#define STARV_DAY_STEP 1

	#define AZUKE_DAY_MIN 1
	#define AZUKE_DAY_MAX 300
	#define AZUKE_DAY_STEP 1

	#define NO_GHN_PCT_STEP 1

#elif 0 // kizamu

	#define STARV_DAY_MIN 1
	#define STARV_DAY_MAX 10
	#define STARV_DAY_STEP 1

	#define AZUKE_DAY_MIN 1
	#define AZUKE_DAY_MAX 30
	#define AZUKE_DAY_STEP 1

	#define NO_GHN_PCT_STEP 10

#elif 0 // hiroku

	#define STARV_DAY_MIN 5
	#define STARV_DAY_MAX 30
	#define STARV_DAY_STEP 5

	#define AZUKE_DAY_MIN 10
	#define AZUKE_DAY_MAX 300
	#define AZUKE_DAY_STEP 10

	#define NO_GHN_PCT_STEP 10

#else // easy

	#define STARV_DAY_MIN 2
	#define STARV_DAY_MAX 10
	#define STARV_DAY_STEP 2

	#define AZUKE_DAY_MIN 10
	#define AZUKE_DAY_MAX 60
	#define AZUKE_DAY_STEP 5

	#define NO_GHN_PCT_STEP 20

#endif

// ---- Pet2 ----

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
static double Pet2_Main(uint starvDay, uint azukeDay, uint gohanPct)
{
	uint numer = 0;
	uint denom;

	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	for(denom = 0; denom < TEST_COUNT; denom++)
		if(Azukeru(starvDay, azukeDay, gohanPct))
			numer++;

	return numer * 100.0 / denom;
}
static double Pet2(uint starvDay, uint azukeDay, uint gohanPct)
{
	static double *(*cache)[AZUKE_DAY_MAX + 1][100 + 1];
//	static double *cache[STARV_DAY_MAX + 1][AZUKE_DAY_MAX + 1][100 + 1];

	errorCase(!m_isRange(starvDay, 0, STARV_DAY_MAX));
	errorCase(!m_isRange(azukeDay, 0, AZUKE_DAY_MAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	if(!cache)
		cache = (double *(*)[AZUKE_DAY_MAX + 1][100 + 1])memAlloc(sizeof(*cache) * (STARV_DAY_MAX + 1));

	if(!cache[starvDay][azukeDay][gohanPct])
	{
		double *p = (double *)memAlloc(sizeof(double));

LOGPOS();
		*p = Pet2_Main(starvDay, azukeDay, gohanPct);
LOGPOS();

		cache[starvDay][azukeDay][gohanPct] = p;
	}
	return *cache[starvDay][azukeDay][gohanPct];
}

// ---- Pet3 ----

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

	b = gohanPct / 100.0;
	numer = b;
	denom = b;

	for(d = 2; d <= starvDay; d++)
	{
		a *= (100 - gohanPct) / 100.0;
		b = a * gohanPct / 100.0;
		numer += b * d;
		denom += b;
	}
	return numer / denom;
}
static double Pet3(uint starvDay, uint azukeDay, uint gohanPct)
{
	double aliveRate;

	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	if(azukeDay < starvDay)
	{
		aliveRate = 1.0;
	}
	else if(!gohanPct)
	{
		aliveRate = 0.0;
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
	}
	return aliveRate * 100.0;
}

// ----

static void Main2(void)
{
	uint starvDay;
	uint mode;
	uint azukeDay;
	uint noGhnPct;

LOGPOS();
	for(starvDay = STARV_DAY_MIN; starvDay <= STARV_DAY_MAX; starvDay += STARV_DAY_STEP)
	for(mode = 1; mode <= 3; mode++)
	{
		autoList_t *rows = newList();
		autoList_t *row = newList();

LOGPOS();
		addElement(row, (uint)xcout("ALIVE_PCT_TABLE -- STARV_DAY %u", starvDay));
		addElement(rows, (uint)row);
		row = newList();

		switch(mode)
		{
		case 1: addElement(row, (uint)strx("MODE -- PET2")); break;
		case 2: addElement(row, (uint)strx("MODE -- PET3")); break;
		case 3: addElement(row, (uint)strx("MODE -- PET3 - PET2")); break;

		default:
			error();
		}
		addElement(rows, (uint)row);
		row = newList();
		addElement(row, (uint)strx("NO_GHN_PCT _ AZUKE_DAY"));

		for(azukeDay = AZUKE_DAY_MIN; azukeDay <= AZUKE_DAY_MAX; azukeDay += AZUKE_DAY_STEP)
		{
			addElement(row, (uint)xcout("%u", azukeDay));
		}
		addElement(rows, (uint)row);

		for(noGhnPct = 0; noGhnPct <= 100; noGhnPct += NO_GHN_PCT_STEP)
		{
			uint gohanPct = 100 - noGhnPct;

			row = newList();
			addElement(row, (uint)xcout("%u", noGhnPct));

			for(azukeDay = AZUKE_DAY_MIN; azukeDay <= AZUKE_DAY_MAX; azukeDay += AZUKE_DAY_STEP)
			{
				double pct;

//cout("starvDay mode noGhnPct azukeDay: %u %u %u %u\n", starvDay, mode, noGhnPct, azukeDay);
				switch(mode)
				{
				case 1: pct = Pet2(starvDay, azukeDay, gohanPct); break;
				case 2: pct = Pet3(starvDay, azukeDay, gohanPct); break;
				case 3: pct = Pet3(starvDay, azukeDay, gohanPct) - Pet2(starvDay, azukeDay, gohanPct); break;

				default:
					error();
				}
//LOGPOS();
				addElement(row, (uint)xcout("%.3f", pct));
			}
			addElement(rows, (uint)row);
		}
LOGPOS();
		writeCSVFile_xx(getOutFile_x(xcout("Pet_StarvDay%02u_Mode%u.csv", starvDay, mode)), rows);
LOGPOS();
	}
LOGPOS();
	openOutDir();
LOGPOS();
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	Main2();
}
