#include "C:\Factory\Common\all.h"

static uint Result[3][3];
static uint Sq[3];

static void Swap(uint i, uint j)
{
	uint tmp = Sq[i];

	Sq[i] = Sq[j];
	Sq[j] = tmp;
}
static void DoSort(uint i1, uint i2, uint i3)
{
	Sq[0] = 0;
	Sq[1] = 1;
	Sq[2] = 2;

	Swap(0, i1);
	Swap(1, i2);
	Swap(2, i3);

	Result[0][Sq[0]]++;
	Result[1][Sq[1]]++;
	Result[2][Sq[2]]++;
}
static void DoTest(int full)
{
	uint i1;
	uint i2;
	uint i3;

	cout("%d\n", full);

	zeroclear(Result);

	if(full)
	{
		for(i1 = 0; i1 < 3; i1++)
		for(i2 = 0; i2 < 3; i2++)
		for(i3 = 0; i3 < 3; i3++)
		{
			DoSort(i1, i2, i3);
		}
	}
	else
	{
		for(i1 = 0; i1 < 3; i1++)
		for(i2 = 1; i2 < 3; i2++)
		for(i3 = 2; i3 < 3; i3++)
		{
			DoSort(i1, i2, i3);
		}
	}
	cout("%u, %u, %u\n", Result[0][0], Result[0][1], Result[0][2]);
	cout("%u, %u, %u\n", Result[1][0], Result[1][1], Result[1][2]);
	cout("%u, %u, %u\n", Result[2][0], Result[2][1], Result[2][2]);
}
int main(int argc, char **argv)
{
	DoTest(1);
	DoTest(0);
}
