#include "C:\Factory\Common\all.h"

#define SRC "aececb:fcd424:0775c4:f70f1f:b51d66:00a752:00b1bb:a1ca62:f29047:fa98bf:7e51a6:464b4f"

int main(int argc, char **argv)
{
	autoList_t *colors = tokenize(SRC, ':');
	char *color;
	uint index;

	cout("%s\n", SRC);

	foreach(colors, color, index)
	{
		uint value = toValueDigits(color, hexadecimal);
		uint r;
		uint g;
		uint b;

		r = value >> 16 & 0xff;
		g = value >>  8 & 0xff;
		b = value >>  0 & 0xff;

		if(r + g + b < 0x80 * 3)
		{
			r += 0xff;
			g += 0xff;
			b += 0xff;

			r /= 2;
			g /= 2;
			b /= 2;

			while(r < 0xff && g < 0xff && b < 0xff)
			{
				r++;
				g++;
				b++;
			}
		}
		else
		{
			r /= 2;
			g /= 2;
			b /= 2;

			while(r && g && b)
			{
				r--;
				g--;
				b--;
			}
		}
		if(index)
			cout(":");

		cout("%02x%02x%02x", r, g, b);
	}
	cout("\n");
}
