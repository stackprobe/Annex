/*
	コマンド: a.exe 入力ファイル

		入力ファイルの各行に指定できる値は 1 ～ 4294967295 です。
*/

#include <stdio.h>
#include <malloc.h>
#include <ctype.h>

#define INPUT_NUM_MAX 1000

typedef unsigned char byte;
typedef unsigned int ui32;

static ui32 GetSq(ui32 val)
{
	ui32 ret = 0;
	ui32 bit;

	for(bit = 1 << 15; bit; bit >>= 1)
	{
		ui32 try = ret | bit;

		if(try * try <= val)
			ret = try;
	}
	return ret;
}

static ui32 InputTbl[INPUT_NUM_MAX];
static ui32 AnswerTbl[INPUT_NUM_MAX];
static int InputNum;
static ui32 MaxNo;

static ui32 ReadNum(FILE *fp)
{
	ui32 no = 0;

	for(; ; )
	{
		int chr = fgetc(fp);

		if(chr == '\n' || chr == EOF)
			break;

		if(isdigit(chr))
		{
			no *= 10;
			no += chr - '0';
		}
	}
	return no;
}
static void ReadInputFile(char *file)
{
	FILE *fp = fopen(file, "rt");

	if(!fp)
		return;

	do
	{
		ui32 no = ReadNum(fp);

		if(!no)
			break;

		if(MaxNo < no)
			MaxNo = no;

		InputTbl[InputNum] = no;
		InputNum++;
	}
	while(InputNum < INPUT_NUM_MAX);

	fclose(fp);
}
static void OutputAnswer(void)
{
	int index;

	for(index = 0; index < InputNum; index++)
		printf("%u\n", AnswerTbl[index]);
}

static byte *SosuTbl;

static int GetSBit(ui32 no)
{
	return SosuTbl[no / 8] & 1 << no % 8;
}
static void SetSBit(ui32 no, int bit)
{
	if(bit)
		SosuTbl[no / 8] |= 1 << no % 8;
	else
		SosuTbl[no / 8] &= ~(1 << no % 8);
}
static void SetMulSBits(ui32 no)
{
	ui32 count;

	if(MaxNo <= no * 2)
		return;

	for(count = no * 2; count < MaxNo - no; count += no)
		SetSBit(count, 1);

	if(count < MaxNo)
		SetSBit(count, 1);
}

static ui32 NextInput;
static ui32 CurrNo;
static ui32 CurrCount;

static int GetNextInput(void)
{
	ui32 lastNi = NextInput;
	int index;

	NextInput = 0;

	for(index = 0; index < InputNum; index++)
	{
		ui32 no = InputTbl[index];

		if(lastNi < no && (!NextInput || no < NextInput))
			NextInput = no;
	}
	return lastNi < NextInput;
}
static void CountSosuToNextInput(void)
{
	while(CurrNo < NextInput)
		if(!GetSBit(CurrNo++))
			CurrCount++;
}
static void SetAnswer(void)
{
	int index;

	for(index = 0; index < InputNum; index++)
		if(InputTbl[index] == CurrNo)
			AnswerTbl[index] = CurrCount;
}

static void MakeSosuTbl(void)
{
	ui32 maxNoSq = GetSq(MaxNo);
	ui32 no;

	SetSBit(0, 1);
	SetSBit(1, 1);
	SetMulSBits(2);

	for(no = 3; no <= maxNoSq; no += 2)
		if(!GetSBit(no))
			SetMulSBits(no);
}
int main(int argc, char **argv)
{
	if(argc != 2)
		return 1;

	ReadInputFile(argv[1]);

	SosuTbl = (byte *)calloc(MaxNo / 8 + 1, 1);

	if(!SosuTbl)
		return 1;

	MakeSosuTbl();

	while(GetNextInput())
	{
		CountSosuToNextInput();
		SetAnswer();
	}
	OutputAnswer();

	free(SosuTbl);
}
