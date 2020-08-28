#include "C:\Factory\Common\all.h"

#define PIDS_FILE "C:\\temp\\pids.txt"

int main(int argc, char **argv)
{
	if(argIs("/C"))
	{
		autoList_t *pids = readLines(PIDS_FILE);
		char *line;
		uint index;
		uint pid = getSelfProcessId();

		foreach(pids, line, index)
			if(pid == toValue(line))
				break;

		if(!line)
		{
			addElement(pids, (uint)xcout("%u", pid));
			writeLines(PIDS_FILE, pids);
		}
		cout("(%u) <== %u\n", getCount(pids), pid);
		return; // g
	}

	createFile(PIDS_FILE);

	while(!waitKey(0))
	{
		execute_x(xcout("\"%s\" /C", getSelfFile()));
	}
}
