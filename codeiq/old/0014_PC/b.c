#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Progress.h"

#define N 12

static int Members[N]; // -1: �����A���ĂȂ�(�ҋ@��), 0�`(N-1): �����A����
static int PCList[N]; // 1: ����, 0: �Ȃ�(�����A����)

static int TryTake(int m) // ret: ? �����A�萬��
{
	int p = Members[m]; // PC

	errorCase(!m_isRange(p, -1, N - 1)); // ���͈̔͂̂͂��B

	if(p != -1) // ? m �� p �������A���Ă���B-> ��U�߂��B
	{
		errorCase(PCList[p] == 1); // ? ���� <- m �������A���Ă���̂Ŗ����͂��B
		Members[m] = -1; // �����A���ĂȂ���Ԃɖ߂��B
		PCList[p] = 1; // PC���߂��B
	}

	for(; ; )
	{
		p++; // ����PC
		errorCase(!m_isRange(p, 0, N)); // ���͈̔͂̂͂��B

		if(N <= p) // ? ����ȏ�PC�͖��� -> �����A��f�O
			return 0;

		if(p != m) // ? ������PC�ł͂Ȃ��B
		{
			if(PCList[p] == 1) // ? ����B-> ����������A��B
			{
				Members[m] = p; // �����A��B
				PCList[p] = 0; // �����A��B
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
		if(N <= m) // ? �S�������A�萬��
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
