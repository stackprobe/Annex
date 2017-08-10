#include "all.h"

typedef struct
{
	int I;
}
VSB01S;

class VSB01C
{
public:
	VSB01S S()
	{
		VSB01S i = { 0 };
		return i;
	}
};

static void I(int *p)
{
	printf("%d\n", p);
}
void VSB01(void)
{
	VSB01C *c = new VSB01C();

//	I(&c->S().I); // NG: ‚½‚Ô‚ñc->S()‚Í‰E•Ó’l‚¾‚©‚ç
}
