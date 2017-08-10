void termination(int errorlevel);
void error2(char *source, int lineno, char *function);

#define error() \
	error2(__FILE__, __LINE__, __FUNCTION__)

#define errorCase(status) \
	do { \
	if((status)) error(); \
	} while(0)

#define LOGPOS() \
	cout("%s (%d) %s\n", __FILE__, __LINE__, __FUNCTION__)

void cout(char *format, ...);
char *xcout(char *format, ...);
