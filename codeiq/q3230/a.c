#include <stdio.h>

#define W 5
#define H 32
#define Y_LOOP (H * 2)

int main()
{
	int dir = 2;
	int x = 0;
	int y = 0;
	int c;

	while((c = fgetc(stdin)) != EOF)
	{
		switch(c)
		{
		case 'R':
			switch(dir)
			{
			case 2: dir = 4; break;
			case 4: dir = 8; break;
			case 8: dir = 6; break;
			case 6: dir = 2; break;
			}
			break;

		case 'L':
			switch(dir)
			{
			case 2: dir = 6; break;
			case 4: dir = 2; break;
			case 8: dir = 4; break;
			case 6: dir = 8; break;
			}
			break;

		case 'B':
			if(dir == 4 || dir == 6)
				dir = 10 - dir;

			x = (W - 1) - x;
			y += H;
			y %= Y_LOOP;
			break;

		default:
			if('0' <= c && c <= '9')
			{
				c -= '0';

				switch(dir)
				{
				case 2: y += c; break;
				case 4: x -= c; break;
				case 6: x += c; break;
				case 8: y -= c; break;
				}
				while(x < 0 || W <= x)
				{
					y += H;

					if(x < 0)
						x += W;
					else
						x -= W;
				}
				y += Y_LOOP;
				y %= Y_LOOP;
			}
			break;
		}
	}
	printf("%d%c", y % H + 1, "aA"[y / H] + x);
}

/*

y—áz
“ü—Í	o—Í
L9R9B9	19a
RRRB1	1D

*/
