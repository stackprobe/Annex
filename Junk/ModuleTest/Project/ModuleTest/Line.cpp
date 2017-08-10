#include "all.h"

char *strx(char *line)
{
	return strcpy((char *)memAlloc(strlen(line) + 1), line);
}
void strz(char *&buffer, char *line)
{
	memFree(buffer);
	buffer = strx(line);
}

char *getConstNullString(void)
{
	return "";
}
char *getNullString(void)
{
	return strx("");
}

void replaceChar(char *line, int chr1, int chr2)
{
	for(char *p = line; *p; p = m_mbcnext(p))
		if(*p == chr1)
			*p = chr2;
}
char *replaceLine(char *line, char *ptn1, char *ptn2) // ret: strr(line)
{
	errorCase(!line);
	errorCase(!ptn1 || !*ptn1);
	errorCase(!ptn2);

	for(char *p = line; *p; )
	{
		if(!strncmp(p, ptn1, strlen(ptn1)))
		{
			autoList<char> *buffer = new autoList<char>(line, strlen(line));
			int repPos = (uint)p - (uint)line;

			buffer->RemoveElements(repPos, strlen(ptn1));
			buffer->InsertElements(repPos, ptn2, strlen(ptn2));
			buffer->AddElement('\0');

			line = buffer->UnbindBuffer();
			p = line + repPos + strlen(ptn2);

			delete buffer;
		}
		else
			p = m_mbcnext(p);
	}
	return line;
}

autoList<char *> *tokenize(char *line, char *delimiters)
{
	autoList<char *> *tokens = new autoList<char *>();
	autoList<char> *token = new autoList<char>();

	for(char *p = line; *p; p++)
	{
		char *d;

		for(d = delimiters; *d; d++)
			if(*d == *p)
				break;

		if(*d)
		{
			token->AddElement('\0');
			tokens->AddElement(token->UnbindBuffer());
		}
		else
			token->AddElement(*p);
	}
	token->AddElement('\0');
	tokens->AddElement(token->UnbindBuffer());

	delete token;
	return tokens;
}
char *untokenize(autoList<char *> *tokens, char *separator)
{
	autoList<char> *buffer = new autoList<char>();

	for(int index = 0; index < tokens->GetCount(); index++)
	{
		char *token = tokens->GetElement(index);

		if(index)
			buffer->AddElements(separator, strlen(separator));

		buffer->AddElements(token, strlen(token));
	}
	buffer->AddElement('\0');
	char *line = buffer->UnbindBuffer();
	delete buffer;
	return line;
}
