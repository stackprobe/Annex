C:\Factory\Tools\RDMD.exe /RC out

COPY WCluster\WCluster\bin\Release\WCluster.exe out
COPY C:\Factory\Tools\ZCluster.exe out
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\ZCluster.exe
REN out\ZCluster.exe ZCluster.exe_
C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out WCluster

PAUSE
