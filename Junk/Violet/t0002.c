#include "C:\Factory\Common\all.h"

typedef struct Message_st
{
	uint Size;
	uchar Data[0];
}
Message_t;

/*
typedef struct Message2_st
{
	uint Size;
	uchar Data[0];
	int Dummy; // エラーになる。
}
Message2_t;
*/

//static uchar Zero[0]; // これはエラーになる！
static uchar Zero[1];

int main(int argc, char **argv)
{
	Message_t m;
//	Message2_t m2;

	cout("%u\n", sizeof(m));
	cout("%u\n", &m);
	cout("%u\n", &m.Size);
	cout("%u\n", &m.Data);

	cout("----\n");

	/*
	cout("%u\n", sizeof(m2));
	cout("%u\n", &m2);
	cout("%u\n", &m2.Size);
	cout("%u\n", &m2.Data);
	cout("%u\n", &m2.Dummy);

	cout("----\n");
	*/

	cout("%u\n", &Zero);
	cout("%u\n", sizeof(Zero));
}
