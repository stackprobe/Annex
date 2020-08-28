#include "C:\Factory\Common\all.h"
#include "string.h"

int main(int argc, char **argv)
{
	{
		char *str = "ABCDE";

		cout("%08x\n", str);
		cout("%08x\n", my_strchr(str, 'A'));
		cout("%08x\n", my_strchr(str, 'C'));
		cout("%08x\n", my_strchr(str, 'E'));
		cout("%08x\n", my_strchr(str, '\0'));
		cout("%08x\n", my_strchr(str, 'X'));
	}
}
