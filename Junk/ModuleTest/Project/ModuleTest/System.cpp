#include "all.h"

void termination(int errorlevel)
{
	exit(errorlevel);
}
void error2(char *source, int lineno, char *function)
{
	{
		static int entcnt;

		if(entcnt)
			exit(2);

		entcnt = 1;
	}

	cout("+-------+\n");
	cout("| ERROR |\n");
	cout("+-------+\n");
	cout("%s (%u) %s\n", source, lineno, function);

	termination(1);
}

void cout(char *format, ...)
{
	va_list marker;

	va_start(marker, format);
	errorCase(vprintf(format, marker) < 0);
	va_end(marker);
}
char *xcout(char *format, ...)
{
	char *buffer;
	int size;
	va_list marker;

	va_start(marker, format);

	for(size = strlen(format) + 128; ; size *= 2)
	{
		buffer = (char *)memAlloc(size + 20);
		int retval = _vsnprintf(buffer, size + 10, format, marker);
		buffer[size + 10] = '\0'; // ‹­§“I‚É•Â‚¶‚éB

		if(0 <= retval && retval <= size)
			break;

		memFree(buffer);
		errorCase(128 * 1024 * 1024 < size); // anti-overflow
	}
	va_end(marker);

	return buffer;
}
