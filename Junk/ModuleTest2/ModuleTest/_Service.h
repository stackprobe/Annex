typedef struct Service_st
{
	char *Name;
	char *Command;
}
Service_t;

Service_t *HTT_CreateService(char *name, char *execFile);
void HTT_ReleaseService(Service_t *i);

extern autoList<Service_t *> *ServiceList;
extern int ServiceNameLenMin;
extern int ServiceNameLenMax;

void LoadServiceFile(char *adapterFile);
Service_t *GetService(char *name);
