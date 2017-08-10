#include "all.h"

void TFuncTest(void)
{
	{
		double pi = 3.14;
		double ret = TFuncTest(pi);
		cout("%f\n", ret);
	}

	{
		TFuncTest2(cout, (char *)"TFuncTest2_OK!\n");
	}
}
