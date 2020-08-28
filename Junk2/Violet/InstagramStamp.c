#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\TimeData.h"

#define RFILE "C:\\etc\\Instagram.stamp"
#define WFILE "C:\\etc\\Instagram.stamp_"

#define STAMPMAX 99991231235959999ui64

static uint64 StampPrev1Sec(uint64 stamp)
{
	TimeData_t td;
	uint64 t;

	errorCase(TimeData2Stamp(Stamp2TimeData(stamp / 1000)) != stamp / 1000);

	td = Stamp2TimeData(stamp / 1000);
	t = TimeData2TSec(td) * 1000 + (stamp % 1000);

	t -= 1000;

	td = TSec2TimeData(t / 1000);
	stamp = TimeData2Stamp(td);
	stamp *= 1000;
	stamp += t % 1000;

	return stamp;
}
int main(int argc, char **argv)
{
	autoList_t *lines = readLines(RFILE);
	char *line;
	uint index;
	uint64 nextMax1 = STAMPMAX;
	uint64 nextMax2 = STAMPMAX;
	uint64 nextMax3 = STAMPMAX;

	reverseElements(lines);

	foreach(lines, line, index)
	{
		autoList_t *tokens = tokenize(line, ' ');
		uint64 stamp1;
		uint64 stamp2;
		uint64 stamp3;

		errorCase(getCount(tokens) != 4); // ファイル名に空白は無いはず。

		stamp1 = toValue64(getLine(tokens, 0));
		stamp2 = toValue64(getLine(tokens, 1));
		stamp3 = toValue64(getLine(tokens, 2));

		m_minim(stamp1, nextMax1);
		m_minim(stamp2, nextMax2);
		m_minim(stamp3, nextMax3);

		memFree(line);
		line = xcout("%I64u %I64u %I64u %s", stamp1, stamp2, stamp3, getLine(tokens, 3));
		setElement(lines, index, (uint)line);

		nextMax1 = StampPrev1Sec(stamp1);
		nextMax2 = StampPrev1Sec(stamp2);
		nextMax3 = StampPrev1Sec(stamp3);
	}
	reverseElements(lines);
	writeLines_cx(WFILE, lines);
}
