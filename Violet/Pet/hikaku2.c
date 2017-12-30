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

static autoList_t *Pet2_Cache;

static double Pet2(uint starvDay, uint azukeDay, uint gohanPct)
{
	autoList_t *cache;

	errorCase(!m_isRange(starvDay, 0, STARV_DAY_MAX));
	errorCase(!m_isRange(azukeDay, 0, AZUKE_DAY_MAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	cache = refList(refList(refList(Pet2_Cache, starvDay), azukeDay), gohanPct);

	if(!getCount(cache))
	{
		double *p = (double *)memAlloc(sizeof(double));

LOGPOS();
		*p = Pet2_Main(starvDay, azukeDay, gohanPct);
LOGPOS();

		addElement(cache, (uint)p);
	}
	return *(double *)getElement(cache, 0);
}

// ---- Pet4 ----

static double P(double a, uint s)
{
	if(s == 0)
	{
		return 1.0;
	}
	if(s == 1)
	{
		return a;
	}
	if(s % 2 == 1)
	{
		return P(a * a, s / 2) * a;
	}
	else
	{
		return P(a * a, s / 2);
	}
}

double G(double a, uint s, uint n, uint d);

static double G_Main(double a, uint s, uint n, uint d)
{
	if(d < s)
	{
		return n == 0 ? 1.0 : 0.0;
	}
	if(n == 0)
	{
		return 0.0;
	}
	else
	{
		double ret = 0.0;
		uint t;

		for(t = 1; t <= s; t++)
		{
			ret += G(a, s, n - 1, d - t) * P(a, t - 1);
		}
		return ret * (1.0 - a) / (1.0 - P(a, s));
	}
}

static autoList_t *G_Cache;

static double G(double a, uint s, uint n, uint d)
{
	autoList_t *cache = refList(refList(G_Cache, n), d); // a, s ‚ÍŒÅ’è

	if(getCount(cache) == 0)
	{
		double *p = (double *)memAlloc(sizeof(double));

		*p = G_Main(a, s, n, d);

		addElement(cache, (uint)p);
	}
	return *(double *)getElement(cache, 0);
}
static double F(uint s, uint d, double a)
{
	double ret = 0.0;
	uint n;

	for(n = 0; n + s <= d + 1 || !n; n++) // n = 0, 1, 2, ... max(0, d + 1 - s)
	{
		ret += G(a, s, n, d) * P(1.0 - P(a, s), n);
	}
	return ret;
}
static double Pet4(uint starvDay, uint azukeDay, uint gohanPct)
{
	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	// init G_Cache
	{
		if(G_Cache)
			releaseDim(G_Cache, 3);

		G_Cache = newList();
	}

	if(!gohanPct)
	{
		return azukeDay < starvDay ? 100.0 : 0.0;
	}
	else
	{
		return F(starvDay, azukeDay, (100 - gohanPct) / 100.0) * 100.0;
	}
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
		case 2: addElement(row, (uint)strx("MODE -- PET4")); break;
		case 3: addElement(row, (uint)strx("MODE -- PET4 - PET2")); break;

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

LOGPOS();
				switch(mode)
				{
				case 1: pct = Pet2(starvDay, azukeDay, gohanPct); break;
				case 2: pct = Pet4(starvDay, azukeDay, gohanPct); break;
				case 3: pct = Pet4(starvDay, azukeDay, gohanPct) - Pet2(starvDay, azukeDay, gohanPct); break;

				default:
					error();
				}
LOGPOS();
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

	Pet2_Cache = newList();
//	G_Cache = newList();

	Main2();
}
