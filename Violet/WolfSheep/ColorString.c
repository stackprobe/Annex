/*
	赤色に塗られた文字を左から右に読んだ文字列、青色に塗られた文字を右から左に読んだ文字列が一致する。
*/

#include "C:\Factory\Common\all.h"

static char *Ptn;
static uint PtnLen;
static char *Colors; // "-BR"
static uint64 Ans;

static void Found(void)
{
#if 0
	uint index;

	for(index = 0; index < PtnLen; index++)
		cout("%c", Colors[index]);

	cout("\n");
#endif

	Ans++;
}
static int Check(void)
{
	{
		uint n = PtnLen / 2;
		uint bCnt = 0;
		uint rCnt = 0;
		uint index;

		for(index = 0; index < PtnLen; index++)
		{
			switch(Colors[index])
			{
			case 'B':
				bCnt++;
				if(n < bCnt) return 0;
				break;

			case 'R':
				rCnt++;
				if(n < rCnt) return 0;
				break;

			default:
				break;
			}
		}
	}

	{
		sint l = -1;
		sint r = PtnLen;

		for(; ; )
		{
			while(++l < PtnLen && Colors[l] == 'B'); // find "-R"
			while(0 <= --r     && Colors[r] == 'R'); // find "-B"

			if(l < PtnLen)
			{
				errorCase(r < 0);

				if(Colors[l] == '-' || Colors[r] == '-')
					break;

				if(Ptn[l] != Ptn[r])
					return 0;
			}
			else
			{
				errorCase(0 <= r);
				break;
			}
		}
	}

	return 1;
}
static uint GetVIndex(uint index)
{
	if(index & 1)
		return PtnLen - 1 - index / 2;
	else
		return index / 2;
}
static void Test01(char *str)
{
	uint index = 0;
	int ahead = 1;

	Ptn = str;
	PtnLen = strlen(str);
	Colors = repeatChar('-', PtnLen);
	Ans = 0;

	for(; ; )
	{
		if(ahead)
		{
			if(!Check())
			{
				ahead = 0;
			}
			else if(index < PtnLen)
			{
				uint vIdx = GetVIndex(index);

				Colors[vIdx] = 'B';
			}
			else
			{
				Found();
				ahead = 0;
			}
		}
		else
		{
			uint vIdx = GetVIndex(index);

			if(Colors[vIdx] == 'B')
			{
				Colors[vIdx] = 'R';
				ahead = 1;
			}
			else
			{
				Colors[vIdx] = '-';
			}
		}

		if(ahead)
		{
			index++;
		}
		else
		{
			if(!index)
				break;

			index--;
		}
	}

	memFree(Colors);

	cout("%I64u\n", Ans);
}
int main(int argc, char **argv)
{
	Test01("cabaacba"); // 4
	Test01("mippiisssisssiipsspiim"); // 504
	Test01("abcdefgh"); // 0
//	Test01("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"); // 9075135300
}
