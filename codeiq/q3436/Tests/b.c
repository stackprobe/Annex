#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	uint m;
	uint n;

	for(m = 1; m <= 9; m++)
	for(n = 1; n <= 9; n++)
	{
		coExecute_x(xcout("a.exe %u %u", m, n));
	}
}
