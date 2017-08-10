#include "all.h"

static void Test_fint(void)
{
	{
		LOGPOS();
		fint_t a;
		LOGPOS();
		fint_t b = 1;
		LOGPOS();
		fint_t c = 1.0;
		LOGPOS();
		fint_t d = fint_t(1, 10);
		LOGPOS();

		cout("%f\n", (double)a);
		cout("%f\n", (double)b);
		cout("%f\n", (double)c);
		cout("%f\n", (double)d);
	}

	{
//		LOGPOS();
//		fint_t a;
		LOGPOS();
		fint_t b;
		b = 1;
		LOGPOS();
		fint_t c;
		c = 1.0;
		LOGPOS();
		fint_t d;
		d = fint_t(1, 10);
		LOGPOS();

//		cout("%f\n", (double)a);
		cout("%f\n", (double)b);
		cout("%f\n", (double)c);
		cout("%f\n", (double)d);
	}

	{
		LOGPOS();
		fint_t a = 1.5;
		fint_t b = 2.5;
		fint_t c;
		LOGPOS();

		c = a + b;

		cout("%f\n", (double)c);

		LOGPOS();
		c = a + (fint_t)1 + b;

		cout("%f\n", (double)c);

		LOGPOS();
		c = a + fint_t(1) + b;

		cout("%f\n", (double)c);
	}

	{
		LOGPOS();
		fint_t a = 123.456;
		fint_t b;
		LOGPOS();

		cout("%f\n", (double)a);

		b = a * (fint_t)-1;
		cout("%f\n", (double)b);

		a *= (fint_t)-1;
		cout("%f\n", (double)a);

		a = -b;
		cout("%f\n", (double)a);
	}
}
int main(int argc, char **argv)
{
	Test_fint();

	cout("OK!\n");

	return 0;
}
