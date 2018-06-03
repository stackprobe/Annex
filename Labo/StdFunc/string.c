#include "string.h"

char *my_strchr(const char *s, int c)
{
	for(; *s != c; s++)
		if(!*s)
			return NULL;

	return (char *)s;
}
