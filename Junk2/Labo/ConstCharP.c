#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	char dummy[] = "ABCDEF";
	char *dummyP;
	char **dummyP2;
	char ***dummyP3;

	dummyP = dummy;
	dummyP2 = &dummyP;
	dummyP3 = &dummyP2;

	{
		const  char * a = dummyP;
		char  const * b = dummyP;
		char * const  c = dummyP;

		a = dummyP;
		b = dummyP;
//		c = dummyP; // �G���[

//		*a = 'a'; // �G���[
//		*b = 'a'; // �G���[
		*c = 'a';
	}

	{
		const  char ** a = dummyP2;
		char  const ** b = dummyP2;
		char * const * c = dummyP2;
		char ** const  d = dummyP2;

		a = dummyP2;
		b = dummyP2;
		c = dummyP2;
//		d = dummyP2; // �G���[

		*a = dummyP;
		*b = dummyP;
//		*c = dummyP; // �G���[
		*d = dummyP;

//		**a = 'a'; // �G���[
//		**b = 'a'; // �G���[
		**c = 'a';
		**d = 'a';
	}

	{
		const  char *** a = dummyP3;
		char  const *** b = dummyP3;
		char * const ** c = dummyP3;
		char ** const * d = dummyP3;
		char *** const  e = dummyP3;

		a = dummyP3;
		b = dummyP3;
		c = dummyP3;
		d = dummyP3;
//		e = dummyP3; // �G���[

		*a = dummyP2;
		*b = dummyP2;
		*c = dummyP2;
//		*d = dummyP2; // �G���[
		*e = dummyP2;

		**a = dummyP;
		**b = dummyP;
//		**c = dummyP; // �G���[
		**d = dummyP;
		**e = dummyP;

//		***a = 'a'; // �G���[
//		***b = 'a'; // �G���[
		***c = 'a';
		***d = 'a';
		***e = 'a';
	}
}
