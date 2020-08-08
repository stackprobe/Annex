/*
	Tunnel.exe RECV-PORT FWD-HOST FWD-PORT /S

		/S ... í‚é~Ç∑ÇÈÅB

	Tunnel.exe RECV-PORT FWD-HOST FWD-PORT [/C CONNECT-MAX]

		CONNECT-MAX ... ç≈ëÂê⁄ë±êî, è»ó™éûÇÕ 1000
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
