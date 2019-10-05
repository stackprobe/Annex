#include "all.h"

static char *NextNotZero(char *p)
{
	while(*p == '0')
		p++;

	return p;
}
UI4096_t AToUI4096(char *str)
{
	UI4096_t ret = UI4096_0();
	uint i;
LOGPOS();

	errorCase(!lineExp("<00><1,1234,09>", str)); // Keisan 2 P 4096 - 1 L 10 == 1233.* ==> 1234 Œ…

	str = xcout("%01242s", NextNotZero(str)); // 1242 == 138 * 9

	for(i = 0; i < 138; i++)
	{
		UI4096_t a = UI4096_x(toValue_x(strxl(str + 9 * i, 9)));
		UI4096_t tmp[2];

		ret = UI4096_Mul(ret, UI4096_N10P9, tmp);
		errorCase(!UI4096_IsZero(tmp[1]));
		ret = UI4096_Add(ret, a, tmp);
		errorCase(!UI4096_IsZero(tmp[1]));
	}
	memFree(str);
LOGPOS();
	return ret;
}
char *c_UI4096ToA(UI4096_t val)
{
	static char *buff;
	char *ret;
	uint i;
LOGPOS();

	if(!buff)
	{
		buff = (char *)memAlloc(1242 + 1);
		buff[1242] = '\0';
	}
	for(i = 0; i < 138; i++)
	{
		UI4096_t tmp[2];
		char s9[10];

		val = UI4096_Div(val, UI4096_N10P9, tmp);
		errorCase(sprintf(s9, "%09u", UI4096_y(tmp[1])) != 9);
		memcpy(buff + (1242 - 9) - 9 * i, s9, 9);
	}
	ret = NextNotZero(buff);

	if(!*ret)
		ret--;

LOGPOS();
	return ret;
}
