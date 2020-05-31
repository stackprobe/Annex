#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define NEST_MAX 1000
#define TAG_LENMAX 1000 // ï∂éöÅH
//#define TAG_LENMAX 100

static char NTags[NEST_MAX][TAG_LENMAX + 1];
static int Nest;
static int LineNum = 1;

static int ReadCh_E(void)
{
	int ch = fgetc(stdin);

	if(ch == '\n')
		LineNum++;

	return ch;
}
static int ReadCh(void)
{
	int ch = ReadCh_E();

	if(ch == EOF)
		exit(1); // fatal

	return ch;
}
static int SkipBlanks(void)
{
	int ch;

	while((ch = ReadCh()) <= ' ');

	return ch;
}
static void SkipComment(void)
{
	int h = 0;

	for(; ; )
	{
		int ch = ReadCh();

		if(ch == '-')
			h++;
		else if(2 <= h && ch == '>')
			break;
		else
			h = 0;
	}
}
static int IsUnclosableTag(char *tag)
{
	return !strcmp(tag, "li");
}
static int ReadTag(void)
{
	int ch = SkipBlanks();
	int closed = 0;
	int selfClosed = 0;
	int i;

	if(ch == '!')
	{
		ReadCh(); // '-'
		ReadCh(); // '-'
		SkipComment();
		return 1;
	}
	if(ch == '/')
	{
		closed = 1;
		ch = SkipBlanks();
	}
	for(i = 0; ; i++)
	{
		if('a' <= ch && ch <= 'z') // to upper
			ch += 'A' - 'a';

		NTags[Nest][i++] = ch;
		ch = ReadCh();

		if(ch <= ' ' || ch == '/' || ch == '>')
			break;
	}
	NTags[Nest++][i] = '\0';

	for(; ; )
	{
		if(ch == '/')
			selfClosed = 1;
		else if(ch == '>')
			break;
		else if(' ' < ch)
			selfClosed = 0;

		ch = ReadCh();
	}
	if(closed)
	{
		i = Nest - 1;
		Nest -= 2;

		for(; ; )
		{
			if(Nest < 0)
				return 0;

			if(!strcmp(NTags[Nest], NTags[i]))
				break;

			if(!IsUnclosableTag(NTags[Nest]))
				return 0;

			Nest--;
		}
	}
	else if(selfClosed)
	{
		Nest--;
	}
	return 1;
}
int main()
{
	int ans = 0;

	for(; ; )
	{
		int ch = ReadCh_E();

		if(ch == EOF)
			break;

		if(ch == '<' && !ReadTag())
		{
			ans = LineNum;
			break;
		}
	}
	printf("%d\n", ans);
}
