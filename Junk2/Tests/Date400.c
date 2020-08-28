#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Date2Day.h"

int main(int argc, char **argv)
{
	uint y;

	for(y = 1; y <= 400; y++)
	{
		cout("%04u/01/01=%I64u\n", y, Date2Day(y, 1, 1));
		cout("%04u/03/01=%I64u\n", y, Date2Day(y, 3, 1));
		cout("%04u/12/31=%I64u\n", y, Date2Day(y, 12, 31));
	}
}
