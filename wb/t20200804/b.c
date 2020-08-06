#include "C:\Factory\Common\all.h"

static uint Digs[11];
static uint DigSt;
static uint DigEd;
static uint DigLen;

static uint LIdx;
static uint LVal;
static uint RIdx;
static uint RVal;
static uint AnsVal;

static void FoundAns(void)
{
	// 前後入れ替えて同じ場合を除外
	// memo: 数字の重複を認めないのだから LVal == RVal は無い。

	if(LVal > RVal)
	{
		if(LVal + RVal == AnsVal)
		{
			cout("+[%u-9] %u + %u = %u\n", DigSt, LVal, RVal, AnsVal);
		}
		else if(LVal * RVal == AnsVal)
		{
			cout("*[%u-9] %u * %u = %u\n", DigSt, LVal, RVal, AnsVal);
		}
	}
}
static void FindAns(void)
{
	if(Digs[RIdx])
	{
		uint v = 0;
		uint i;

		for(i = RIdx; i < DigLen; i++)
		{
			v *= 10;
			v += Digs[i];
		}

		AnsVal = v;

		FoundAns();
	}
	else
	{
		if(RIdx + 1 == DigLen)
		{
			AnsVal = 0;

			FoundAns();
		}
	}
}
static void FindROp(void)
{
	if(Digs[LIdx])
	{
		RIdx = LIdx;
		RVal = 0;

		while(RIdx + 1 < DigLen)
		{
			RVal *= 10;
			RVal += Digs[RIdx];
			RIdx++;

			FindAns();
		}
	}
	else
	{
		RIdx = LIdx + 1;
		RVal = 0;

		FindAns();
	}
}
static void FindLOp(void)
{
	if(Digs[0])
	{
		LIdx = 0;
		LVal = 0;

		while(LIdx + 2 < DigLen)
		{
			LVal *= 10;
			LVal += Digs[LIdx];
			LIdx++;

			FindROp();
		}
	}
	else
	{
		LIdx = 1;
		LVal = 0;

		FindROp();
	}
}
static int HasDig(uint dig, uint st, uint ed)
{
	uint i;

	for(i = st; i < ed; i++)
		if(Digs[i] == dig)
			return 1;

	return 0;
}
static void Search(uint index)
{
	if(index < DigLen)
	{
		uint dig;

		for(dig = DigSt; dig <= DigEd; dig++)
		{
			if(!HasDig(dig, 0, index))
			{
				Digs[index] = dig;

				Search(index + 1);
			}
		}
	}
	else
		FindLOp();
}
int main(int argc, char **argv)
{
	hasArgs(0); // for //x options

	// ---- [1-9]

	DigSt = 1;
	DigEd = 9;
	DigLen = 9;

	Search(0);

	// ---- [0-9]

	DigSt = 0;
	DigEd = 9;
	DigLen = 10;

	Search(0);

	// ----
}
