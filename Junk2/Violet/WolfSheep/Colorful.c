#include "C:\Factory\Common\all.h"

//#define A_LEN 5
//#define RADIX 2
// 4

//#define A_LEN 10
//#define RADIX 2
// 0

#define A_LEN 10
#define RADIX 3
// 216

//#define A_LEN 10
//#define RADIX 4
// 95616

//#define A_LEN 17
//#define RADIX 4
// 331776

static int Arr[A_LEN];

static void Found(void)
{
	uint index;

	for(index = 0; index < lengthof(Arr); index++)
		cout("%c", '0' + Arr[index]);

	cout("\n");
}
static int Check(int count)
{
	if(3 <= count)
	{
		uint curr = count - 2;
		uint index;

		for(index = 0; index < curr; index++)
		{
			if(
				Arr[index + 0] == Arr[curr + 0] &&
				Arr[index + 1] == Arr[curr + 1]
				)
				return 0;
		}
	}
	return 1;
}
static void Main2(void)
{
	int forward = 1;
	uint index = 0;

	for(; ; )
	{
		if(forward)
		{
			if(!Check(index))
			{
				forward = 0;
			}
			else if(index < lengthof(Arr))
			{
				Arr[index] = 0;
			}
			else
			{
				Found();
				forward = 0;
			}
		}
		else
		{
			if(Arr[index] < RADIX - 1)
			{
				Arr[index]++;
				forward = 1;
			}
			else
			{
				// noop
			}
		}

		if(forward)
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
