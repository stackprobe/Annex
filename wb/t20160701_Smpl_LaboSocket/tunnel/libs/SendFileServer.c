/*
	SendFileServer.exe [/TRUST] [/P RECV-PORT] ROOT-DIR [/C CONNECT-MAX] [/T SOCK-TIMEOUT]

		RECV-PORT    ... �|�[�g�ԍ�, �f�t�H == 60022
		ROOT-DIR     ... ���[�g�f�B���N�g��
		CONNECT-MAX  ... �ő�ڑ���, �f�t�H == 1000
		SOCK-TIMEOUT ... �^�C���A�E�g [�b] �f�t�H == 1��

		���ulineToFairLocalPath()�ŕύX�����悤�ȃ��[�J�����v���܂ރp�X�͎g�p�ł��Ȃ��B

	SendFileServer.exe [/P RECV-PORT] /S

		��~����B

	���h���C�u�E�������̋󂫗̈�̃`�F�b�N�Ȃǂ��Ă��Ȃ��B
	�O�Ɍ��J����Ƃ��͒��ӁI
*/

#include "C:\Factory\Common\Options\SockServerTh.h"
#include "C:\Factory\Common\Options\SockStream.h"
#include "SendFileCommon.h"

#define STOPEVENTUUID "{1182ccea-90e1-49c5-83f6-35ceefb7ebca}"

static char *RootDir;
static uint PortNo = DEF_PORTNO;
static uint ConnectMax = 10;
static uint SockTimeoutSec = 86400;
static int AcceptParentDirFlag;

static char *StopEventName;
static uint StopEventHdl;
static int StopServerRq;

#define COMMUNICATION_ERROR \
	{ \
		cout("COMMUNICATION_ERROR(%u)\n", __LINE__); \
		goto communicationError; \
	}

