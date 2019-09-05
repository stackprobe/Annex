C:\Factory\Tools\RDMD.exe /RC out

COPY C:\Factory\SubTools\Chroco.exe out
COPY Tools\List.exe out

C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\Chroco.exe
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\List.exe

COPY C:\Factory\Resource\CP932.txt out
COPY C:\Factory\Resource\JIS0208.txt out

C:\Factory\Tools\xcp.exe doc out

C:\Factory\SubTools\zip.exe /O out Chroco

PAUSE
