#include <stdio.h>
#include <string.h>
#include <ctype.h>

typedef unsigned int ui32;
typedef unsigned __int64 ui64;

#define RADIX 10
#define F_NUM 4

void RevRng(char *p, char *q)
{
	while(p < q)
	{
		int tmp = *p;

		*p++ = *q;
		*q-- = tmp;
	}
}

void AddInt(ui32 *num, int i, ui64 val)
{
	for(; i < F_NUM && val; i++)
	{
		val += num[i];
		num[i] = (ui32)val;
		val >>= 32;
	}
}
void MulInt(ui32 *num, ui32 mulnum)
{
	int i;

	for(i = F_NUM - 1; 0 <= i; i--)
		AddInt(num, i, (ui64)num[i] * (mulnum - 1));
}
ui32 DivInt(ui32 *num, ui32 divnum)
{
	ui64 val = 0;
	int i;

	for(i = F_NUM - 1; 0 <= i; i--)
	{
		val <<= 32;
		val += num[i];
		num[i] = (ui32)(val / divnum);
		val %= divnum;
	}
	return (ui32)val;
}
int IsZero(ui32 *num)
{
	int i;

	for(i = 0; i < F_NUM; i++)
		if(num[i])
			return 0;

	return 1;
}
void Inv(ui32 *num)
{
	int i;

	for(i = 0; i < F_NUM; i++)
		num[i] = ~num[i];

	AddInt(num, 0, 1);
}

void FromString(ui32 *num, const char *p)
{
	int neg = 0;

	memset(num, 0x00, F_NUM * sizeof(ui32));

	if(*p == '-')
	{
		neg = 1;
		p++;
	}
	for(; *p; p++)
	{
		if(isdigit(*p))
		{
			MulInt(num, RADIX);
			AddInt(num, 0, *p - '0');
		}
	}
	if(neg)
		Inv(num);
}
void ToString(ui32 *num, char *str)
{
	int neg = 0;
	int i;
	char *p = str;

	if(num[F_NUM - 1] & 1 << 31)
	{
		neg = 1;
		Inv(num);
	}
	while(!IsZero(num))
		*p++ = DivInt(num, RADIX) + '0';

	if(neg)
		*p++ = '-';
	else if(str == p)
		*p++ = '0';

	*p = '\0';
	RevRng(str, p - 1);
}

void AddNum(ui32 *anum, ui32 *lnum, ui32 *rnum)
{
	int i;

	memset(anum, 0x00, F_NUM * sizeof(ui32));

	for(i = 0; i < F_NUM; i++)
		AddInt(anum, i, (ui64)lnum[i] + rnum[i]);
}

/*
 * 出力:
 *        result : 計算結果を文字列として入れてください。
 *                 ※符号・文字終端領域を含めて32バイト確保します。
 *
 * 入力:
 *        left   : 加算を行う左辺値文字列です。
 *        right  : 加算を行う右辺値文字列です。
 *
 */
void Add(char *result, const char *left, const char *right)
{
	ui32 lnum[F_NUM];
	ui32 rnum[F_NUM];
	ui32 anum[F_NUM];

	FromString(lnum, left);
	FromString(rnum, right);

	AddNum(anum, lnum, rnum);

	ToString(anum, result);
}

/*
 * test
 *
 */
int main(int argc, char **argv)
{
	char result[F_NUM * 10 + 2];

	if(argc != 3)
		return 1;

	Add(result, argv[1], argv[2]);

	printf("%s\n", result);
}
