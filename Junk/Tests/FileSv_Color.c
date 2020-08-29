#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"
#include "C:\Factory\Common\Options\crc.h"

static char *B_LinkColor;
static char *B_BackColor;
static char *B_TextColor;

static char *GetRandColor(uint hexlow, uint hexhi)
{
	char *str = strx("#999999");
	char *p;

	for(p = str + 1; *p; p++)
		*p = hexadecimal[mt19937_range(hexlow, hexhi)];

	return str;
}
int main(int argc, char **argv)
{
	char *computerName = getEnvLine("COMPUTERNAME");

	cout("%s\n", computerName);

	computerName = nextArg();

	mt19937_initRnd(crc32CheckLine(computerName));

	B_LinkColor = GetRandColor(12, 15);
	B_BackColor = GetRandColor(6, 9);
	B_TextColor = GetRandColor(0, 3);

	cout("%s\n", B_LinkColor);
	cout("%s\n", B_BackColor);
	cout("%s\n", B_TextColor);
}
