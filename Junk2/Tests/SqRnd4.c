#include <stdio.h>

static int Sq[3];
static int Result[3][3]; // [X][Y] ... X ”Ô–Ú‚ª Y ‚É‚È‚Á‚½‰ñ”

static void Swap(int i, int j)
{
	int tmp = Sq[i];

	Sq[i] = Sq[j];
	Sq[j] = tmp;
}
static void DoTest(int a, int b, int c)
{
	Sq[0] = 0;
	Sq[1] = 1;
	Sq[2] = 2;

	Swap(0, a);
	Swap(1, b);
	Swap(2, c);

	printf("(%d, %d, %d) ==> [%d, %d, %d]\n", a, b, c, Sq[0], Sq[1], Sq[2]);

	Result[0][Sq[0]]++;
	Result[1][Sq[1]]++;
	Result[2][Sq[2]]++;
}
int main(int argc, char **argv)
{
	int a;
	int b;
	int c;

	//*
	for(a = 0; a < 3; a++)
	for(b = 0; b < 3; b++)
	for(c = 0; c < 3; c++)
	/*/
	for(a = 0; a < 3; a++)
	for(b = 1; b < 3; b++)
	for(c = 2; c < 3; c++)
	//*/
	{
		DoTest(a, b, c);
	}

	printf("Sq[0]=0 ... %d\n", Result[0][0]);
	printf("Sq[0]=1 ... %d\n", Result[0][1]);
	printf("Sq[0]=2 ... %d\n", Result[0][2]);

	printf("Sq[1]=0 ... %d\n", Result[1][0]);
	printf("Sq[1]=1 ... %d\n", Result[1][1]);
	printf("Sq[1]=2 ... %d\n", Result[1][2]);

	printf("Sq[2]=0 ... %d\n", Result[2][0]);
	printf("Sq[2]=1 ... %d\n", Result[2][1]);
	printf("Sq[2]=2 ... %d\n", Result[2][2]);
}
