#include "C:\Factory\Common\all.h"

typedef struct State_st
{
	int Value;
	int Store;
	int Rem;
	struct State_st *Prev;
	char *Action;
}
State_t;

static State_t ToNext(State_t *state)
{
	State_t next = *state;

	next.Rem--;
	next.Prev = state;

	return next;
}
static void PrintRoute(State_t *state)
{
	if(state->Prev)
		PrintRoute(state->Prev);

	cout("%s ", state->Action);
}
static int GetDepth(State_t *state)
{
	if(state->Prev)
		return GetDepth(state->Prev) + 1;

	return 0;
}
static void Search(State_t state)
{
	State_t next;

	while(1000 <= state.Value)
	{
		state.Value =
			state.Value % 1000 +              // 1`3 Œ…–Ú
			(state.Value / 10000) * 1000 +    // 5` Œ…–Ú -> 4` Œ…–Ú
			((state.Value / 1000) % 10) * 10; // 4 Œ…–Ú -> 2 Œ…–Ú
	}
	if(state.Value == 822)
	{
		PrintRoute(&state);
		cout("GOAL (%d)\n", GetDepth(&state));
		return;
	}
	if(state.Rem == 0)
		return;

	next = ToNext(&state);

	{
		char *tmp = xcout("%d", next.Value);
		char *tmp2;

		tmp2 = strx(tmp);
		reverseLine(tmp2);
		tmp = addLine(tmp, tmp2);

		next.Value = atoi(tmp);

		memFree(tmp);
		memFree(tmp2);
	}

	next.Action = "Mirror";

	Search(next);

	next = ToNext(&state);

	next.Value *= 10;
	next.Value += 5;
	next.Action = "5";

	Search(next);

	if(next.Store != -1)
	{
		next = ToNext(&state);

		{
			char *tmp = xcout("%d%d", next.Value, next.Store);

			next.Value = atoi(tmp);

			memFree(tmp);
		}

		next.Action = "Store";

		Search(next);
	}

	next = ToNext(&state);

	next.Value /= 10;
	next.Action = "<<";

	Search(next);

	if(state.Store != state.Value)
	{
		next = ToNext(&state);

		next.Store = state.Value;
		next.Rem++;
		next.Action = "Store+";

		Search(next);
	}
}
int main(int argc, char **argv)
{
	State_t state;

	state.Value = 25;
	state.Store = -1;
	state.Rem = 5;
	state.Prev = NULL;
	state.Action = "START";

	Search(state);
}
