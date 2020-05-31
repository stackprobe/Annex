/*
	åüèÿ
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\Random.h"
#include "C:\Factory\Common\Options\Sequence.h"

static autoList_t *List;

/*
	èdï°ÇÃñ≥Ç¢ÅAÇPÅ`óvëfêîÇÃÉäÉXÉg
*/
static void MkList(void)
{
	if(List)
		releaseAutoList(List);

	List = createSq(mt19937_range(2, 1000), 1, 1);
	shuffle(List);
}
static uint TryTest(void)
{
	char *rFile = makeTempFile(NULL);
	char *wFile = makeTempFile(NULL);
	uint ans;

	{
		FILE *fp = fileOpen(rFile, "wt");
		uint index;

		for(index = 0; index < getCount(List); index++)
			writeLine_x(fp, xcout("%u", getElement(List, index)));

		fileClose(fp);
	}

	coExecute_x(xcout("a\\a\\bin\\Release\\a.exe \"%s\" > \"%s\"", rFile, wFile));

	ans = toValue_x(readText(wFile));

	removeFile_x(rFile);
	removeFile_x(wFile);

	return ans;
}
static uint DoBSort(void)
{
	uint cnt = 0;

	for(; ; )
	{
		uint i;
		int swapped = 0;

		for(i = 0; i + 1 < getCount(List); i++)
		{
			if(getElement(List, i + 1) < getElement(List, i))
			{
				swapElement(List, i, i + 1);
				swapped = 1;
				cnt++;
			}
		}
		if(!swapped)
			break;
	}
	return cnt;
}

static void ShowList(void)
{
	uint i;

	for(i = 0; i < getCount(List); i++)
	{
		if(i)
			cout(", ");

		cout("%u", getElement(List, i));
	}
	cout("\n");
}

int main(int argc, char **argv)
{
	mt19937_init();

	while(!hasKey())
	{
		uint cnt1;
		uint cnt2;

		MkList();
		ShowList();
		cout("%u\n", getCount(List));

		cnt1 = TryTest();
		cnt2 = DoBSort();
		ShowList();

		cout("%u %u (%u\n", cnt1, cnt2, cnt1 - cnt2);

		errorCase(cnt1 != cnt2);
	}
}
