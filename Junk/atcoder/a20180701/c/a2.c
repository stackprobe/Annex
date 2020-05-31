// use int

#include "C:\Factory\Common\all.h"

#define N_MAX 200000

static double GetSadness(int *aa, int n, double b)
{
	double ret = 0.0;
	int i;

	for(i = 0; i < n; i++)
		ret += fabs(aa[i] - (b + (i + 1)));

	return ret;
}
static double Search(int *aa, int n)
{
	double l = -IMAX * 100.0; // “K“–
	double r =  IMAX * 100.0; // “K“–
	double m;
	double md;
	int i;

	m = (l + r) / 2.0;
	md = GetSadness(aa, n, m);

	for(i = 0; i < 300; i++)
	{
		double lm = (l + m) / 2.0;
		double rm = (m + r) / 2.0;
		double lmd;
		double rmd;

		lmd = GetSadness(aa, n, lm);
		rmd = GetSadness(aa, n, rm);

		if(lmd < md)
		{
			r = m;
			m = lm;
			md = lmd;
		}
		else if(rmd < md)
		{
			l = m;
			m = rm;
			md = rmd;
		}
		else
		{
			l = lm;
			r = rm;
		}
	}

	cout("l: %.20f\n", l); // test
	cout("m: %.20f\n", m); // test
	cout("r: %.20f\n", r); // test

#if 1
	{
		double ans;
		double ans2;
		double ans3;
		double ans4;
		double ans5;

		m = (double)d2i64(m);

		ans  = GetSadness(aa, n, m - 2.0);
		ans2 = GetSadness(aa, n, m - 1.0);
		ans3 = GetSadness(aa, n, m);
		ans4 = GetSadness(aa, n, m + 1.0);
		ans5 = GetSadness(aa, n, m + 2.0);

		cout("ans:  %.20f\n", ans);
		cout("ans2: %.20f\n", ans2);
		cout("ans3: %.20f\n", ans3);
		cout("ans4: %.20f\n", ans4);
		cout("ans5: %.20f\n", ans5);

		m_minim(ans, ans2);
		m_minim(ans, ans3);
		m_minim(ans, ans4);
		m_minim(ans, ans5);

		return ans;
	}
#else // old, ng
	return md;
#endif
}
int main(int argc, char **argv)
{
	int n;
	int aa[N_MAX];
	int i;
	double ans;

	scanf("%d", &n);

	errorCase(n < 1);
	errorCase(N_MAX < n);

	for(i = 0; i < n; i++)
		scanf("%d", aa + i);

	ans = Search(aa, n);

	cout("%I64d\n", (__int64)(ans + 0.5));
}
