/*
	2 �ȏ�̐��� n �� ( 2^n + 1 ) / n^2 �������ƂȂ�悤�Ȃ��̂�S�ċ��߂�B

	- - -

	3 ���������炵���B
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Calc.h"

int main(int argc, char **argv)
{
	uint n;

	// ���ł� 1 ���A
	for(n = 1; waitKey(0) != 0x1b; n++)
	{
		char *numer = calcLine_xc(calcPower("2", n, 10), '+', "1", 10, 0);
		char *denom = calcPower_x(xcout("%u", n), 2, 10);
		char *ans;

		ans = calcLine_xx(numer, '/', denom, 10, 0);

//		cout("[%c] %u\n", calcLastMarume ? ' ' : '*', n);

		cmdTitle_x(xcout("%u �Ȃ�", n));

		if(!calcLastMarume)
			cout("%u\n", n);

		memFree(ans);
	}
}
