#include "C:\Factory\Common\all.h"

#define SRC "aececb:fcd424:0775c4:f70f1f:b51d66:00a752:00b1bb:a1ca62:f29047:fa98bf:7e51a6:464b4f"

static uint GetHL(int chr)
{
	return m_c2i(chr) < 8 ? 1 : 2;
}
static void Plus(char *p, sint val)
{
	val += (sint)m_c2i(*p);

	*p = m_i2c(val);
}
static void Han(char *p)
{
	sint val = (sint)m_c2i(*p);

	val ^= 0x0f;

	*p = m_i2c(val);
}
int main(int argc, char **argv)
{
	autoList_t *colors = tokenize(SRC, ':');
	char *color;
	uint index;

	cout("%s\n", SRC);

	foreach(colors, color, index)
	{
		uint hl = GetHL(color[0]) | GetHL(color[2]) | GetHL(color[4]);

		if(hl == 1) // low
		{
			Plus(color + 0, 8);
			Plus(color + 2, 8);
			Plus(color + 4, 8);
		}
		else if(hl == 2) // hi
		{
			Plus(color + 0, -8);
			Plus(color + 2, -8);
			Plus(color + 4, -8);
		}
		else if(hl == 3) // low & hi
		{
			Han(color + 0);
			Han(color + 1);
			Han(color + 2);
			Han(color + 3);
			Han(color + 4);
			Han(color + 5);
		}
		else
			error();

		if(index)
			cout(":");

		cout("%s", color);
	}
	cout("\n");
}
