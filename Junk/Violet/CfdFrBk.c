#include "C:\Factory\Common\all.h"

#define SRC "aececb:fcd424:0775c4:f70f1f:b51d66:00a752:00b1bb:a1ca62:f29047:fa98bf:7e51a6:464b4f"

int main(int argc, char **argv)
{
	int hi;

	for(hi = 0; hi < 2; hi++)
	{
		autoList_t *colors = tokenize(SRC, ':');
		char *color;
		uint index;

		foreach(colors, color, index)
		{
			uint rgb = toValueDigits(color, hexadecimal);
			uint r;
			uint g;
			uint b;

			r = rgb >> 16 & 0xff;
			g = rgb >>  8 & 0xff;
			b = rgb >>  0 & 0xff;

			if(hi)
			{
				r += 0xff;
				g += 0xff;
				b += 0xff;
			}
			r /= 2;
			g /= 2;
			b /= 2;

			if(index)
				cout(":");

			cout("%02x%02x%02x", r, g, b);
		}
		cout("\n");

		releaseDim(colors, 1);
	}
}
