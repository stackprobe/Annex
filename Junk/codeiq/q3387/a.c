#include <stdio.h>
#include <string.h>

static size_t GetIndex(char *str, int chr)
{
	int index;

	for(index = 0; ; index++)
		if(str[index] == chr)
			break;

	return index;
}
static void TIdxToXY(int index, int *px, int *py)
{
	*px = index % 5;
	*py = index / 5;
}
static int XYToTIdx(int x, int y)
{
	return x + y * 5;
}
static void CenterToRotPos(int *px, int *py, int rot)
{
	int x = *px;
	int y = *py;

	switch(rot % 8)
	{
	case 0: x--; y--; break;
	case 1:      y--; break;
	case 2: x++; y--; break;
	case 3: x++;      break;
	case 4: x++; y++; break;
	case 5:      y++; break;
	case 6: x--; y++; break;
	case 7: x--;      break;
	}

	*px = x;
	*py = y;
}
int main()
{
	char table[20];
	char operations[11];
	char *op;

	scanf("%s %s", table, operations);

	for(op = operations; *op; op++)
	{
		int carry = '\0';
		int rot;
		int x;
		int y;

		TIdxToXY(GetIndex(table, *op), &x, &y);

		for(rot = 0; rot < 8 || carry; rot++)
		{
			int rx = x;
			int ry = y;

			CenterToRotPos(&rx, &ry, rot);

			if(
				0 <= rx && rx < 4 &&
				0 <= ry && ry < 4
				)
			{
				int ri = XYToTIdx(rx, ry);

				if(table[ri] != '-')
				{
					int next = table[ri];

					table[ri] = carry;
					carry = next;
				}
			}
		}
	}
	printf("%s\n", table);
}
