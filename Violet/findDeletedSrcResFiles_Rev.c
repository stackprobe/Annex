#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *files = lssFiles("C:\\Dev");
	char *file;
	uint index;

	addElements_x(files, lssFiles("C:\\Factory"));
	addElements_x(files, lssFiles("C:\\home\\bat"));
	addElements_x(files, lssFiles("C:\\var\\bat"));

	foreach(files, file, index)
	{
		if(lineExp("<>.<1,5,09AZaz> - <>.txt", file))
		{
			char *wFile;
			char *p;

			cout("< %s\n", file);

			errorCase(!lineExp("<>.<1,5,09AZaz> - <1,100,09AZaz>.txt", file));

			wFile = strx(file);

			p = strrstr(wFile, " - ");
			errorCase(!p);
			*p = '\0';
			p += 3;

			wFile = addLine_x(wFile, xcout("_%s", p));

			cout("> %s\n", wFile);

			coExecute_x(xcout("REN \"%s\" \"%s\"", file, getLocal(wFile)));
		}
	}
	releaseDim(files, 1);
}
