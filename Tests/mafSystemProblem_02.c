#include <stdlib.h>
#include <malloc.h>

int main(int argc, char **argv)
{
	void **p = NULL, **n;
	while(n = malloc(1000)) *n = p, p = n;
	while(p) n = *p, free(p), p = n;
//	printf("####1\n");
	system("DIR");
//	printf("####2\n");
}
