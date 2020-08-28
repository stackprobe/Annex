#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	char *text = readText_b(nextArg());

	replaceChar(text, ' ', '\n');

	writeOneLineNoRet_b_cx(nextArg(), text);
}
