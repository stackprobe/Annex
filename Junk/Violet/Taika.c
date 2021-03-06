#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *lines = readLines(c_dropFile());
	uint index;

	for(index = 2; ; index++)
	{
		char *line1 = getLine(lines, index);
		char *line2 = getLine(lines, index + 1);

		if(
			startsWith(line1, " （") &&
			startsWith(line2, " （")
			)
		{
			char *line = getLine(lines, index - 2);
			char *p = line1 + 3;
			char *q;
			char *r;

			q = ne_strstr(p, "年");
			q[0] = 0;
			q += 2;

			r = ne_strstr(q, "月");
			r[0] = 0;
			r += 2;

			ne_strstr(r, "日")[0] = 0;

			cout("%u %s\n", atoi(p) * 10000 + atoi(q) * 100 + atoi(r), line);
		}
	}
}
