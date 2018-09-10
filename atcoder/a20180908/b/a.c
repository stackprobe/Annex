#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoList_t *ws = newList();
	char *w;
	int n;
	int i;

	scanf("%d", &n);

	while(n--)
	{
		w = (char *)memAlloc(11);

		scanf("%s", w);

		addElement(ws, (uint)w);
	}

	foreach(ws, w, i)
		if(i && strchr(getLine(ws, i - 1), '\0')[-1] != getLine(ws, i)[0])
			goto ng;

	rapidSortLines(ws);

	foreach(ws, w, i)
		if(i && !strcmp(getLine(ws, i - 1), getLine(ws, i)))
			goto ng;

	cout("Yes\n");
	return;
ng:
	cout("No\n");

	// g
}
