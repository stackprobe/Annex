#include "all.h"

static uint EvStop;
static int Stopped;

static void FNLZ(void)
{
	handleClose(EvStop);
}
void Stop_INIT(void)
{
	EvStop = eventOpen("{b14487c3-8699-4fab-a093-43c31182f4ba}");
	addFinalizer(FNLZ);
}
void Stop(void)
{
	eventSet(EvStop);
}
int IsStopped(void)
{
	if(Stopped)
		return 1;

	if(pulseSec(2, NULL) && handleWaitForMillis(EvStop, 0))
	{
		LOGPOS();
		Stopped = 1;
		return 1;
	}
	return 0;
}
