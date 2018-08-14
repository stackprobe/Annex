#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

static sint Neg2ToInt(char *s)
{
	sint ans = 0;
	sint b = 1;

	reverseLine(s);

	while(*s)
	{
		if(*s == '1')
		{
			ans += b;
		}
		else
		{
			errorCase(*s != '0');
		}
		s++;
		b *= -2;
	}
	return ans;
}
static void Test01(sint c)
{
	char *s;
	sint t;

	cout("< %d\n", c);

	writeOneLine_cx("1.tmp", xcout("%d", c));
	coExecute_x(xcout("< 1.tmp > 2.tmp ..\\a.exe", c));
	s = readFirstLine("2.tmp");
	removeFile("1.tmp");
	removeFile("2.tmp");

	cout("> %s\n", s);

	t = Neg2ToInt(s);
	memFree(s);

	cout("t %d\n", t);

	errorCase(c != t);

	cout("OK!\n");
}
int main(int argc, char **argv)
{
	sint c;

	mt19937_initCRnd();

	for(c = -100; c <= 100; c++)
	{
		Test01(c);
	}

//	Test01(-1);
//	Test01(-10);
//	Test01(-100);
	Test01(-1000);
	Test01(-10000);
	Test01(-100000);
	Test01(-1000000);
	Test01(-10000000);
	Test01(-100000000);
//	Test01(-1000000000);

//	Test01(1);
//	Test01(10);
//	Test01(100);
	Test01(1000);
	Test01(10000);
	Test01(100000);
	Test01(1000000);
	Test01(10000000);
	Test01(100000000);
//	Test01(1000000000);

	for(c = -100; c <= 100; c++)
	{
		Test01( (1000000000 + c));
		Test01(-(1000000000 + c));
	}
	while(!waitKey(0))
	{
		Test01((sint)mt19937_range(0, 2000000000) - 1000000000);
	}
}
