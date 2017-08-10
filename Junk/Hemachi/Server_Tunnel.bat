START C:\Factory\Labo\Socket\Tunnel.exe /FP 64999 /P 65500
START C:\Factory\Labo\Socket\crypTunnelOld.exe /FD localhost /FP 64998 /P 64999 /R /T 20 /KB *default
START C:\Factory\Labo\Socket\Tunnel.exe /FP 64997 /P 64998
START C:\Factory\Labo\Socket\Adapter.exe /P 64997 /X 3000 /TP /RES C:\Factory\Labo\Junk\Hemachi\Adapter.conf
START C:\Factory\Labo\Junk\Hemachi\Server.exe
