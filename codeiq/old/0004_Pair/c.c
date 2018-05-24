#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"

int main(int argc, char **argv)
{
	char *rFile;
	char *wFile1;
	char *wFile2;
	uint h;
	uint w;
	uint r;
	uint c;
	uint pct;
	uint yesnum = 0;
	uint nonum = 0;

	mt19937_init();

	while(!waitKey(0))
	{
		h = mt19937_range(1, 10);
		w = mt19937_range(1, 10);
		pct = mt19937_range(0, 100);

		{
			FILE *fp = fileOpen("input.tmp", "wt");

			for(r = 0; r < h; r++)
			{
				for(c = 0; c < w; c++)
				{
					writeChar(fp, "OX"[mt19937_rnd(100) < pct]);
				}
				writeChar(fp, '\n');
			}
			fileClose(fp);
		}

		execute("TYPE input.tmp");

		coExecute("< input.tmp a.exe > 1.tmp");
		execute("TYPE 1.tmp");

		coExecute("< input.tmp b.exe > 2.tmp");
		execute("TYPE 2.tmp");

		errorCase(!isSameFile("1.tmp", "2.tmp"));

		{
			char *line = readFirstLine("1.tmp");

			if(!strcmp(line, "yes"))
				yesnum++;
			else if(!strcmp(line, "no"))
				nonum++;
			else
				error();

			memFree(line);
		}

		execute_x(xcout("TITLE Y:%u N:%u", yesnum, nonum));
	}
}
