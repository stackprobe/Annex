#include <stdlib.h>
#include <malloc.h>

int main(int argc, char **argv)
{
	while(malloc(1000));
//	printf("####1\n");
	system("DIR"); // 実行されない！
//	printf("####2\n");
}
