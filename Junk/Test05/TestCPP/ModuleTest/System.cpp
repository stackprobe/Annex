#include "all.h"

static oneObject(autoList<void (*)(void)>, new autoList<void (*)(void)>(), GetFinalizers)

void addFinalizer(void (*func)(void))
{
	GetFinalizers()->AddElement(func);
}
void (*unaddFinalizer(void))(void)
{
	return GetFinalizers()->UnaddElement();
}

void termination(int errorlevel)
{
	while(GetFinalizers()->GetCount())
	{
		GetFinalizers()->UnaddElement()();
	}
	exit(errorlevel);
}
void error2(char *source, int lineno, char *function)
{
	{
		static int once;

		if(once)
			exit(2);

		once = 1;
	}

	cout("Error %s %d %s\n", source, lineno, function);

	MessageBox(NULL, xcout("Error %s %d %s", source, lineno, function), "HTT Error", MB_OK | MB_ICONSTOP);

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

static int ArgIndex = 1;

int hasArgs(int count)
{
	return count <= __argc - ArgIndex;
}
int argIs(char *spell)
{
	if(ArgIndex < __argc)
	{
		if(!_stricmp(__argv[ArgIndex], spell))
		{
			ArgIndex++;
			return 1;
		}
	}
	return 0;
}
char *getArg(int index)
{
	errorCase(index < 0 || __argc - ArgIndex <= index);
	return __argv[ArgIndex + index];
}
char *nextArg(void)
{
	char *arg = getArg(0);

	ArgIndex++;
	return arg;
}
int getArgIndex(void)
{
	return ArgIndex;
}
void setArgIndex(int index)
{
	errorCase(index < 0 || __argc < index); // index == __argc は全部読み終わった状態
	ArgIndex = index;
}

char *getTempRtDir(void)
{
	static char *dir;
	
	if(!dir)
	{
		dir = getenv("TMP");

		if(!dir)
			dir = getenv("TEMP");

		errorCase(!existDir(dir));
	}
	return dir;
}
char *makeTempPath(char *suffix)
{
	for(; ; )
	{
		char *pw = makePw36();
		char *path = combine_cx(getTempRtDir(), xcout("HTT_%s%s", pw, suffix));
		memFree(pw);

		if(!existPath(path))
			return path;

		memFree(path);
	}
}
char *makeTempFile(char *suffix)
{
	char *out = makeTempPath(suffix);
	createFile(out);
	return out;
}
char *makeTempDir(char *suffix)
{
	char *out = makeTempPath(suffix);
	createDir(out);
	return out;
}

double dNow(void)
{
	return clock() / (double)CLOCKS_PER_SEC;
}
time_t now(void)
{
	static time_t firstAccessTime = -1;

	if(firstAccessTime == -1)
		firstAccessTime = time(NULL);

	return time(NULL) - firstAccessTime;
//	return clock() / (double)CLOCKS_PER_SEC;
}
int iNow(void)
{
	return (int)now();
}
