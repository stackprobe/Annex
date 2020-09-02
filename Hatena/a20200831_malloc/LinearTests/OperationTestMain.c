#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"
#include "MYLIB_malloc.h"

#define TEST_COUNT 3000000

static uint AllocNumMax;
static uint AllocSizeMax;

typedef struct PtrInfo_st
{
	void *Ptr; // NULL == 未確保
	uint Size;
	int StartChar; // 最初のバイト値
	int Char;      // 最初と最後以外のバイト値
	int EndChar;   // 最後のバイト値
}
PtrInfo_t;

static PtrInfo_t *PtrInfos;

static void SetPtr(PtrInfo_t *i, void *ptr, uint size)
{
	uint index;

	errorCase(!ptr);

	i->Ptr       = ptr;
	i->Size      = size;
	i->StartChar = mt19937_rnd(256);
	i->Char      = mt19937_rnd(256);
	i->EndChar   = mt19937_rnd(256);

	for(index = 0; index < size; index++)
	{
		int chr;

		if(!index)
			chr = i->StartChar;
		else if(index + 1 < size)
			chr = i->Char;
		else
			chr = i->EndChar;

		((uchar *)ptr)[index] = chr;
	}
}
static void CheckPtr(PtrInfo_t *i)
{
	uint index;

	for(index = 0; index < i->Size; index++)
	{
		int chr;

		if(!index)
			chr = i->StartChar;
		else if(index + 1 < i->Size)
			chr = i->Char;
		else
			chr = i->EndChar;

		errorCase(((uchar *)i->Ptr)[index] != chr);
	}
}
int main(int argc, char **argv)
{
	uint count;

	mt19937_initCRnd();

	AllocNumMax  = toValue(nextArg());
	AllocSizeMax = toValue(nextArg());

	errorCase(!m_isRange(AllocNumMax,  1, IMAX / sizeof(PtrInfo_t)));
	errorCase(!m_isRange(AllocSizeMax, 1, IMAX));

	PtrInfos = (PtrInfo_t *)memCalloc(AllocNumMax * sizeof(PtrInfo_t));

	for(count = 0; count < TEST_COUNT; count++)
	{
		uint index = mt19937_rnd(AllocNumMax);
		uint size;
		void *ptr;

		if(!PtrInfos[index].Ptr)
		{
			size = mt19937_rnd(AllocSizeMax) + 1;
			ptr = MYLIB_malloc(size);

			SetPtr(PtrInfos + index, ptr, size);
		}
		else if(mt19937_rnd(2))
		{
			CheckPtr(PtrInfos + index);

			ptr = PtrInfos[index].Ptr;
			size = mt19937_rnd(AllocSizeMax) + 1;
			ptr = MYLIB_realloc(ptr, size);

			SetPtr(PtrInfos + index, ptr, size);
		}
		else
		{
			CheckPtr(PtrInfos + index);

			MYLIB_free(PtrInfos[index].Ptr);
			PtrInfos[index].Ptr = NULL;
		}
	}
	memFree(PtrInfos);

	cout("OK!\n");
}
