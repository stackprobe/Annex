#include "C:\Factory\Common\all.h"

#define H_MAX 10
#define W_MAX 10

#define ATTEND 'O'
#define ABSENT 'X'

static uint T[H_MAX][W_MAX];
static uint H;
static uint W;

static autoList_t *ReadToEnd(FILE *fp)
{
	autoList_t *lines = newList();

	for(; ; )
	{
		char *line = readLine(fp);

		if(!line)
			break;

		if(!*line)
		{
			memFree(line);
			break;
		}
		addElement(lines, (uint)line);
	}
	return lines;
}
static int AllIs(int chr)
{
	uint rowidx;
	uint colidx;

	for(rowidx = 0; rowidx < H; rowidx++)
	for(colidx = 0; colidx < W; colidx++)
	{
		if(T[rowidx][colidx] != chr)
			return 0;
	}
	return 1;
}

static void TryPut(uint index);

static void TryPut2(uint index, uint r1, uint c1, uint r2, uint c2)
{
	if(H <= r2) return;
	if(W <= c2) return;

	if(T[r2][c2] != ATTEND) return;

	T[r1][c1] = ABSENT;
	T[r2][c2] = ABSENT;

	TryPut(index + 1);

	T[r1][c1] = ATTEND;
	T[r2][c2] = ATTEND;
}
static void TryPut(uint startPos)
{
	uint index;

	for(index = startPos; index < W * H; index++)
	{
		uint r = index / W;
		uint c = index % W;

		if(T[r][c] == ATTEND)
		{
			TryPut2(index, r, c, r + 1, c);
			TryPut2(index, r, c, r, c + 1);

			return;
		}
	}

	cout("yes\n");
	termination(0);
}
int main(int argc, char **argv)
{
	autoList_t *lines = ReadToEnd(stdin);
	char *line;
	uint rowidx;
	uint colidx;

	errorCase(!m_isRange(getCount(lines), 1, H_MAX));

	H = getCount(lines);
	W = strlen(getLine(lines, 0));

	errorCase(!m_isRange(W, 1, W_MAX));

	foreach(lines, line, rowidx)
	{
		errorCase(strlen(line) != W);

		for(colidx = 0; colidx < W; colidx++)
		{
			int chr = line[colidx];

			errorCase(chr != ATTEND && chr != ABSENT);

			T[rowidx][colidx] = chr;
		}
	}

	if(!AllIs(ABSENT))
	{
		TryPut(0);
	}

	cout("no\n");
}
