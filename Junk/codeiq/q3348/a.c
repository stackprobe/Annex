#include <stdio.h>

static int GetMin(int a, int b)
{
	return a < b ? a : b;
}
static int GetMax(int a, int b)
{
	return a < b ? b : a;
}
static int GetCharCount(char *p, int chr)
{
	int count = 0;

	for(; *p; p++)
		if(*p == chr)
			count++;

	return count;
}
static void ReplaceChar(char *p, int cs, int cd)
{
	for(; *p; p++)
		if(*p == cs)
			*p = cd;
}

static int StrikeNum;
static int SpareNum;

static int ScoreMin = 300;
static int ScoreMax;

static int N_StrikeMin;
static int N_StrikeMax;
static int N_SpareMin;
static int N_SpareMax;

enum
{
	N_STRIKE,
	N_SPARE,
	N_OTHER,

	N_NUM, // num of N_*
};

static int N_Scorecard[9]; // N_* ...

static int GetScoreNext(char *p, int n)
{
	int score = 0;

	for(; n; p++)
	{
		if(*p == 'X')
		{
			score += 10;
			n--;
		}
		else if(*p == '/')
		{
			score += 10 - (p[-1] - '0');
			n--;
		}
		else if(*p == '-')
		{
			// noop
		}
		else
		{
			score += *p - '0';
			n--;
		}
	}
	return score;
}
static int GetScore(char *scorecard)
{
	int score = 0;
	int i;

	for(i = 0; i < 10; i++)
	{
		if(i == 9 || scorecard[i * 2] == 'X' || scorecard[i * 2 + 1] == '/')
		{
			score += GetScoreNext(scorecard + i * 2, 3);
		}
		else
		{
			score += scorecard[i * 2] - '0' + scorecard[i * 2 + 1] - '0';
		}
	}
//	printf("%03d %s\n", score, scorecard); // test
	return score;
}
static void CheckScore(char *finalRoll)
{
	char scorecard[22];
	char wsc[22];
	int i;

	for(i = 0; i < 9; i++)
	{
		char *p;

		switch(N_Scorecard[i])
		{
		case N_STRIKE: p = "X-"; break;
		case N_SPARE:  p = "!/"; break;
		case N_OTHER:  p = "!0"; break;
		}
		strcpy(scorecard + i * 2, p);
	}
	strcpy(scorecard + 18, finalRoll);

	if(
		StrikeNum != GetCharCount(scorecard, 'X') ||
		SpareNum  != GetCharCount(scorecard, '/')
		)
		return;

	strcpy(wsc, scorecard);
	ReplaceChar(wsc, '!', '0');
	ScoreMin = GetMin(ScoreMin, GetScore(wsc));

	strcpy(wsc, scorecard);
	ReplaceChar(wsc, '!', '9');
	ScoreMax = GetMax(ScoreMax, GetScore(wsc));
}
static void N_Found(void)
{
	/*
		'!' == '0' or '9'
	*/
	CheckScore("!0-");
	CheckScore("!/!");
	CheckScore("!/X");
	CheckScore("X!0");
	CheckScore("X!/");
	CheckScore("X!X");
	CheckScore("XX!");
	CheckScore("XXX");
}
static int N_IsFairScorecard(int i)
{
	return 1; // todo
}
static void N_Search(void)
{
	int ahead = 1;
	int i = 0;

	for(; ; )
	{
		if(ahead)
		{
			if(i == 9)
			{
				N_Found();
				i--;
				ahead = 0;
				continue;
			}
			N_Scorecard[i] = 0;
		}
		else
		{
			N_Scorecard[i]++;
		}
		if(N_Scorecard[i] < N_NUM && N_IsFairScorecard(i))
		{
			i++;
			ahead = 1;
		}
		else
		{
			if(!i)
				break;

			i--;
			ahead = 0;
		}
	}
}
int main()
{
	scanf("%d %d", &StrikeNum, &SpareNum);

	N_StrikeMin = GetMax(StrikeNum - 3, 0);
	N_StrikeMax = GetMin(StrikeNum - 0, 9);
	N_SpareMin = GetMax(SpareNum - 1, 0);
	N_SpareMax = GetMin(SpareNum - 0, 9);

	N_Search();

	printf("%d\n", ScoreMax - ScoreMin);
}
