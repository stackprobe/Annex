/*
	引用元
		http://keicode.com/windows/winsock-packet-capture.php
*/


#include "all.h"

//////////////////////////////////////////////////////////////////////////


WSADATA g_wsaData;


//////////////////////////////////////////////////////////////////////////


VOID PrintTCPHeader(PTCP_HEADER tcp_header) {

     if (!tcp_header) {
          SetLastError ( ERROR_INVALID_PARAMETER );
          return;
     }

     printf("Src Port: %u\n", ntohs(tcp_header->th_sport));
     printf("Dst Port: %u\n",ntohs(tcp_header->th_dport));
     printf("Sequence number: %u\n", ntohl(tcp_header->th_seq));
     printf("Acknowledgement: %u\n", ntohl(tcp_header->th_ack));
     printf("Header Length: %u\n", tcp_header->th_hl);
     // Flags
     printf("Flags:\n");
     printf("\tURG: %u\n", tcp_header->th_flags & 0x20 ? 1:0);
     printf("\tACK: %u\n", tcp_header->th_flags & 0x10 ? 1:0);
     printf("\tPSH: %u\n", tcp_header->th_flags & 0x8 ? 1:0);
     printf("\tRST: %u\n", tcp_header->th_flags & 0x4 ? 1:0);
     printf("\tSYN: %u\n", tcp_header->th_flags & 0x2 ? 1:0);
     printf("\tFIN: %u\n", tcp_header->th_flags & 0x1 ?1:0);
     
     printf("Window size: %u\n", ntohs(tcp_header->th_win));
     printf("Checksum: %u\n", ntohs(tcp_header->th_sum));
     printf("Urgent offset: %u\n", ntohs(tcp_header->th_urp));

}

int SockWait(int sock, uint millis, uint forWrite)
{
	fd_set fs;
	struct timeval tv;
	int retval;

	FD_ZERO(&fs);
	FD_SET(sock, &fs);
	tv.tv_sec = millis / 1000;
	tv.tv_usec = (millis % 1000) * 1000;

	printf("select()\n");

	retval = select(sock + 1,
		forWrite ? NULL : &fs,
		forWrite ? &fs : NULL,
		NULL,
		&tv
		);

	printf("%d\n", retval);

	if(retval < -1 || 1 < retval)
	{
		printf("Error: SockWait\n");
	}
	return retval;
}

//////////////////////////////////////////////////////////////////////////
//
// main 関数
//

