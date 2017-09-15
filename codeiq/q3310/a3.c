#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>

#define LINE_MAX 32

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
	void **head;
	void ***nextp;
	void **curr;
	int longest = 0;

	nextp = &head;

	for(; ; )
	{
		char *line = ReadLine();

		if(!line)
			break;

		*nextp = (void **)malloc(sizeof(void *) + strlen(line) + 1);

		if(!*nextp) // ? malloc error
			abort();

		strcpy((char *)(*nextp + 1), line);
		nextp = (void ***)*nextp;

		longest = GetMax(longest, strlen(line));
	}
	*nextp = NULL;

	curr = head;

	while(curr)
	{
		int c;
		void **tmp;

		for(c = strlen((char *)(curr + 1)); c < longest; c++)
			printf("_");

		printf("%s\n", curr + 1);

		tmp = (void **)*curr;
		free(curr);
		curr = tmp;
	}
}
