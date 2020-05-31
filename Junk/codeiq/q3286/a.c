#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <malloc.h>

static void Error(void)
{
	printf("FATAL ERROR !!!\n");
	exit(1);
}
static void *MemRealloc(void *block, size_t size)
{
	block = realloc(block, size);

	if(!block)
		Error();

	return block;
}
static void *MemAlloc(size_t size)
{
	return MemRealloc(NULL, size);
}
static char *StrDpx(char *str)
{
	char *p = MemAlloc(strlen(str) + 1);

	strcpy(p, str);
	return p;
}
static char *AddStr(char *p, char *str)
{
	p = (char *)MemRealloc(p, strlen(p) + strlen(str) + 1);
	strcat(p, str);
	return p;
}
static char *AddNum(char *p, int num)
{
	char buff[2];

	if(10 <= num)
		p = AddNum(p, num / 10);

	buff[0] = '0' + (num % 10);
	buff[1] = '\0';

	return AddStr(p, buff);
}

static int HSz;
static int HCnt;
static char *Html;

static void AddToHtml(int ch)
{
	if(HSz <= HCnt)
	{
		HSz *= 2;
		Html = (char *)MemRealloc(Html, HSz);
	}
	Html[HCnt++] = ch;
}
static void ReadHtml(void)
{
	HSz = 16;
	HCnt = 0;
	Html = (char *)MemAlloc(HSz);

	for(; ; )
	{
		int ch = fgetc(stdin);

		if(ch == EOF)
			break;

		AddToHtml(ch);
	}
	AddToHtml('\0');
}

typedef struct Tag_st
{
	struct Tag_st *Parent;
	int Sr;
	char *Nm;
	char *Txt;
}
Tag_t;

typedef struct Row_st
{
	int Sr;
	char *Pt;
	char *Txt;
}
Row_t;

static Row_t **Rows;
static int RCnt;

static void AddRow(Row_t *row)
{
	RCnt++;
	Rows = MemRealloc(Rows, RCnt * sizeof(Row_t *));
	Rows[RCnt - 1] = row;
}
static char *GetTagPt(Tag_t *tag)
{
	if(tag->Parent)
		return AddStr(AddStr(GetTagPt(tag->Parent), "/"), tag->Nm);
	else
		return AddStr(StrDpx("/"), tag->Nm);
}
static int Unescape(char **pw, char **pr, char *ptn, int wChr)
{
	if(strncmp(*pr, ptn, strlen(ptn)))
		return 0;

	*pr += strlen(ptn) - 1;
	*(*pw)++ = wChr;
	return 1;
}
static void NormTagTxt(char *str)
{
	char *r;
	char *w = str;

	for(r = str; *r; r++)
	{
		if(
			*r != '\r' &&
			*r != '\n' &&
			!Unescape(&w, &r, "&lt;", '<') &&
			!Unescape(&w, &r, "&gt;", '>') &&
			!Unescape(&w, &r, "&amp;", '&')
			)
			*w++ = *r;
	}
	*w = '\0';
}
static Row_t *CreateRow(Tag_t *tag)
{
	Row_t *row = (Row_t *)MemAlloc(sizeof(Row_t));

	NormTagTxt(tag->Txt);

	row->Sr = tag->Sr;
	row->Pt = GetTagPt(tag);
	row->Txt = StrDpx(tag->Txt);

	return row;
}
static int CompRow(Row_t const **a, Row_t const **b)
{
	return a[0]->Sr - b[0]->Sr;
}
static void DoNumberingRows(void)
{
	int i;
	int j;
	int k;
	int ndx;

	for(i = 0; i < RCnt; i++)
	{
		char *ppt;
		char *pt;

		for(j = i + 1; j < RCnt; j++)
			if(!strcmp(Rows[i]->Pt, Rows[j]->Pt))
				break;

		if(j == RCnt)
			continue;

		ppt = StrDpx(Rows[i]->Pt);
		strrchr(ppt, '/')[1] = '\0';

		while(++j < RCnt)
			if(strncmp(ppt, Rows[j]->Pt, strlen(ppt)))
				break;

		ndx = 1;

		for(k = i + 1; k < j; k++)
		{
			char *wPt;

			if(!strcmp(Rows[i]->Pt, Rows[k]->Pt))
				ndx++;
			else if(strncmp(Rows[i]->Pt, Rows[k]->Pt, strlen(Rows[i]->Pt)) || Rows[k]->Pt[strlen(Rows[i]->Pt)] != '/')
				continue;

			wPt = AddStr(AddStr(AddNum(AddStr(StrDpx(Rows[i]->Pt), "["), ndx), "]"), Rows[k]->Pt + strlen(Rows[i]->Pt));
			free(Rows[k]->Pt);
			Rows[k]->Pt = wPt;
		}
		Rows[i]->Pt = AddStr(Rows[i]->Pt, "[1]");
		free(ppt);
	}
}
int main()
{
	Tag_t *tag = NULL;
	Tag_t *wTag;
	Row_t *row;
	char *p;
	char *q;
	char *r;
	int i;
	int sr = 0;
	int selfClosed;

	ReadHtml();
	p = Html;

	while(q = strchr(p, '<'))
	{
		if(tag)
		{
			*q = '\0';
			tag->Txt = AddStr(tag->Txt, p);
		}
		q++;
		p = strchr(q, '>');

		switch(*q)
		{
		case '!':
			break;

		default:
			if(selfClosed = p[-1] == '/')
				p[-1] = '\0';
			else
				*p = '\0';

			r = strchr(q, ' ');

			if(r)
				*r = '\0';

			wTag = MemAlloc(sizeof(Tag_t));
			wTag->Parent = tag;
			tag = wTag;
			tag->Sr = sr++;
			tag->Nm = StrDpx(q);
			tag->Txt = StrDpx("");

			if(!selfClosed)
				break;

			// fall through

		case '/':
			AddRow(CreateRow(tag));

			wTag = tag;
			tag = tag->Parent;

			free(wTag->Nm);
			free(wTag->Txt);
			free(wTag);
			break;
		}
		p++;
	}
	free(Html);

	if(!RCnt)
		Error(); // no tags ???

	qsort(Rows, RCnt, sizeof(Row_t *), CompRow);

	DoNumberingRows();

	for(i = 0; i < RCnt; i++)
	{
		row = Rows[i];

		printf("\"%s\",\"%s\"\n", row->Pt, row->Txt);

		free(row->Pt);
		free(row->Txt);
		free(row);
	}
	free(Rows);
}
