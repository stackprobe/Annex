/*
	a.exe JUDGE-TYPE
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#define W 39
#define H 38

static int Map[W][H];

static uint GetArroundCount(uint x, uint y)
{
	uint count = 0;
	sint xc;
	sint yc;

	for(xc = -1; xc <= 1; xc++)
	for(yc = -1; yc <= 1; yc++)
	{
		uint xx = (x + W + xc) % W;
		uint yy = (y + H + yc) % H;

		if(Map[xx][yy])
		{
			count++;
		}
	}
	return count;
}
static int Judge_0004(uint x, uint y)
{
	return 4 <= GetArroundCount(x, y);
}
static int Judge_0005(uint x, uint y)
{
	return 5 <= GetArroundCount(x, y);
}
static int Judge_0006(uint x, uint y)
{
	return 6 <= GetArroundCount(x, y);
}
static int Judge_1001(uint x, uint y)
{
	return GetArroundCount(x, y) <= 1;
}
static int Judge_1002(uint x, uint y)
{
	return GetArroundCount(x, y) <= 2;
}
static int Judge_1003(uint x, uint y)
{
	return GetArroundCount(x, y) <= 3;
}
static int Judge_1004(uint x, uint y)
{
	return GetArroundCount(x, y) <= 4;
}
static int Judge_1005(uint x, uint y)
{
	return GetArroundCount(x, y) <= 5;
}
static int Judge_1006(uint x, uint y)
{
	return GetArroundCount(x, y) <= 6;
}
static int Judge_1007(uint x, uint y)
{
	return GetArroundCount(x, y) <= 7;
}
static int Judge_2001(uint x, uint y) // 奇数
{
	return GetArroundCount(x, y) % 2 == 1;
}
static int Judge_2002(uint x, uint y) // 偶数
{
	return GetArroundCount(x, y) % 2 == 0;
}
static int Judge_2003(uint x, uint y) // 素数
{
	uint count = GetArroundCount(x, y);

	return
		count == 2 ||
		count == 3 ||
		count == 5 ||
		count == 7;
}
static int Judge_2004(uint x, uint y) // ! 素数
{
	return !Judge_2003(x, y);
}

static int (*Judge)(uint, uint);

int main(int argc, char **argv)
{
	uint x;
	uint y;
	uint c;

	mt19937_initCRnd();

	switch(nextArg()[0]) // JUDGE-TYPE
	{
	case '4': Judge = Judge_0004; break; // ほぼ全部 1
	case '5': Judge = Judge_0005; break; // 群島★★★
	case '6': Judge = Judge_0006; break; // ほぼ全部 0
	case 'A': Judge = Judge_1001; break; // 柱だらけ
	case 'B': Judge = Judge_1002; break; // 直線の多い迷路・壁少ない★★
	case 'C': Judge = Judge_1003; break; // 直線の多い迷路★★★
	case 'D': Judge = Judge_1004; break; // 直線の多い迷路・壁多い★
	case 'E': Judge = Judge_1005; break; // 直線の多い迷路・壁過多
	case 'F': Judge = Judge_1006; break; // 直線の多い迷路・壁過多^2
	case 'G': Judge = Judge_1007; break; // 小部屋だらけ
	case '1': Judge = Judge_2001; break; // 何とも...
	case '2': Judge = Judge_2002; break; // 何とも...
	case 'P': Judge = Judge_2003; break; // 何とも...
	case 'N': Judge = Judge_2004; break; // 歩き回れる

	default:
		error();
	}
	for(x = 0; x < W; x++)
	for(y = 0; y < H; y++)
	{
		Map[x][y] = mt19937_rnd(2);
	}
	for(c = 0; c < W * H * 10; c++)
	{
		x = mt19937_rnd(W);
		y = mt19937_rnd(H);

		Map[x][y] = Judge(x, y);
	}
	for(y = 0; y < H; y++)
	{
		for(x = 0; x < W; x++)
		{
			cout("%s", Map[x][y] ? "■" : "□");
		}
		cout("\n");
	}
}
