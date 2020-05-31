#include <stdio.h>
#include <string.h>

static void OdrS(char *s)
{
	if(s[1] < s[0])
	{
		int tmp = s[0];
		s[0] = s[1];
		s[1] = tmp;
	}
}
static int IsNoopL(char *s, char *p)
{
	return strchr(p, s[0]) && strchr(p, s[1]);
}
static int IsNoop(char *s)
{
	return
		IsNoopL(s, "abcd") ||
		IsNoopL(s, "defg") ||
		IsNoopL(s, "ghij") ||
		IsNoopL(s, "jkla");
}
static int IsCorner(int c)
{
	return !!strchr("adgj", c);
}
static int CountCorner2(int c1, int c2)
{
	int ret = 0;
	int c;

	for(c = c1; c <= c2; c++)
		if(IsCorner(c))
			ret++;

	return ret;
}
static int CountCorner(int c1, int c2)
{
	if(c2 < c1)
		return CountCorner2('a', c2 - 1) + CountCorner2(c1 + 1, 'j');
	else
		return CountCorner2(c1 + 1, c2 - 1);
}
static int GetMin(int v1, int v2)
{
	return v1 < v2 ? v1 : v2;
}
static int GetMax(int v1, int v2)
{
	return v1 < v2 ? v2 : v1;
}
static void OdrI(int *a, int *b)
{
	if(*b < *a)
	{
		int tmp = *a;
		*a = *b;
		*b = tmp;
	}
}
static void OdrI3(int *a, int *b, int *c)
{
	OdrI(a, b);
	OdrI(b, c);
	OdrI(a, b);
}
static void OdrI4(int *a, int *b, int *c, int *d)
{
	OdrI3(a, b, c);
	OdrI(c, d);
	OdrI(b, c);
	OdrI(a, b);
}
static int IsCrossing(char *s1, char *s2)
{
	if(s2[0] < s1[0])
	{
		char *tmp = s1;
		s1 = s2;
		s2 = tmp;
	}
	return
		s1[0] < s2[0] &&
		s2[0] < s1[1] &&
		s1[1] < s2[1];
}
int main()
{
	char s1[3];
	char s2[3];

	scanf("%s", s1);
	scanf("%s", s2);

	OdrS(s1);
	OdrS(s2);

	if(IsNoop(s1))
		s1[0] = 0;

	if(IsNoop(s2))
		s2[0] = 0;

	if(!*s1)
		strcpy(s1, s2);

	if(!strcmp(s1, s2))
		s2[0] = 0;

	if(!*s1)
	{
		printf("4\n");
	}
	else if(!*s2)
	{
		int p1 = 2 + CountCorner(s1[0], s1[1]);
		int p2 = 2 + CountCorner(s1[1], s1[0]);

		OdrI(&p1, &p2);

		printf("%d,%d\n", p1, p2);
	}
	else if(IsCrossing(s1, s2)) /* cross */
	{
		int c1 = GetMin(s1[0], s2[0]);
		int c2 = GetMax(s1[0], s2[0]);
		int c3 = GetMin(s1[1], s2[1]);
		int c4 = GetMax(s1[1], s2[1]);
		int p1;
		int p2;
		int p3;
		int p4;

		p1 = 3 + CountCorner(c1, c2);
		p2 = 3 + CountCorner(c2, c3);
		p3 = 3 + CountCorner(c3, c4);
		p4 = 3 + CountCorner(c4, c1);

		OdrI4(&p1, &p2, &p3, &p4);

		printf("%d,%d,%d,%d\n", p1, p2, p3, p4);
	}
	else if(strchr(s1, s2[0]) || strchr(s1, s2[1])) /* v-cut */
	{
		int cv;
		int c1;
		int c2;
		int p1;
		int p2;
		int p3;

		if(strchr(s1, s2[0]))
		{
			cv = s2[0];
			c1 = s2[1];
		}
		else
		{
			cv = s2[1];
			c1 = s2[0];
		}
		if(cv == s1[0])
			c2 = s1[1];
		else
			c2 = s1[0];

		OdrI(&c1, &c2);

		if(c1 < cv && cv < c2)
			OdrI(&c1, &c2);

		p1 = 2 + CountCorner(cv, c1);
		p2 = 3 + CountCorner(c1, c2);
		p3 = 2 + CountCorner(c2, cv);

		OdrI3(&p1, &p2, &p3);

		printf("%d,%d,%d\n", p1, p2, p3);
	}
	else
	{
		int c1;
		int c2;
		int c3;
		int c4;
		int p1;
		int p2;
		int p3;

		if(s1[0] < s2[0])
		{
			c1 = s1[0];
			c2 = s1[1];
			c3 = s2[0];
			c4 = s2[1];
		}
		else
		{
			c1 = s2[0];
			c2 = s2[1];
			c3 = s1[0];
			c4 = s1[1];
		}

		p1 = 2 + CountCorner(c2, c1);
		p2 = 4 + CountCorner(c1, c3) + CountCorner(c4, c2);
		p3 = 2 + CountCorner(c3, c4);

		OdrI3(&p1, &p2, &p3);

		printf("%d,%d,%d\n", p1, p2, p3);
	}
	return 0;
}
