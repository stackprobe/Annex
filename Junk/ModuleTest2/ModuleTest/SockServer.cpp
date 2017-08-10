#include "all.h"

#define BUFFSIZE 1000000

#define IP_FILE "IP.httdat"
#define RECV_FILE "Recv.httdat"
#define SEND_FILE "Send.httdat"

typedef struct Connect_st
{
	int Sock;
	char *WorkDir;
	char *IP;
	time_t NextServiceExecTime;
	Service_t *Service;
}
Connect_t;

static void *Buffer;
static autoList<Connect_t *> *ConnectList;
static int StopServerEvent;
static char *ServiceName;
static int SockSignaled;
static int SockWaitMillisCount;
static int SockWaitMillis;

static void Disconnect(Connect_t *c)
{
	shutdown(c->Sock, SD_BOTH);
	closesocket(c->Sock);

	forceRemoveDir(c->WorkDir);
	DecrementIPCount(c->IP);

	memFree(c->WorkDir);
	memFree(c->IP);
	memFree(c);
}
static void DeleteFileBegin(char *file, int delSize)
{
	__int64 size = getFileSize(file);

	if(size == delSize)
	{
		createFile(file);
		return;
	}
	FILE *rfp = fileOpen(file, "rb");
	FILE *wfp = fileOpen(file, "r+b");

	fileSeek(rfp, SEEK_SET, delSize);

	for(__int64 remain = size - delSize; 0 < remain; )
	{
		int rwSize = (int)m_min(remain, BUFFSIZE);

		fileRead(rfp, Buffer, rwSize);
		fileWrite(wfp, Buffer, rwSize);

		remain -= rwSize;
	}
	fileClose(rfp);
	fileClose(wfp);

	setFileSize(file, size - delSize);
}
static int Transmit(Connect_t *c) // ret: ? 切断
{
	int size = SockTransmit(c->Sock, Buffer, BUFFSIZE, SockWaitMillis, 0);

	if(size == -1) // ? 切断 || エラー
		return 1;

	addCwd(c->WorkDir);
	int retval = 0;

	if(size)
	{
		writeBlock(RECV_FILE, Buffer, size, 1);
		c->NextServiceExecTime = -1;
		SockSignaled = 1;
	}
	if(1 <= getFileSize(SEND_FILE))
	{
		__int64 sendFSz = getFileSize(SEND_FILE);

		size = (int)m_min(sendFSz, BUFFSIZE);
		readBlock(SEND_FILE, Buffer, size);
		size = SockTransmit(c->Sock, Buffer, size, SockWaitMillis, 1);

		if(size == -1) // ? 切断 || エラー
		{
			retval = 1;
			goto endFunc;
		}
		if(size)
		{
			DeleteFileBegin(SEND_FILE, size);
			c->NextServiceExecTime = -1;
			SockSignaled = 1;
		}
	}
	if(c->NextServiceExecTime < 0 || c->NextServiceExecTime < now())
	{
		if(!c->Service)
		{
			__int64 recvFSz = getFileSize(RECV_FILE);

			if(ServiceNameLenMin <= recvFSz)
			{
				size = (int)m_min(recvFSz, ServiceNameLenMax + 1);
				readBlock(RECV_FILE, ServiceName, size);

				for(int index = 0; index < size; index++)
				{
					int chr = ServiceName[index];

					if('\x21' <= chr && chr <= '\x7e') // ? サービス名に使用できる文字
					{
						1; // noop
					}
					else if(chr == '\x20') // ? サービス名を閉じる文字
					{
						ServiceName[index] = '\0';
						c->Service = GetService(ServiceName);

						if(!c->Service) // ? not found
						{
							retval = 1;
							goto endFunc;
						}
						goto connected;
					}
					else // ? 不正な文字
					{
						retval = 1;
						goto endFunc;
					}
				}
				if(ServiceNameLenMax + 1 <= recvFSz)
				{
					retval = 1;
					goto endFunc;
				}
			}
			if(getFileWriteTime(IP_FILE) + (GetResStatus() == RS_GREEN ? 60L : 2L) < time(NULL))
			{
				retval = 1;
				goto endFunc;
			}
		}
		else
		{
connected:
			system(c->Service->Command);

			if(!existFile(SEND_FILE))
			{
				retval = 1;
				goto endFunc;
			}
			if(!existFile(IP_FILE))
				writeLine(IP_FILE, "Why did you kill me?"); // 2bs

			if(!existFile(RECV_FILE))
				createFile(RECV_FILE); // 2bs
		}
		c->NextServiceExecTime = now() + 2;
	}
endFunc:
	unaddCwd();
	return retval;
}
void SockServer(int portno)
{
	Buffer = memAlloc(BUFFSIZE);
	ConnectList = new autoList<Connect_t *>();
	StopServerEvent = eventOpen(EVENT_STOPSERVER);
	ServiceName = (char *)memAlloc(ServiceNameLenMax + 1);
	SockSignaled = 0;
	SockWaitMillisCount = 0;
	SockWaitMillis = 0;

	SockStartup();

	int sock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP);
	errorCase(sock == -1); // ? 失敗

	struct sockaddr_in sa;
	memset(&sa, 0x00, sizeof(sa));
	sa.sin_family = AF_INET;
	sa.sin_addr.s_addr = htonl(INADDR_ANY);
	sa.sin_port = htons((unsigned short)portno);

	int retval = bind(sock, (struct sockaddr *)&sa, sizeof(sa));
	errorCase(retval != 0); // ? 失敗

	retval = listen(sock, SOMAXCONN);
	errorCase(retval != 0); // ? 失敗

	time_t nextPeriodExecTime = -1;

	while(!waitForMillis(StopServerEvent, 0) && !isHitKey(0x1b)) // ? not 停止
	{
		int delaied = 0;

		switch(GetResStatus())
		{
		case RS_GREEN:
			if(SockWait(sock, ConnectList->GetCount() ? 0 : (delaied = 1, 2000), 0)) // ? 接続あり
			{
				struct sockaddr_in clsa;
				int sasz = sizeof(clsa);
				int clSock = accept(sock, (struct sockaddr *)&clsa, &sasz);
				errorCase(clSock == -1); // ? 失敗

				char *dir = makeTempDir();
				addCwd(dir);
				char *ip = strx(inet_ntoa(clsa.sin_addr));
				writeLine(IP_FILE, ip);
				createFile(RECV_FILE);
				createFile(SEND_FILE);
				unaddCwd();

				Connect_t *c = nb(Connect_t);

				c->Sock = clSock;
				c->WorkDir = dir;
				c->IP = ip;

				IncrementIPCount(ip);

				if(GetIPCount(ip) <= 100)
				{
					ConnectList->AddElement(c);
				}
				else
				{
					Disconnect(c);
				}
			}
			// fall through

		case RS_YELLOW:
			for(int index = 0; index < ConnectList->GetCount() && GetResStatus() != RS_RED; index++)
			{
				SockWaitMillis = 0;

				if(!delaied)
				{
					if(SockSignaled)
					{
						SockSignaled = 0;
						SockWaitMillisCount = 0;
					}
					else
					{
						SockWaitMillisCount++;
						m_minim(SockWaitMillisCount, 100);
						SockWaitMillis = SockWaitMillisCount;
					}
					delaied = 1;
				}
				if(Transmit(ConnectList->GetElement(index))) // ? 切断
				{
					Disconnect(ConnectList->FastDesertElement(index));
					index--;
				}
			}
			break;

		case RS_RED:
			while(ConnectList->GetCount())
			{
				Disconnect(ConnectList->UnaddElement());
			}
			break;
		}
		if(nextPeriodExecTime < now()) // ? 定期的なサービスの空実行
		{
			char *dir = makeTempDir();
			addCwd(dir);

			for(int index = 0; index < ServiceList->GetCount(); index++)
			{
				Service_t *s = ServiceList->GetElement(index);

				createFile(IP_FILE);
				createFile(RECV_FILE);
				createFile(SEND_FILE);

				system(s->Command);

				clearDir(".");
			}
			unaddCwd();
			removeDir(dir);
			memFree(dir);

			nextPeriodExecTime = now() + 60;
		}
		if(!delaied)
		{
			Sleep(2000);
		}
	}
	while(ConnectList->GetCount())
	{
		Disconnect(ConnectList->UnaddElement());
	}
	retval = closesocket(sock);
	errorCase(retval != 0);

	SockCleanup();

	memFree(Buffer);
	delete ConnectList;
	handleClose(StopServerEvent);
	memFree(ServiceName);
}
