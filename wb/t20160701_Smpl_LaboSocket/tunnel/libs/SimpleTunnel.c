/*
	Tunnel.exe RECV-PORT FWD-HOST FWD-PORT /S

		/S ... ��~����B

	Tunnel.exe RECV-PORT FWD-HOST FWD-PORT [/C CONNECT-MAX]

		CONNECT-MAX ... �ő�ڑ���, �ȗ����� 1000
*/

#include "C:\Factory\Labo\Socket\tunnel\libs\Tunnel.h"

static void Perform(int sock, int fwdSock)
{
	CrossChannel(sock, fwdSock, NULL, 0, NULL, 0);
}
static int ReadArgs(void)
{
	return 0;
}
int main(int argc, char **argv)
{
	TunnelMain(ReadArgs, Perform, "Tunnel", NULL);
}
