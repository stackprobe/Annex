#include "C:\Factory\Common\all.h"

static uint MasuVals[10];
static uint Masu;
static uint Ptn;

static int IsCorrect(void)
{
	uint i;

	for(i = 1; i < Masu; i++)
		if(MasuVals[i] <= MasuVals[i - 1])
			return 0;

	return 1;
}
static uint Mul(void)
{
	uint m = 1;
	uint i;

	for(i = 0; i < Masu; i++)
		m *= MasuVals[i];

	return m;
}
static uint Search_2(uint m)
{
	uint ans = 0;
	uint i;

	for(i = 0; i < Masu; i++)
		MasuVals[i] = 1;

	for(; ; )
	{
		if(IsCorrect() && Mul() == m)
			ans++;

		// next
		{
			for(i = 0; i < Masu; i++)
			{
				if(MasuVals[i] < 9)
				{
					MasuVals[i]++;
					break;
				}
				MasuVals[i] = 1;
			}
			if(i == Masu)
				break;
		}
	}
	return ans;
}
static uint Search(void)
{
	uint ans = 0;
	uint m;

	for(m = 1; m <= 32768; m++)
		if(Search_2(m) == Ptn)
			ans++;

	return ans;
}
int main(int argc, char **argv)
{
	Masu = toValue(nextArg());
	Ptn  = toValue(nextArg());

	errorCase(!m_isRange(Masu, 1, 9));
	errorCase(!m_isRange(Ptn,  1, 9));

	cout("%u\n", Search());
}
