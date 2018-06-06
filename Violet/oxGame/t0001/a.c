/*
	a.exe [/Ro] [/Rx]
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\CRRandom.h"

#define OTHER_PL_STN(plStn) \
	((plStn) ^ 'o' ^ 'x')

static int PoRndFlg;
static int PxRndFlg;

typedef struct State_st
{
	int Map[3][3]; // " ox"
}
State_t;

static void InitState(State_t *i)
{
	uint x;
	uint y;

	for(x = 0; x < 3; x++)
	for(y = 0; y < 3; y++)
	{
		i->Map[x][y] = ' ';
	}
}
static void PrintState(State_t *i)
{
	uint x;
	uint y;

	cout("------\n");

	for(y = 0; y < 3; y++)
	{
		for(x = 0; x < 3; x++)
		{
			switch(i->Map[x][y])
			{
			case ' ': cout("Å@"); break;
			case 'o': cout("Åõ"); break;
			case 'x': cout("Å~"); break;

			default:
				error();
			}
		}
		cout("\n");
	}
	cout("------\n");
}
static int IsWin(State_t *i, int plStn)
{
	uint x;
	uint y;

	for(x = 0; x < 3; x++)
	{
		if(
			i->Map[x][0] == plStn &&
			i->Map[x][1] == plStn &&
			i->Map[x][2] == plStn
			)
			return 1;
	}
	for(y = 0; y < 3; y++)
	{
		if(
			i->Map[0][y] == plStn &&
			i->Map[1][y] == plStn &&
			i->Map[2][y] == plStn
			)
			return 1;
	}
	if(
		i->Map[0][0] == plStn &&
		i->Map[1][1] == plStn &&
		i->Map[2][2] == plStn
		)
		return 1;

	if(
		i->Map[0][2] == plStn &&
		i->Map[1][1] == plStn &&
		i->Map[2][0] == plStn
		)
		return 1;

	return 0;
}
static int IsWinOrDraw(State_t *i)
{
	uint x;
	uint y;

	for(x = 0; x < 3; x++)
	for(y = 0; y < 3; y++)
	{
		if(i->Map[x][y] == ' ')
			goto noDraw;
	}
	return 1; // win or draw

noDraw:
	return IsWin(i, 'o') || IsWin(i, 'x');
}

// ---- Thinker ----

static uint GetWeight(State_t *i, int plStn) // ret: 1 == lose, 2 == draw, 3 == win
{
	uint x;
	uint y;
	uint ret;


	// TODO âΩÇ©ïœ...


	if(IsWin(i, plStn))
		return 3;

	if(IsWin(i, OTHER_PL_STN(plStn)))
		return 1;

	ret = 2;

	for(x = 0; x < 3; x++)
	for(y = 0; y < 3; y++)
	{
		if(i->Map[x][y] == ' ')
		{
			uint oplWgt;

			i->Map[x][y] = plStn;
			oplWgt = GetWeight(i, OTHER_PL_STN(plStn));
			i->Map[x][y] = ' ';

			if(oplWgt == 3)
				return 1;

			if(oplWgt == 1)
				ret = 3;
		}
	}
	return ret;
}

// ---- Turn ----

static void TurnP(State_t *i, int plStn, int rndFlg)
{
	uint x;
	uint y;

	errorCase(IsWinOrDraw(i));

	if(rndFlg)
	{
		do
		{
			x = mt19937_rnd(3);
			y = mt19937_rnd(3);
		}
		while(i->Map[x][y] != ' ');

		i->Map[x][y] = plStn;
	}
	else
	{
		uint wgtMap[3][3];

		zeroclear(wgtMap);

		for(x = 0; x < 3; x++)
		for(y = 0; y < 3; y++)
		{
			if(i->Map[x][y] == ' ')
			{
				i->Map[x][y] = plStn;
				wgtMap[x][y] = GetWeight(i, plStn);
				i->Map[x][y] = ' ';
			}
		}

		{
			uint maxWgt = 0;
			uint maxWgtCnt = 0;
			uint c;

			for(x = 0; x < 3; x++)
			for(y = 0; y < 3; y++)
			{
				if(maxWgt < wgtMap[x][y])
				{
					maxWgt = wgtMap[x][y];
					maxWgtCnt = 1;
				}
				else if(maxWgt == wgtMap[x][y])
				{
					maxWgtCnt++;
				}
			}
			c = mt19937_rnd(maxWgtCnt) + 1;

			for(x = 0; x < 3; x++)
			for(y = 0; y < 3; y++)
			{
				if(maxWgt == wgtMap[x][y] && !--c)
					goto fnd_c_maxWgt;
			}
			error();
		}

	fnd_c_maxWgt:
		i->Map[x][y] = plStn;
	}
}
static void TurnPo(State_t *i)
{
	TurnP(i, 'o', PoRndFlg);
}
static void TurnPx(State_t *i)
{
	TurnP(i, 'x', PxRndFlg);
}

// ----

int main(int argc, char **argv)
{
	State_t state;

	mt19937_initCRnd();

readArgs:
	if(argIs("/Ro"))
	{
		PoRndFlg = 1;
		goto readArgs;
	}
	if(argIs("/Rx"))
	{
		PxRndFlg = 1;
		goto readArgs;
	}

	InitState(&state);

//	PrintState(&state);

	for(; ; )
	{
		TurnPo(&state);

		if(IsWinOrDraw(&state))
			break;

		PrintState(&state);

		TurnPx(&state);

		if(IsWinOrDraw(&state))
			break;

		PrintState(&state);
	}
	PrintState(&state);
}
