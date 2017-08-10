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
		buffer[size + 10] = '\0'; // 強制的に閉じる。

		if(0 <= retval && retval <= size)
			break;

		memFree(buffer);
		errorCase(128 * 1024 * 1024 < size); // anti-overflow
	}
	va_end(marker);

	return buffer;
}

uint64 nowTick(void)
{
//	uint currTick = GetTickCount_TEST();
	uint currTick = GetTickCount();
	static uint lastTick;
	static uint64 baseTick;
	uint64 retTick;
	static uint64 lastRetTick;

	if(currTick < lastTick) // ? カウンタが戻った -> オーバーフローした？
	{
		uint diffTick = lastTick - currTick;

		if(_UI32_MAX / 2 < diffTick) // オーバーフローだろう。
		{
			LOGPOS();
			baseTick += (uint64)UINT_MAX + 1;
		}
		else // オーバーフローか？
		{
			LOGPOS();
			baseTick += diffTick; // 前回と同じ戻り値になるように調整する。
		}
	}
	lastTick = currTick;
	retTick = baseTick + currTick;
	errorCase(retTick < lastRetTick); // 2bs
	lastRetTick = retTick;
	return retTick;
}
uint now(void)
{
	return (uint)(nowTick() / 1000);
}
char *LOGPOS_Time(void)
{
	static char buff[23]; // UINT64MAX -> "307445734561825:51.615"
	uint64 millis = nowTick();

	sprintf(buff, "%I64u:%02u.%03u", millis / 60000, (uint)((millis / 1000) % 60), (uint)(millis % 1000));

	return buff;
}
