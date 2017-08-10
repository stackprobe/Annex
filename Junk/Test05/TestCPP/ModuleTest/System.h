void addFinalizer(void (*func)(void));
void (*unaddFinalizer(void))(void);

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

int hasArgs(int count);
int argIs(char *spell);
char *getArg(int index);
char *nextArg(void);
int getArgIndex(void);
void setArgIndex(int index);

char *getTempRtDir(void);
char *makeTempPath(char *suffix = ".tmp");
char *makeTempFile(char *suffix = ".tmp");
char *makeTempDir(char *suffix = ".tmp");

double dNow(void);
time_t now(void);
int iNow(void);
