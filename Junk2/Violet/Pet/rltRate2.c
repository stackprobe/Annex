#include "C:\Factory\Common\all.h"

#define AZUKE_DAY_LMT 10

/*
	r1 == 明日飯食える確率
	r2 == 明後日飯食える確率
	r3 == 3日後飯食える確率
	...

	餓死する確率を除く
*/
static char *RateTable[AZUKE_DAY_LMT][AZUKE_DAY_LMT]; // [預ける日数][死のルーレットを回す回数] == rate

static void Main2(uint starvDay)
{
	uint azukeDay;
	uint rltCnt;
	FILE *fp;

	cout("STARV_DAY: %u\n", starvDay);

	errorCase(!m_isRange(starvDay, 1, AZUKE_DAY_LMT));

	for(azukeDay = 0; azukeDay < AZUKE_DAY_LMT; azukeDay++)
	for(rltCnt = 0; rltCnt < AZUKE_DAY_LMT; rltCnt++)
	{
		if(azukeDay < starvDay)
		{
			RateTable[azukeDay][rltCnt] = strx(rltCnt == 0 ? "1" : "0");
		}
		else if(azukeDay == starvDay)
		{
			RateTable[azukeDay][rltCnt] = strx(rltCnt == 1 ? "1" : "0");
		}
		else if(rltCnt == 0)
		{
			RateTable[azukeDay][rltCnt] = strx("0");
		}
		else
		{
			autoList_t *terms = newList();
			char *term;
			uint d;

			for(d = 1; d <= starvDay; d++)
			{
				char *rate = RateTable[azukeDay - d][rltCnt - 1];

				if(!strcmp(rate, "0"))
				{
					term = NULL;
				}
				else if(!strcmp(rate, "1"))
				{
					term = xcout("r%u", d);
				}
				else if(strchr(rate, '+'))
				{
					term = xcout("r%u*(%s)", d, rate);
				}
				else
				{
					term = xcout("r%u*%s", d, rate);
				}
				if(term)
					addElement(terms, (uint)term);
			}
			if(getCount(terms))
				term = untokenize_xc(terms, "+");
			else
				term = strx("0");

			RateTable[azukeDay][rltCnt] = term;
		}
	}
	fp = fileOpen_xc(getOutFile("rltRate2.csv"), "wt");

	writeLine_x(fp, xcout("STARV_DAY %u", starvDay));

	writeToken(fp, "AZUKE_DAY ＼ RLT_CNT");

	for(rltCnt = 0; rltCnt < AZUKE_DAY_LMT; rltCnt++)
	{
		writeToken_x(fp, xcout(",%u", rltCnt));
	}
	writeChar(fp, '\n');

	for(azukeDay = 0; azukeDay < AZUKE_DAY_LMT; azukeDay++)
	{
		writeToken_x(fp, xcout("%u", azukeDay));

		for(rltCnt = 0; rltCnt < AZUKE_DAY_LMT; rltCnt++)
		{
			writeToken_x(fp, xcout(",%s", RateTable[azukeDay][rltCnt]));
		}
		writeChar(fp, '\n');
	}
	fileClose(fp);
	openOutDir();
}
int main(int argc, char **argv)
{
	Main2(toValue(nextArg()));
}
