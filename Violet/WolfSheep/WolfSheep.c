#include "C:\Factory\Common\all.h"

#define W_NUM 10
#define S_NUM 10

static int Arr[W_NUM + S_NUM];

static void Found(void)
{
	uint index;

	for(index = 0; index < lengthof(Arr); index++)
		cout("%c", Arr[index]);

	cout("\n");
}
static int Check(int count)
{
	uint index;

	{
		uint wCnt = 0;
		uint sCnt = 0;

		for(index = 0; index < count; index++)
		{
			if(Arr[index] == 'W')
				wCnt++;
			else
				sCnt++;
		}

		if(W_NUM < wCnt) return 0;
		if(S_NUM < sCnt) return 0;
	}


//#define W_SPAN 2
//#define W_MAX 1
// 11

//#define W_SPAN 3
//#define W_MAX 2
// 24068

#define W_SPAN 4
#define W_MAX 2
// 196

//#define W_SPAN 5
//#define W_MAX 3
// 25101

//#define W_SPAN 6
//#define W_MAX 3
// 1471

//#define W_SPAN 7
//#define W_MAX 4
// 35147

//#define W_SPAN 8
//#define W_MAX 4
// 6266


	if(W_SPAN <= count)
	{
		uint wCnt = 0;

		for(index = count - W_SPAN; index < count; index++)
			if(Arr[index] == 'W')
				wCnt++;

		if(W_MAX < wCnt)
			return 0;
	}

	return 1;
}
static void Main2(void)
{
	uint index = 0;
	int ahead = 1;

	for(; ; )
	{
		if(ahead)
		{
			if(!Check(index))
			{
				ahead = 0;
			}
			else if(index < lengthof(Arr))
			{
				Arr[index] = 'W';
			}
			else
			{
				Found();
				ahead = 0;
			}
		}
		else
		{
			if(Arr[index] == 'W')
			{
				Arr[index] = 'S';
				ahead = 1;
			}
			else
			{
				// noop
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
}
int main(int argc, char **argv)
{
	Main2();
}
