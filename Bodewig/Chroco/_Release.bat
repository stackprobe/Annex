C:\Factory\Tools\RDMD.exe /RC out

COPY C:\Factory\SubTools\Chroco.exe out
COPY Tools\ofiles.exe out

C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\Chroco.exe
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\ofiles.exe

COPY C:\Factory\Resource\CP932.txt out
COPY C:\Factory\Resource\JIS0208.txt out

MD out\MkList

COPY MkList\MkList\bin\Release\*.exe out\MkList
COPY MkList\MkList\bin\Release\*.dll out\MkList

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out Chroco

IF NOT "%1" == "/-P" PAUSE
