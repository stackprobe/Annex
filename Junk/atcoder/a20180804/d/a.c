#include "C:\Factory\Common\all.h"

static int Rev(int v)
{
	char buff[4];

	sprintf(buff, "%d", v);
	reverseLine(buff);
	return atoi(buff);
}

typedef struct State_st
{
	int Footprint;
	int Completed;
	int Incompleted;
}
State_t;

static State_t Map[1000][1000];
static autoList_t *Route;

static int IsCompleted(int x, int y)
{
	int foot = x * 1000 + y;
	State_t *state;
	int index;
	int completed;

	setCount(Route, 0);

	for(; ; )
	{
		state = Map[x] + y;

		if(state->Footprint == foot)
		{
			completed = 0;
			break;
		}
		if(state->Completed)
		{
			completed = 1;
			break;
		}
		if(state->Incompleted)
		{
			completed = 0;
			break;
		}
		addElement(Route, (uint)state);

		if(x < y)
			x = Rev(x);
		else
			y = Rev(y);

		if(x < y)
			y -= x;
		else
			x -= y;

		if(!x || !y)
		{
			completed = 1;
			break;
		}
		state->Footprint = foot;
	}
	foreach(Route, state, index)
	{
		if(completed)
			state->Completed = 1;
		else
			state->Incompleted = 1;
	}
	return completed;
}
int main(int argc, char **argv)
{
	int ans = 0;
	int n;
	int m;
	int x;
	int y;

	Route = newList();

	scanf("%d", &n);
	scanf("%d", &m);

	for(x = 1; x <= n; x++)
	for(y = 1; y <= m; y++)
	{
		if(!IsCompleted(x, y))
		{
			ans++;
		}
	}
	cout("%d\n", ans);
}
