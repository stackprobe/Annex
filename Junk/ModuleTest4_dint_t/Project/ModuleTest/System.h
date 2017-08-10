void termination(int errorlevel);
void error2(char *source, int lineno, char *function);

#define error() \
	error2(__FILE__, __LINE__, __FUNCTION__)

#define errorCase(status) \
	do { \
	if((status)) error(); \
	} while(0)

void cout(char *format, ...);
char *xcout(char *format, ...);

uint64 nowTick(void);
uint now(void);
char *LOGPOS_Time(void);

#define LOGPOS() \
	cout("%s (%d) %s %u %s\n", getLocal(__FILE__), __LINE__, __FUNCTION__, GetCurrentThreadId(), LOGPOS_Time())