void main(int argc, char* argv[])  {

     INT i, nRet, nNIC;
     char szBuff[4096], *lpNIC;
     DWORD dwBytesReturned;
     SOCKET_ADDRESS_LIST* sock_addr_list;
     SOCKADDR_IN *nic_addr, addr;
     ULONG uRCVALL_OPTION = RCVALL_ON;

     IP_HEADER ip_header;
     TCP_HEADER tcp_header;
     DWORD dwTotalHeaderLen;
     char* lpBuffer;

     // WSAStartup
     WSAStartup(MAKEWORD(2,2), &g_wsaData);
     
     // 生ソケットの作成
     SOCKET hSocket = socket(AF_INET, SOCK_RAW, IPPROTO_IP);
     if(INVALID_SOCKET == hSocket) {
          printf("socket Failed.\n");
          return;
     }

	{
		unsigned long arg = 1L;
		if(0!=ioctlsocket(hSocket,FIONBIO,&arg))
		{
			printf("ERROR: ioctlsocket FIONBIO fault.\n");
			return;
		}
	}

     // インターフェイスの列挙と選択
     nRet = WSAIoctl(
          hSocket, SIO_ADDRESS_LIST_QUERY, NULL, 0, 
          szBuff, sizeof(szBuff), &dwBytesReturned,
          NULL, NULL);

     if(WSAEFAULT == nRet) {
          printf("ERROR: WSAIoctl Failed.\n");
          return;
     }

     sock_addr_list = (SOCKET_ADDRESS_LIST*) szBuff;

     if(sock_addr_list->iAddressCount > 1) {

          //
          // モニターするネットワーク選択を促す
          //
          
          for(i=0; i<sock_addr_list->iAddressCount; i++) {
               nic_addr = (SOCKADDR_IN*) sock_addr_list->Address[i].lpSockaddr;
               lpNIC = inet_ntoa(nic_addr->sin_addr);
               printf("NIC [%02d]: %s\n", i, lpNIC);
          }

          printf("Enter NIC Number you want to monitor --> ");
          scanf_s("%d", &nNIC);
          printf("\n");
          if(nNIC < 0 || sock_addr_list->iAddressCount <= nNIC) {
               printf("Invalid number.\n");
               return;
          }
     }
     else if(1 == sock_addr_list->iAddressCount) {
          // NIC がひとつしか利用可能でない場合
          nNIC = 0;
     }
     else {
          printf("No Network Available.\n");
          return;
     }
     
     nic_addr = (SOCKADDR_IN*) sock_addr_list->Address[nNIC].lpSockaddr;
     printf("[INFO] %s\n", inet_ntoa(nic_addr->sin_addr));

     // Bind
     addr.sin_addr.s_addr = nic_addr->sin_addr.s_addr;
     addr.sin_family = AF_INET;
     addr.sin_port = htons(0);
     nRet = bind(hSocket, (SOCKADDR*) &addr, sizeof(addr));
     if(SOCKET_ERROR == nRet ) {
          printf("bind Failed. %d\n", WSAGetLastError());
          return;
     }

     // ソケットを IP パケットを受け取り可能とする
     nRet = WSAIoctl(
          hSocket, SIO_RCVALL, &uRCVALL_OPTION, sizeof(ULONG),
          NULL, 0, &dwBytesReturned, NULL, NULL);

     if(0 != nRet) {
          printf("WSAIoctl Failed. SIO_RCVALL mode setting.\n");
          return;
     }


     //
     // 受信
     //
     
     while(1) {

		for(; ; )
		{
			while(_kbhit())
			{
				printf("stdin ");

				if(_getch() == 0x1b)
				{
					printf("1\n");
					goto endRecvLoop;
				}
				printf("0\n");
			}

			if(1 <= SockWait(hSocket, 1000, 0))
				break;
		}

          dwBytesReturned = recv(hSocket, szBuff, sizeof(szBuff), 0);

          if(SOCKET_ERROR == dwBytesReturned || 0==dwBytesReturned) {
               printf("recv returned SOCKET_ERROR or closed.\n");
               break;
          }
          memmove(&ip_header, szBuff, sizeof(IP_HEADER));

          printf("---------------------------------------------------------\n");
          printf("* IP HEADER\n");
          PrintHexDump(((ip_header.ip_hl) * 4), (PBYTE) szBuff);
          //PrintIPHeader(ip_header);
          
          if(6 == ip_header.ip_p) { // TCP パケットなら中身をダンプ
          
               lpBuffer = szBuff;
               lpBuffer += ((ip_header.ip_hl) * 4);
               memmove(&tcp_header, lpBuffer, sizeof(TCP_HEADER));

               printf("* TCP HEADER\n");
               PrintHexDump(sizeof(TCP_HEADER), (PBYTE)&tcp_header);
               PrintTCPHeader(&tcp_header);

               printf("* TCP Payload\n");
               // TotalHeaderLength = (IP Header) + (TCP Header)
               dwTotalHeaderLen = ((ip_header.ip_hl) * 4) + ((tcp_header.th_hl) * 4);
               lpBuffer = szBuff;
               lpBuffer += dwTotalHeaderLen;
               PrintHexDump(
                    dwBytesReturned - dwTotalHeaderLen,
                    (PBYTE) lpBuffer);

          }

          printf("* RAW DATA\n");
          PrintHexDump(dwBytesReturned, (PBYTE) szBuff);

          
     }
endRecvLoop:

     // クリーンアップ
     closesocket(hSocket);
     WSACleanup();
     
}
