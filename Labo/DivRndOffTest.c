#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int numer;
	int denom;

	for(numer = -1000; numer <= 1000; numer++)
	for(denom = -1000; denom <= 1000; denom++)
	if(denom)
	{
		int a = divRndOff(numer, denom);
		int b;
		double db = (double)numer / denom;

		if(db < 0.0)
			db -= 0.5;
		else
			db += 0.5;

		b = (int)db;

//		cout("%d / %d = %d, %d\n", numer, denom, a, b);
		errorCase(a != b);
	}
	cout("OK!\n");
}
