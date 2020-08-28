#include "C:\Factory\Common\all.h"

#define AZUKE_DAY_LMT 100

static double RateTable[AZUKE_DAY_LMT][AZUKE_DAY_LMT]; // [預ける日数][死のルーレットを回す回数] == rate

/*
	ret: ((100 - gohanPct) / 100.0) ^ (d - 1) * (gohanPct / 100.0)
*/
static double GetStepDayRate_N(uint d, uint gohanPct) // ret: 餓死するケースを含む確率
{
	double rate = gohanPct / 100.0;

	while(--d)
		rate *= (100 - gohanPct) / 100.0;

	return rate;
}
static double GetStepDayRate(uint d, uint gohanPct, uint starvDay) // ret: 餓死するケースを除く確率
{
	double numer = GetStepDayRate_N(d, gohanPct);
	double denom = 0.0;
	uint s;

	for(s = 1; s <= starvDay; s++)
		denom += GetStepDayRate_N(s, gohanPct);

	return numer / denom;
}
static void Main2(uint starvDay, uint gohanPct)
{
	uint azukeDay;
	uint rltCnt;
	FILE *fp;

	cout("STARV_DAY: %u\n", starvDay);
	cout("GOHAN_PCT: %u\n", gohanPct);

	errorCase(!m_isRange(starvDay, 1, AZUKE_DAY_LMT));
	errorCase(!m_isRange(gohanPct, 0, 100));

	for(azukeDay = 0; azukeDay < starvDay; azukeDay++)
	{
		RateTable[azukeDay][0] = 1.0;
	}
	for(azukeDay = starvDay; azukeDay < AZUKE_DAY_LMT; azukeDay++)
	{
		uint d;

		for(d = 1; d <= starvDay; d++)
		{
			double a = GetStepDayRate(d, gohanPct, starvDay);

			for(rltCnt = 1; rltCnt < AZUKE_DAY_LMT; rltCnt++)
			{
				RateTable[azukeDay][rltCnt] += RateTable[azukeDay - d][rltCnt - 1] * a;
			}
		}
	}
	fp = fileOpen_xc(getOutFile("rltRate.csv"), "wt");

	writeLine_x(fp, xcout("STARV_DAY %u", starvDay));
	writeLine_x(fp, xcout("GOHAN_PCT %u", gohanPct));

	writeToken(fp, "AZUKE_DAY ＼ RLT_CNT");

	for(rltCnt = 0; rltCnt < AZUKE_DAY_LMT; rltCnt++)
	{
		writeToken_x(fp, xcout(",%u", rltCnt));
	}
	writeChar(fp, '\n');
//	writeLine(fp, ",TOTAL"); // test

	for(azukeDay = 0; azukeDay < AZUKE_DAY_LMT; azukeDay++)
	{
//		double total = 0.0; // test

		writeToken_x(fp, xcout("%u", azukeDay));

		for(rltCnt = 0; rltCnt < AZUKE_DAY_LMT; rltCnt++)
		{
			writeToken_x(fp, xcout(",%.10f", RateTable[azukeDay][rltCnt]));
//			total += RateTable[azukeDay][rltCnt]; // test
		}
		writeChar(fp, '\n');
//		writeLine_x(fp, xcout(",%.10f", total)); // test
	}
	fileClose(fp);
	openOutDir();
}
int main(int argc, char **argv)
{
	Main2(toValue(getArg(0)), toValue(getArg(1)));
}
