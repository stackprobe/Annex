#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *list = newList();
	uint count;

	for(count = 0; count < UINTMAX; count++)
	{
		if(count % 1000000 == 0)
			cout("%u\n", count);

		addElement(list, count);
	}
}
