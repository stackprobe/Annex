char *strx(char *line);
void strz(char *&buffer, char *line);

char *getConstNullString(void);
char *getNullString(void);

#define m_ismbc1(chr) \
	(_ismbblead((chr)))

#define m_ismbc2(chr) \
	(_ismbbtrail((chr)))

#define m_ismbc(p) \
	(m_ismbc1((p)[0]) && (p)[1])
//	(m_ismbc1((p)[0]) && m_ismbc2((p)[1]))

#define m_mbcnext(p) \
	((p) + (m_ismbc((p)) ? 2 : 1))

void replaceChar(char *line, int chr1, int chr2);
char *replaceLine(char *line, char *ptn1, char *ptn2);

autoList<char *> *tokenize(char *line, char *delimiters);
char *untokenize(autoList<char *> *tokens, char *separator);
