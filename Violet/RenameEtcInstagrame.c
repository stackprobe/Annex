#include "C:\Factory\Common\all.h"

#define RW_DIR "C:\\etc\\Instagram"

static void Main2(void)
{
	autoList_t *files = lsFiles(RW_DIR);
	char *file;
	uint index;

	eraseParents(files);
	sortJLinesICase(files);
	addCwd(RW_DIR);

	foreach(files, file, index)
	{
		cout("< %s\n", file);

		if(lineExp("<18,09>.jpg", file))
		{
			char *fileNew = NULL;
			uint y = toValue_x(strxl(file +  0, 4));
			uint m = toValue_x(strxl(file +  4, 2));
			uint d = toValue_x(strxl(file +  6, 2));
			uint h = toValue_x(strxl(file +  8, 2));
			uint i = toValue_x(strxl(file + 10, 2));
			uint s = toValue_x(strxl(file + 12, 2));
			uint c = toValue_x(strxl(file + 14, 4));

			cout("%04u/%02u/%02u %02u:%02u:%02u.%04u\n", y, m, d, h, i, s, c);

			if(
				y != 0 &&
				m == 0 &&
				d == 0 &&
				h == 0 &&
				i == 0 &&
				s == 0 &&
				c != 0
				)
				fileNew = xcout("IMG_%04u0000_00%04u.jpg", y, c);

			if(
				y != 0 &&
				m != 0 &&
				d != 0 &&
				// h
				// i
				// s
				c == 0
				)
				fileNew = xcout("IMG_%04u%02u%02u_%02u%02u%02u.jpg", y, m, d, h, i, s);

			if(fileNew)
			{
				cout("> %s\n", fileNew);

				coExecute_x(xcout("REN %s %s", file, fileNew));

				memFree(fileNew);
			}
		}
	}
	unaddCwd();
	releaseDim(files, 1);
}
int main(int argc, char **argv)
{
	errorCase(!argIs("unsafe")); // for safety

	Main2();
}
