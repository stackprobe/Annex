#include "C:\Factory\Common\all.h"

#define GOSA_MARGIN 0.0000000001

static double *NewR(double r)
{
	return (double *)memClone(&r, sizeof(double));
}
static sint Trim_Comp(uint v1, uint v2)
{
	double a = *(double *)v1;
	double b = *(double *)v2;

	if(a < b)
		return -1;

	if(b < a)
		return 1;

	return 0;
}
static void Trim(autoList_t *rs)
{
	uint index;

	rapidSort(rs, Trim_Comp);

	for(index = 1; index < getCount(rs); index++)
	{
		double a = *(double *)getElement(rs, index - 1);
		double b = *(double *)getElement(rs, index);

		errorCase(b < a);

		if(b - a < GOSA_MARGIN)
		{
			memFree((void *)desertElement(rs, index));
			index--;
		}
	}
}
static autoList_t *GetRs(uint rNum)
{
	autoList_t *dest = newList();

	errorCase(!rNum);

	if(rNum == 1)
	{
		addElement(dest, (uint)NewR(1.0));
	}
	else
	{
		uint cnt;

		for(cnt = 1; cnt <= rNum / 2; cnt++)
		{
			autoList_t *rs1 = GetRs(cnt);
			autoList_t *rs2 = GetRs(rNum - cnt);
			double *r1;
			double *r2;
			uint i1;
			uint i2;

			foreach(rs1, r1, i1)
			foreach(rs2, r2, i2)
			{
				double a = *r1;
				double b = *r2;
				double s, p;

				s = a + b;
				p = 1.0 / ((1.0 / a) + (1.0 / b));

				addElement(dest, (uint)NewR(s));
				addElement(dest, (uint)NewR(p));
			}
			releaseDim(rs1, 1);
			releaseDim(rs2, 1);
		}
	}
	Trim(dest);
	return dest;
}
static void Search(uint rNum)
{
	autoList_t *rs = GetRs(rNum);
	double *r;
	uint index;

	foreach(rs, r, index)
		cout("%.20f\n", *r);

	cout("%u\n", getCount(rs));

	releaseDim(rs, 1);
}
int main(int argc, char **argv)
{
	Search(toValue(nextArg()));
}
