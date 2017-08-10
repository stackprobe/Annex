#include "all.h"

#define BLOCK_MAX 1024
#define BLOCKSIZE_MAX 300
//#define BLOCKSIZE_MAX 300000
#define TOTALSIZE_MAX INT_MAX

static struct
{
	uchar *Block;
	int BlockSize;
	int Seed;
}
BlockInfos[BLOCK_MAX];

static int BlockCount;
static int TotalSize;

static int AllocCount;
static int ReallocCount;
static int FreeCount;

static int NextBlockSize(void)
{
	__int64 blockSize = rnd(BLOCKSIZE_MAX);

	blockSize *= rnd(BLOCKSIZE_MAX);
	blockSize /= BLOCKSIZE_MAX;

	errorCase(blockSize < 0 || BLOCKSIZE_MAX <= blockSize);

	return (int)blockSize;
}
static int NextSeed(void)
{
	static int seed;
	return seed++;
}
void MemoryTest(void)
{
	int nextMillis = 0;

	for(; ; )
	{
		int blockSize = NextBlockSize();

		if(!BlockCount || BlockCount + 1 < BLOCK_MAX && TotalSize + blockSize < TOTALSIZE_MAX && rnd(2)) // alloc
		{
			cout("A");

			uchar *block = (uchar *)memAlloc(blockSize);
			int seed = NextSeed();

			Random *r = new Random(seed);

			for(int c = 0; c < blockSize; c++)
			{
				block[c] = r->Rnd(256);
			}
			delete r;

			BlockInfos[BlockCount].Block = block;
			BlockInfos[BlockCount].BlockSize = blockSize;
			BlockInfos[BlockCount].Seed = seed;
			BlockCount++;

			TotalSize += blockSize;

			AllocCount++;
		}
		else // realloc, free
		{
			int index = rnd(BlockCount);

			uchar *block = BlockInfos[index].Block;
			blockSize = BlockInfos[index].BlockSize;
			Random *r = new Random(BlockInfos[index].Seed);

			for(int c = 0; c < blockSize; c++)
			{
//				errorCase(block[c] != r->Rnd(256));
				int rc;
				if(block[c] != (rc = r->Rnd(256)))
				{
					cout("_(%d %d) %d %d %d %d\n", block[c], rc, blockSize, c, index, BlockCount);
//					error();
				}
			}
			delete r;

			if(rnd(2)) // realloc
			{
				cout("R");

				int newBlockSize = NextBlockSize();
LOGPOS();
				int smallBlockSize = m_min(blockSize, newBlockSize);
LOGPOS();

				block = (uchar *)memRealloc(block, newBlockSize);
LOGPOS();
				r = new Random(BlockInfos[index].Seed);
LOGPOS();

				for(int c = 0; c < smallBlockSize; c++)
				{
//					errorCase(block[c] != r->Rnd(256));
LOGPOS();
					int rc;
LOGPOS();
					if(block[c] != (rc = r->Rnd(256)))
					{
						cout("_(%d %d) %d %d %d %d\n", block[c], rc, blockSize, c, index, BlockCount);
//						error();
					}
LOGPOS();
				}
LOGPOS();
				delete r;
LOGPOS();
				int newSeed = NextSeed();
LOGPOS();
				r = new Random(newSeed);
LOGPOS();

				for(int c = 0; c < newBlockSize; c++)
				{
					block[c] = r->Rnd(256);
				}
LOGPOS();
				delete r;
LOGPOS();

				BlockInfos[index].Block = block;
LOGPOS();
				BlockInfos[index].BlockSize = newBlockSize;
LOGPOS();
				BlockInfos[index].Seed = newSeed;
LOGPOS();

				TotalSize -= blockSize;
LOGPOS();
				TotalSize += newBlockSize;
LOGPOS();

				ReallocCount++;
LOGPOS();
			}
			else // free
			{
				cout("F");

LOGPOS();
				memFree(block);
LOGPOS();

				BlockCount--;
				BlockInfos[index] = BlockInfos[BlockCount];

				TotalSize -= blockSize;

				FreeCount++;
			}
		}
	}
}
