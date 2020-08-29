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
			startsWith(line1, " i") &&
			startsWith(line2, " i")
			)
		{
			char *line = getLine(lines, index - 2);
			char *p = line1 + 3;
			char *q;
			char *r;

			q = ne_strstr(p, "”N");
			q[0] = 0;
			q += 2;

			r = ne_strstr(q, "Œ");
			r[0] = 0;
			r += 2;

			ne_strstr(r, "“ú")[0] = 0;

			cout("%u %s\n", atoi(p) * 10000 + atoi(q) * 100 + atoi(r), line);
		}
	}
}
