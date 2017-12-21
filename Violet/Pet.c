/*
	毎日１回決まった時間に餌やりが必要なペットがいて、そいつは一定の日数絶食すると餓死するとして、
	その餌やりを一定の確率で忘れる人に一定の日数間預けたとき、どれくらいの確率で死ぬか。

	starvDay == 餓死する連続絶食日数
	azukeDay == 預ける日数
	gohanPct == (１日の)餌やりする確率 (noGhnPct == (１日の)餌やりを忘れる確率)
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csv.h"
#include "C:\Factory\Common\Options\CRRandom.h"

//#define AZUKE_DAY_MAX 300
#define AZUKE_DAY_MAX 100

#define TEST_COUNT 1000000

static uint DeadDays[AZUKE_DAY_MAX + 2]; // [AZUKE_DAY_MAX + 1] == INFINITE
static uint DeadCounts[AZUKE_DAY_MAX + 2];

static int TryGohan(uint gohanPct)
{
	return mt19937_rnd32() * 100.0 / UINTMAX < gohanPct;
}
static uint DoTest_GetDeadDay(uint starvDay, uint gohanPct)
{
	uint noGhnDay = 0;
	uint day;

	for(day = 1; day <= AZUKE_DAY_MAX; day++)
	{
		if(TryGohan(gohanPct))
		{
			noGhnDay = 0;
		}
		else
		{
			noGhnDay++;

			if(starvDay <= noGhnDay)
				break;
		}
	}
	return day;
}
static void MakePcts(uint starvDay, uint gohanPct)
{
	uint count;
	uint azukeDay;
	autoList_t gal;

	zeroclear(DeadDays);

	for(count = 0; count < TEST_COUNT; count++)
	{
		DeadDays[DoTest_GetDeadDay(starvDay, gohanPct)]++;
	}
	DeadCounts[0] = DeadDays[0]; // = 0;

	for(azukeDay = 1; azukeDay <= AZUKE_DAY_MAX + 1; azukeDay++)
	{
		DeadCounts[azukeDay] = DeadCounts[azukeDay - 1] + DeadDays[azukeDay];
	}
}
static double GetPct(uint azukeDay)
{
	return DeadCounts[azukeDay] * 100.0 / TEST_COUNT;
}
static void Main2(void)
{
	uint starvDay;
	uint azukeDay;
	uint noGhnPct;

LOGPOS();
	for(starvDay = 1; starvDay <= 30; starvDay++)
	{
		autoList_t *rows = newList();
		autoList_t *row = newList();

LOGPOS();
		addElement(row, (uint)xcout("DEAD_PCT_TABLE -- STARV_DAY %u", starvDay));
		addElement(rows, (uint)row);
		row = newList();
		addElement(row, (uint)strx("NO_GHN_PCT ＼ AZUKE_DAY"));

		for(azukeDay = 0; azukeDay <= AZUKE_DAY_MAX; azukeDay++)
		{
			addElement(row, (uint)xcout("%u", azukeDay));
		}
//		addElement(row, (uint)strx("INFINITE")); // AZUKE_DAY_MAX + 1
		addElement(rows, (uint)row);

		for(noGhnPct = 0; noGhnPct <= 100; noGhnPct++)
		{
			uint gohanPct = 100 - noGhnPct;

			row = newList();
			addElement(row, (uint)xcout("%u", noGhnPct));

LOGPOS();
			MakePcts(starvDay, gohanPct);
LOGPOS();

			for(azukeDay = 0; azukeDay <= AZUKE_DAY_MAX; azukeDay++)
//			for(azukeDay = 0; azukeDay <= AZUKE_DAY_MAX + 1; azukeDay++)
			{
				addElement(row, (uint)xcout("%.3f", GetPct(azukeDay)));
			}
			addElement(rows, (uint)row);
		}
LOGPOS();
		writeCSVFile_xx(getOutFile_x(xcout("Pet_StarvDay%02u.csv", starvDay)), rows);
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
