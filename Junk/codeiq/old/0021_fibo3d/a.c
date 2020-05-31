#include <stdio.h>
#include <stdlib.h>

#define LINELENMAX 1000

static char *ReadLine(FILE *fp)
{
	static char buff[LINELENMAX + 1];
	int c;

	for(c = 0; c < LINELENMAX; c++)
	{
		int chr = fgetc(fp);

		if(chr == EOF || chr == '\n')
			break;

		buff[c] = chr;
	}
	buff[c] = '\0';
	return buff;
}

#define FIBO_CYC 1500

static int Fibo[FIBO_CYC];

static void MakeFibo(void)
{
	int i;

	Fibo[0] = 0;
	Fibo[1] = 1;

	for(i = 2; i < FIBO_CYC; i++)
	{
		Fibo[i] = (Fibo[i - 2] + Fibo[i - 1]) % 1000;
	}
}
static int GetFibo(int index)
{
	if(index < 0)
		return -1;

	return Fibo[index % FIBO_CYC];
}

int main(int argc, char **argv)
{
	if(2 <= argc)
	{
		FILE *fp = fopen(argv[1], "rt");

		if(fp)
		{
			int num = atoi(ReadLine(fp));
			int c;

			MakeFibo();

			for(c = 0; c < num; c++)
			{
				int index = atoi(ReadLine(fp));
				int fibo;

				fibo = GetFibo(index);

				printf("%d\n", fibo);
			}
			fclose(fp);
		}
	}
}
