C:\Factory\Tools\RDMD.exe /RC out

COPY KCamera\KCamera\bin\Release\*.exe out
COPY KCamera\KCamera\bin\Release\*.dll out
COPY KCamera\KCamera\bin\Release\*.xml out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out KCamera

IF NOT "%1" == "/-P" PAUSE
