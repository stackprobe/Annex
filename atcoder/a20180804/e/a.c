#include "C:\Factory\Common\all.h"

static int H;
static char D[100001];
static char S[1000][1001];
static char S2[1000][1001];

static int MoveUD(char *mvPtn, char *glPtn)
{
	char ptn[2];
	int r;
	int c;

	for(r = 1; r < H; r++)
	{
		char *r1 = S[r - 1];
		char *r2 = S[r];

		for(c = 0; r1[c]; c++)
		{
			ptn[0] = r1[c];
			ptn[1] = r2[c];

			if(!memcmp(ptn, mvPtn, 2))
			{
				r1[c] = '$';
				r2[c] = '$';
			}
			else if(!memcmp(ptn, glPtn, 2))
			{
				return 1;
			}
		}
	}
	for(r = 0; r < H; r++)
	{
		replaceChar(S[r], '$', 'S');
	}
	return 0;
}


// zantei >>>>
static void replacePtn(char *line, char *ptn1, char *ptn2, int ignoreCase) // mbs_
{
	char *p = line;
	uint ptnSz = strlen(ptn1);

	errorCase(ptnSz != strlen(ptn2));

	while(p = mbs_strstrCase(p, ptn1, ignoreCase))
	{
		memcpy(p, ptn2, ptnSz);
		p += ptnSz;
	}
}
// <<<< zantei


static int MoveLR(char *mvPtn, char *glPtn)
{
	int i;

	for(i = 0; i < H; i++)
	{
		if(strstr(S[i], glPtn))
			return 1;

		replacePtn(S[i], mvPtn, "SS", 0);
	}
	return 0;
}
static int MoveUDLR(int dire)
{
	switch(dire)
	{
	case 'U': return MoveUD(".S", "GS");
	case 'D': return MoveUD("S.", "SG");
	case 'L': return MoveLR(".S", "GS");
	case 'R': return MoveLR("S.", "SG");
	}
	error(); // never
	return -1; // dummy
}
static int IsSame_S_S2(void)
{
	int i;

	for(i = 0; i < H; i++)
		if(strcmp(S[i], S2[i]))
			return 0;

	return 1;
}
static void Copy_S_S2(void)
{
	int i;

	for(i = 0; i < H; i++)
		strcpy(S2[i], S[i]);
}
int main(int argc, char **argv)
{
	int dummy;
	int i;
	int ans = 1;

	scanf("%d", &H); // n
	scanf("%d", &dummy); // m
	scanf("%d", &dummy); // k

	scanf("%s", D);

	for(i = 0; i < H; i++)
		scanf("%s", S[i]);

	for(; ; )
	{
		for(i = 0; D[i]; i++, ans++)
			if(MoveUDLR(D[i]))
				goto endLoop;

		if(IsSame_S_S2())
		{
			ans = -1;
			break;
		}
		Copy_S_S2();
	}
endLoop:

	cout("%d\n", ans);
}