/*
	rootDir: �M���ł���t���p�X
	untPath: �M�p�ł��Ȃ��p�X������

	ret: �������ꂽ�t���p�X, NULL == ���s

	�t���p�X == �h���C�u���t���E��΃p�X
*/
static char *ToFullPathEx_cx(char *rootDir, char *untPath)
{
	char *retPath;
	autoList_t *ptkns;
	char *ptkn;
	uint ptknidx;

	/*
		�ʓ|�Ȃ̂� "C:xxx..." �Ƃ� "\\xxx..." �͍l�����Ȃ��B
	*/
	if(m_isalpha(untPath[0]) && untPath[1] == ':' && (untPath[2] == '/' || untPath[2] == '\\')) // ? �h���C�u���t���E��΃p�X
	{
		retPath = untPath;
	}
	else // ? �h���C�u�������E���΃p�X
	{
		retPath = combine_cx(rootDir, untPath);
	}

	escapeYen(retPath);
	ptkns = tokenize_x(retPath, '/');

	if(getCount(ptkns) == 1)
	{
		addElement(ptkns, (uint)strx("_"));
	}
	errorCase(getCount(ptkns) < 2);

	/*
		�ʓ|�Ȃ̂� "." �� ".." �͍l�����Ȃ��B
	*/
	foreach(ptkns, ptkn, ptknidx)
	{
		if(ptknidx == 0) // ? �h���C�u��
		{
			errorCase(!lineExp("<1,AZaz>:", ptkn)); // 2bs
		}
		else
		{
			ptkn = lineToFairLocalPath_x(ptkn, 0);
			setElement(ptkns, ptknidx, (uint)ptkn);
		}
	}
	retPath = untokenize_xc(ptkns, "\\");

	if(PATH_SIZE < strlen(retPath))
	{
		LOGPOS();

		memFree(retPath);
		retPath = combine(rootDir, "_");

		if(PATH_SIZE < strlen(retPath))
		{
			LOGPOS();

			memFree(retPath);
			retPath = xcout("%c:\\_", rootDir[0]);
		}
	}
	cout("retPath: %s\n", retPath);
	return retPath;
}
static int TryCreateParent(char *path) // path: ���S�ȃt���p�X(�񃋁[�gDIR), ret: ? ���� || ���ɑ��݂���
{
	path = changeLocal(path, "");

	if(isAbsRootDir(path))
		return 1;

	if(existDir(path))
		return 1;

	if(existFile(path))
		return 0;

	if(!TryCreateParent(path))
		return 0;

	cout("MD %s\n", path);
	createDir(path);
	return 1;
}
static void PerformTh(int sock, char *strip)
{
	SockStream_t *ss = CreateSockStream(sock, SockTimeoutSec);
	char *signature;
	char *mode = NULL;
	char *file = NULL;
	int forWrite;

	signature = SockRecvLine(ss, strlen(SIGNATURE) + 1);

	if(strcmp(signature, SIGNATURE)) // ? �s��v
		COMMUNICATION_ERROR

	mode = SockRecvLine(ss, m_max(strlen(S_MODE_R), strlen(S_MODE_W)) + 1);
	file = SockRecvLine(ss, PATH_SIZE + 1);

	file = ToFullPathEx_cx(RootDir, file);

	if(!AcceptParentDirFlag)
	{
		char *pBase = strx(RootDir);

		pBase = addChar(pBase, '\\');
		trimPath(pBase);

		if(!startsWith(file, pBase)) // ? file �� RootDir �̔z���ł͂Ȃ��B
		{
			memFree(pBase);
			COMMUNICATION_ERROR
		}
		memFree(pBase);
	}

	if(!strcmp(mode, S_MODE_R))
		forWrite = 0;
	else if(!strcmp(mode, S_MODE_W))
		forWrite = 1;
	else
		COMMUNICATION_ERROR

	if(!forWrite) // �ǂݍ���
	{
		FILE *fp;

		if(!existFile(file))
			COMMUNICATION_ERROR

		{
			uint64 fdSize = getFileSize(file);
			char *sFDSize;

			sFDSize = xcout("%I64u", fdSize);
			SockSendLine(ss, sFDSize);
			memFree(sFDSize);
		}

		fp = fileOpen(file, "rb");

		for(; ; )
		{
			int chr = readChar(fp);

			if(chr == EOF)
				break;

			SockSendChar(ss, chr);
		}
		fileClose(fp);
		SockSendLine(ss, S_ENDFILEDATA);
	}
	else // �����o��
	{
		uint64 fdSize;
		FILE *fp;
		uint64 fdIndex;

		if(!TryCreateParent(file))
			COMMUNICATION_ERROR

		{
			char *sFDSize = SockRecvLine(ss, 20);

			fdSize = toValue64(sFDSize);
			memFree(sFDSize);
		}

		fp = fileOpen(file, "wb");

		for(fdIndex = 0; fdIndex < fdSize; fdIndex++)
		{
			int chr = SockRecvChar(ss);

			writeChar(fp, chr);
		}
		fileClose(fp);

		{
			char *buff = SockRecvLine(ss, strlen(S_ENDFILEDATA) + 1);

			if(strcmp(buff, S_ENDFILEDATA)) // ? �s��v
			{
				memFree(buff);
				COMMUNICATION_ERROR
			}
			memFree(buff);
		}
		SockSendLine(ss, S_ENDRECV);
	}

communicationError:
	ReleaseSockStream(ss);
	memFree(signature);
	memFree(file);
	memFree(mode);
}
static int IdleTh(void)
{
	while(hasKey())
		if(getKey() == 0x1b) // ? �G�X�P�[�v�L�[���� -> ��~�v��
			StopServerRq = 1;

	if(handleWaitForMillis(StopEventHdl, 0)) // ? ��~�v��
		StopServerRq = 1;

	if(!StopServerRq)
		return 1;

	cout("��~���܂�...\n");
	return 0;
}
int main(int argc, char **argv)
{
	if(argIs("/TRUST"))
	{
		AcceptParentDirFlag = 1;

		cout("################\n");
		cout("## TRUST MODE ##\n");
		cout("################\n");
	}
	if(argIs("/P"))
	{
		PortNo = toValue(nextArg());
	}

	RootDir = nextArg();

	errorCase(m_isEmpty(RootDir));
	errorCase(!PortNo || 0xffff < PortNo);

	RootDir = makeFullPath_x(RootDir);

	errorCase(!existDir(RootDir));

	StopEventName = xcout(STOPEVENTUUID ".%u", PortNo);
	StopEventHdl = eventOpen(StopEventName);

	if(argIs("/S"))
	{
		eventWakeupHandle(StopEventHdl);
		return;
	}
	if(argIs("/C"))
	{
		ConnectMax = toValue(nextArg());
	}
	if(argIs("/T"))
	{
		SockTimeoutSec = toValue(nextArg());
	}
	errorCase(hasArgs(1)); // ? �s���ȃI�v�V����

	sockServerTh(PerformTh, PortNo, ConnectMax, IdleTh);

	handleClose(StopEventHdl);
}
