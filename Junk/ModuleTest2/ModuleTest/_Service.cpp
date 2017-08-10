#include "all.h"

Service_t *HTT_CreateService(char *name, char *command)
{
	errorCase(m_isEmpty(name));
	errorCase(m_isEmpty(command));

	Service_t *i = nb(Service_t);

	i->Name = strx(name);
	i->Command = strx(command);

	return i;
}
void HTT_ReleaseService(Service_t *i)
{
	memFree(i->Name);
	memFree(i->Command);
	memFree(i);
}

autoList<Service_t *> *ServiceList;
int ServiceNameLenMin;
int ServiceNameLenMax;

#define SERVICENAMELENMAXMAX 1000

static int CompServiceName(Service_t *i1, Service_t *i2)
{
	return _stricmp(i1->Name, i2->Name);
}
void LoadServiceFile(char *serviceFile)
{
	char *baseDir = getDir(serviceFile);
	FILE *fp = fileOpen(serviceFile, "rt");

	ServiceList = new autoList<Service_t *>();
	ServiceNameLenMin = SERVICENAMELENMAXMAX;
	ServiceNameLenMax = 0;

	for(; ; )
	{
		char *line = readLine(fp);

		if(!line)
			break;

		if(*line != ';') // ? not comment
		{
			char *p = strchr(line, ' ');
			errorCase(!p);
			*p++ = '\0';

			int len = strlen(line);
			m_minim(ServiceNameLenMin, len);
			m_maxim(ServiceNameLenMax, len);

			if(!strncmp(p, ".\\", 2))
				p = combine_xc(getFullPath(baseDir), p + 2);
			else
				p = strx(p);

			{
				char *ESCPTN = "\n";
				char *basePath = getDir_x(getFullPath(serviceFile));
				basePath = combine_xc(basePath, ""); // "X:\\" -> "X:\\", "X:\\abc" -> "X:\\abc\\"

				p = replace(p, "%%", ESCPTN);
				p = replace(p, "%BASE%", basePath, 1);
				p = replace(p, ESCPTN, "%");

				memFree(basePath);
			}

			ServiceList->AddElement(HTT_CreateService(line, p));
			memFree(p);
		}
		memFree(line);
	}
	fileClose(fp);
	memFree(baseDir);

	errorCase(!ServiceList->GetCount()); // ? no services
	errorCase(ServiceNameLenMin < 1);
	errorCase(SERVICENAMELENMAXMAX < ServiceNameLenMax);
	errorCase(ServiceNameLenMax < ServiceNameLenMin);

	ServiceList->Sort(CompServiceName);

	for(int index = 1; index < ServiceList->GetCount(); index++) // サービス名の重複チェック
	{
		errorCase(!CompServiceName(
			ServiceList->GetElement(index - 1),
			ServiceList->GetElement(index)
			));
	}
}
Service_t *GetService(char *name)
{
	errorCase(!name);
	static Service_t ferret;
	ferret.Name = name;

	int index = ServiceList->BinSearch(CompServiceName, &ferret);

	if(index == -1) // ? not found
		return NULL;

	return ServiceList->GetElement(index);
}
