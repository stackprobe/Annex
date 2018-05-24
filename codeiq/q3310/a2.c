#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

#define LINE_MAX 32

typedef struct Line_st
{
	char Line[LINE_MAX + 1];
	struct Line_st *Next;
}
Line_t;

static char *ReadLine(void)
{
	static char buff[LINE_MAX + 1];
	int i = 0;
	int c;

	for(; ; )
	{
		c = fgetc(stdin);

		if(c == '\r')
			continue;

		if(c == '\n' || c == EOF)
			break;

		if(LINE_MAX <= i) // ? will buffer overrun
			abort();

		buff[i++] = c;
	}
	if(!i && c == EOF)
		return NULL;

	buff[i] = '\0';
	return buff;
}
static int GetMax(int a, int b)
{
	return a < b ? b : a;
}
int main()
{
	Line_t *head;
	Line_t **nextp;
	Line_t *curr;
	int longest = 0;
	char unders[LINE_MAX + 1];

	nextp = &head;

	for(; ; )
	{
		char *line = ReadLine();

		if(!line)
			break;

		*nextp = (Line_t *)malloc(sizeof(Line_t));

		if(!*nextp) // ? malloc error
			abort();

		strcpy((*nextp)->Line, line);
		nextp = &((*nextp)->Next);

		longest = GetMax(longest, strlen(line));
	}
	*nextp = NULL;

	memset(unders, '_', LINE_MAX);
	unders[LINE_MAX] = '\0';

	curr = head;

	while(curr)
	{
		Line_t *tmp;

		printf("%s%s\n", unders + LINE_MAX - longest + strlen(curr->Line), curr->Line);

		tmp = curr->Next;
		free(curr);
		curr = tmp;
	}
}
