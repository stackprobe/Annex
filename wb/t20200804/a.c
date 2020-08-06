#include "C:\Factory\Common\all.h"

#define B_1111111110 0x3fe
#define B_1111111111 0x3ff

static int IsDupl(uint v)
{
	uint m;

	if(v)
	{
		m = 0;

		while(v)
		{
			uint mx = 1 << v % 10;

			if(m & mx)
				return 1;

			m |= mx;
			v /= 10;
		}
	}
	else
	{
		// noop
	}
	return 0;
}

static uint M_Cnt;

static uint Mask(uint v)
{
	uint m;

	if(v)
	{
		m = 0;

		while(v)
		{
			m |= 1 << v % 10;
			v /= 10;

			M_Cnt++;
		}
	}
	else
	{
		m = 1;

		M_Cnt++;
	}
	return m;
}
static uint Mask_u3(uint v1, uint v2, uint v3)
{
	M_Cnt = 0;
	return Mask(v1) | Mask(v2) | Mask(v3);
}
static void Search(uint (*fCalc)(uint, uint), int chrOp)
{
	uint a;
	uint b;
	uint c;
	uint m;

	for(a = 0; a < UINTMAX; a++)
	{
		if(!IsDupl(a))
		{
			for(b = 0; b <= a; b++)
			{
				c = fCalc(a, b);
				m = Mask_u3(a, b, c);

				if(M_Cnt == 9)
				{
					if(m == B_1111111110)
					{
						cout("%c[1-9] %u %c %u = %u\n", chrOp, a, chrOp, b, c);
					}
				}
				else if(M_Cnt == 10)
				{
					if(m == B_1111111111)
					{
						cout("%c[0-9] %u %c %u = %u\n", chrOp, a, chrOp, b, c);
					}
				}
				else if(11 <= M_Cnt)
				{
					if(b < 2)
						goto endLoop_a;

					break;
				}
			}
		}
	}
endLoop_a:;
}
static uint F_Add(uint a, uint b)
{
	return a + b;
}
static uint F_Mul(uint a, uint b)
{
	return a * b;
}
int main(int argc, char **argv)
{
	Search(F_Add, '+');
	Search(F_Mul, '*');
}
