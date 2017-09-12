#include "C:\Factory\Common\all.h"

#define NODE_NUM 6
#define ROUTE_LEN (NODE_NUM + 1)

int GetEnergy(int from, int to)
{
	if(from == 1)
	{
		if(to == 2) return 7;
		if(to == 3) return 12;
		if(to == 4) return 8;
		if(to == 5) return 11;
		if(to == 6) return 7;
	}
	if(from == 2)
	{
		if(to == 1) return 3;
		// ----
		if(to == 3) return 10;
		if(to == 4) return 7;
		if(to == 5) return 13;
		if(to == 6) return 2;
	}
	if(from == 3)
	{
		if(to == 1) return 4;
		if(to == 2) return 8;
		// ----
		if(to == 4) return 9;
		if(to == 5) return 12;
		if(to == 6) return 3;
	}
	if(from == 4)
	{
		if(to == 1) return 6;
		if(to == 2) return 6;
		if(to == 3) return 9;
		// ----
		if(to == 5) return 10;
		if(to == 6) return 7;
	}
	if(from == 5)
	{
		if(to == 1) return 7;
		if(to == 2) return 7;
		if(to == 3) return 11;
		if(to == 4) return 10;
		// ----
		if(to == 6) return 5;
	}
	if(from == 6)
	{
		if(to == 1) return 9;
		if(to == 2) return 7;
		if(to == 3) return 8;
		if(to == 4) return 9;
		if(to == 5) return 10;
	}
	return -1; // ïsê≥Ç»åoòH
}

static int Route[NODE_NUM + 1];

static void DoTestRoute(void)
{
	int index;
	int e = 0;

	cout("%d", Route[0]);

	for(index = 0; index < ROUTE_LEN - 1; index++)
	{
		cout(" -> %d", Route[index + 1]);
		e += GetEnergy(Route[index], Route[index + 1]);
	}
	cout(" = %d\n", e);
}

static int HasPair(int *bgn, int *end)
{
	int *p;
	int *q;

	for(p = bgn + 1; p <= end; p++)
	for(q = bgn; q < p; q++)
		if(*p == *q)
			return 1;

	return 0;
}

static int IsRouteOk(void)
{
	int index;

	for(index = 0; index < ROUTE_LEN - 1; index++)
		if(GetEnergy(Route[index], Route[index + 1]) == -1)
			return 0;

	if(Route[0] != 1)
		return 0;

	for(index = 1; index < ROUTE_LEN - 1; index++)
		if(Route[index] == 1)
			return 0;

	if(HasPair(Route + 1, Route + ROUTE_LEN - 2))
		return 0;

	if(Route[ROUTE_LEN - 1] != 1)
		return 0;

	return 1;
}
static int RouteInc(void)
{
	int index;

	for(index = 0; index < ROUTE_LEN; index++)
	{
		if(Route[index] < NODE_NUM)
		{
			Route[index]++;
			return 0;
		}
		Route[index] = 1;
	}
	return 1;
}
static void DoTest(void)
{
	int index;

	for(index = 0; index < ROUTE_LEN; index++)
		Route[index] = 1;

	for(; ; )
	{
		if(IsRouteOk())
			DoTestRoute();

		if(RouteInc())
			break;
	}
}

int main(int argc, char **argv)
{
	DoTest();
}
