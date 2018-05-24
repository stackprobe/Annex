#include <stdio.h>
#include <limits.h>
#include <math.h>

#define PLAYER_MAX 20

int main()
{
	int id;
	int rate;
	int rates[PLAYER_MAX];
	int c = 0;
	int a1;
	int a2;
	int b1;
	int b2;
	int minDiff = INT_MAX;
	int minA1;
	int minA2;
	int minB1;
	int minB2;

	while(scanf("%d %d", &id, &rate) != EOF)
	{
		rates[id] = rate;
		c++;
	}
	for(a1 = 0; a1 < c; a1++)
	for(a2 = a1 + 1; a2 < c; a2++)
	for(b1 = a1 + 1; b1 < c; b1++)
	for(b2 = b1 + 1; b2 < c; b2++)
	if(a2 != b1 && a2 != b2)
	{
		int diff = abs(rates[a1] + rates[a2] - rates[b1] - rates[b2]);

		if(diff < minDiff)
		{
			minDiff = diff;
			minA1 = a1;
			minA2 = a2;
			minB1 = b1;
			minB2 = b2;
		}
	}
	printf(
		"%d %d\n"
		"%d %d\n"
		,minA1, minA2
		,minB1, minB2
		);
}
