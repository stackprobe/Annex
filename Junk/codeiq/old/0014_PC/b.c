#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Progress.h"

#define N 12

static int Members[N]; // -1: 持ち帰ってない(待機中), 0～(N-1): 持ち帰った
static int PCList[N]; // 1: ある, 0: ない(持ち帰った)

static int TryTake(int m) // ret: ? 持ち帰り成功
{
	int p = Members[m]; // PC

	errorCase(!m_isRange(p, -1, N - 1)); // この範囲のはず。

	if(p != -1) // ? m は p を持ち帰っている。-> 一旦戻す。
	{
		errorCase(PCList[p] == 1); // ? ある <- m が持ち帰っているので無いはず。
		Members[m] = -1; // 持ち帰ってない状態に戻す。
		PCList[p] = 1; // PCも戻す。
	}

	for(; ; )
	{
		p++; // 次のPC
		errorCase(!m_isRange(p, 0, N)); // この範囲のはず。

		if(N <= p) // ? これ以上PCは無い -> 持ち帰り断念
			return 0;

		if(p != m) // ? 自分のPCではない。
		{
			if(PCList[p] == 1) // ? ある。-> それを持ち帰る。
			{
				Members[m] = p; // 持ち帰る。
				PCList[p] = 0; // 持ち帰る。
				return 1;
			}
		}
	}
	error();
	return -1; // dummy
}
int main(int argc, char **argv)
{
	int i;
	int m = 0; // member
	int ptnNum = 0;

	for(i = 0; i < N; i++)
	{
		Members[i] = -1;
		PCList[i] = 1;
	}

	while(0 <= m)
	{
		if(N <= m) // ? 全員持ち帰り成功
		{
			ptnNum++;
			m--;
		}

		if(TryTake(m))
			m++;
		else
			m--;
	}

	cout("%d\n", ptnNum);
}
