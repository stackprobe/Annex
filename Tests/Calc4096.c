#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\uintx\uint4096.h"

static void ToUInt4096(char *line, uint4096_t *dest)
{
	uint src[128];
	uint index;

	errorCase_m(!lineExp("<0,1024,09AFaf>", line), "•s³‚È‰‰Z€");

	line = strx(line);
	toLowerLine(line);

	while(strlen(line) < 1024)
		line = insertChar(line, 0, '0');

	for(index = 0; index < 128; index++)
		src[127 - index] = toValueDigits_xc(strxl(line + index * 8, 8), hexadecimal);

	ToUI4096(src, dest);
}
static char *FromUInt4096(uint4096_t *a)
{
	char *line = strx("");
	uint dest[128];
	uint index;

	UnUI4096(a, dest);

	for(index = 0; index < 128; index++)
		line = addLine_x(line, xcout("%08x", dest[127 - index]));

	while(line[0] == '0' && line[1])
		eraseChar(line);

	return line;
}
int main(int argc, char **argv)
{
	uint4096_t a;
	int operator;
	uint4096_t b;
	uint4096_t ans;
	uint4096_t dummy;
	char *str;

	ToUInt4096(nextArg(), &a);
	operator = nextArg()[0];
	ToUInt4096(nextArg(), &b);

	LOGPOS();

	switch(operator)
	{
	case '+':
		UI4096_Add(&a, &b, &ans);
		break;

	case '-':
		UI4096_Sub(&a, &b, &ans);
		break;

	case '*':
		UI4096_Mul(&a, &b, &ans, &dummy);
		break;

	case '/':
		UI4096_Div(&a, &b, &ans);
		break;

	default:
		error_m("•s–¾‚È‰‰Zq");
	}

	LOGPOS();

	str = FromUInt4096(&ans);
	cout("%s\n", str);
	memFree(str);
}
