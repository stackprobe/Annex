/*

s = ‰ì€‚·‚é“ú”	1, 2, 3, ...
d = —a‚¯‚é“ú”		1, 2, 3, ...
a = ‰a‚ğ–Y‚ê‚éŠm—¦	0 <=, < 1

f(s,d,a) =	¶‚«‚Ä‹A‚Á‚Ä‚­‚éŠm—¦


			‡
f(s,d,a) =	ƒ°			{ g(s, d, a, n) * (1 - a^s)^n }
			n=0,1,2,...


g(s,d,a,n) =
	if d <  s AND n == 0 then	1
	if d <  s AND n != 0 then	0
	if d >= s AND n == 0 then	0

								s
	if d >= s AND n != 0 then	ƒ°			{ g(a, s, n - 1, d - t) * a^(t - 1) * (1 - a) / (1 - a^s) }
								t=1,2,3,...

*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

/*
	ret: a ^ s
*/
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
#if 0
	if(d == s)
	{
		return n == 1 ? 1.0 : 0.0;
	}
#endif
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
/*
	s = ‰ì€‚·‚é“ú”	1, 2, 3, ...
	d = —a‚¯‚é“ú”		1, 2, 3, ...
	a = ‰a‚ğ–Y‚ê‚éŠm—¦	0 <=, < 1

	ret: ¶‚«‚Ä‹A‚Á‚Ä‚­‚éŠm—¦
*/
static double F(uint s, uint d, double a)
{
#if 0
	if(d < s)
	{
		return 1.0;
	}
	else
	{
		double ret = 0.0;
		uint n;

		for(n = 1; n + s <= d + 1; n++) // n = 1, 2, 3, ... (d + 1 - s)
		{
			ret += G(a, s, n, d) * P(1.0 - P(a, s), n);
		}
		return ret;
	}
#else
	{
		double ret = 0.0;
		uint n;

		for(n = 0; n + s <= d + 1 || !n; n++) // n = 0, 1, 2, ... max(0, d + 1 - s)
		{
			ret += G(a, s, n, d) * P(1.0 - P(a, s), n);
		}
		return ret;
	}
#endif
}
static void Pet4(uint starvDay, uint azukeDay, uint gohanPct)
{
	double aliveRate;

	cout("STARV_DAY: %u\n", starvDay);
	cout("AZUKE_DAY: %u\n", azukeDay);
	cout("GOHAN_PCT: %u\n", gohanPct);

	errorCase(!m_isRange(starvDay, 1, IMAX));
	errorCase(!m_isRange(azukeDay, 1, IMAX));
	errorCase(!m_isRange(gohanPct, 0, 100));

	if(!gohanPct)
	{
		aliveRate = azukeDay < starvDay ? 1.0 : 0.0;
	}
	else
	{
		aliveRate = F(starvDay, azukeDay, (100 - gohanPct) / 100.0);
	}
	cout("--\n");
	cout("ALIVE_PCT: %.3f PCT\n", aliveRate * 100.0);
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	G_Cache = newList();

	Pet4(
		toValue(getArg(0)),
		toValue(getArg(1)),
		toValue(getArg(2))
		);
}
