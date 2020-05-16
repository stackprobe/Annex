#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"
#include "C:\Factory\Common\Options\Sequence.h"
#include "C:\Factory\DevTools\libs\RandData.h"
#include "C:\Factory\OpenSource\md5.h"

static void Jammer(autoBlock_t *block, uint seed)
{
	autoList_t *swapIdxLst = createSq(getSize(block) / 2, 0, 1);
	uint swapIdx;
	uint index;

	mt19937_initRnd(seed);
	shuffle(swapIdxLst);
	mt19937_initCRnd(); // restore

	foreach(swapIdxLst, swapIdx, index)
		swapByte(block, index, getCount(swapIdxLst) + swapIdx);

	releaseAutoList(swapIdxLst);
}
static void DebugPrint_Block(autoBlock_t *block)
{
	cout("block_hash: %s (%u)\n", c_md5_makeHexHashBlock(block), getSize(block));
}
static void Test01_b(autoBlock_t *block, uint seed)
{
	autoBlock_t *bk = copyAutoBlock(block);

	DebugPrint_Block(block);
	Jammer(block, seed);
	DebugPrint_Block(block);
	Jammer(block, seed);
	DebugPrint_Block(block);

	errorCase(!isSameBlock(block, bk));

	releaseAutoBlock(bk);

	cout("ok\n");
}
static void Test01_a(uint size, uint seed)
{
	autoBlock_t *block;

	block = MakeRandBinaryBlock(size);

	Test01_b(block, seed);

	releaseAutoBlock(block);
}
static void Test01(void)
{
	uint c;

	Test01_a(100, 1);
	Test01_a(100, 2);
	Test01_a(100, 3);
	Test01_a(200, 1);
	Test01_a(200, 2);
	Test01_a(200, 3);
	Test01_a(300, 1);
	Test01_a(300, 2);
	Test01_a(300, 3);

	for(c = 0; c < 1000; c++)
	{
		cout("c: %u\n", c);

		Test01_a(mt19937_rnd(10), mt19937_rnd32());
		Test01_a(mt19937_rnd(100), mt19937_rnd32());
		Test01_a(mt19937_rnd(1000), mt19937_rnd32());
		Test01_a(mt19937_rnd(10000), mt19937_rnd32());
		Test01_a(mt19937_rnd(100000), mt19937_rnd32());
	}
}
int main(int argc, char **argv)
{
	mt19937_initCRnd();

	Test01();
}
