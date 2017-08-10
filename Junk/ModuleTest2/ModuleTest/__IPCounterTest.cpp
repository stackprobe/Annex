#include "all.h"

static autoList<char *> *IPList;

static char *MakeIP(void)
{
	return xcout("192.168.123.%d", rnd(256));
//	return xcout("192.168.%d.%d", rnd(256), rnd(256));
}
static int GetIPCount_Test(char *ip)
{
	int count = 0;

	for(int index = 0; index < IPList->GetCount(); index++)
		if(!strcmp(IPList->GetElement(index), ip))
			count++;

	return count;
}
static void CheckIPCount(char *ip)
{
	int c1 = GetIPCount(ip);
	int c2 = GetIPCount_Test(ip);

	cout("ip: %s\n", ip);
	cout("c1: %d\n", c1);
	cout("c2: %d\n", c2);

	errorCase(c1 != c2);
}
static int IsDoDel(void)
{
	int c = rnd(3);
	int ret;

	if(((int)dNow() / 30) % 2)
	{
		cout("rate 2/3\n");
		ret = c == 0 || c == 1;
	}
	else
	{
		cout("rate 1/3\n");
		ret = c == 0;
	}
	cout("%s\n", ret ? "DEL" : "ADD");
	return ret;
}
void Test_IPCounter(void)
{
	IPList = new autoList<char *>();

	while(!_kbhit() || _getch() != 0x1b)
	{
		cout("Total: %d\n", IPList->GetCount());

		if(IsDoDel()) // ? íœ
		{
			if(IPList->GetCount())
			{
				char *ip = IPList->DesertElement(rnd(IPList->GetCount()));

				DecrementIPCount(ip);
				memFree(ip);
			}
		}
		else // ? ’Ç‰Á
		{
			char *ip = MakeIP();

			IncrementIPCount(ip);
			IPList->AddElement(ip);
		}

		if(IPList->GetCount())
			CheckIPCount(IPList->GetElement(rnd(IPList->GetCount())));

		{
			char *ip = MakeIP();
			CheckIPCount(ip);
			memFree(ip);
		}
	}
	releaseList(IPList, (void (*)(char *))memFree);
}
