C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe C:\Dat\Resource out\Resource.dat C:\Dev\Fairy\Tools\MaskGZDataForDonut3\MaskGZData.exe

C:\Factory\SubTools\CallConfuserCLI.exe GGGGTMPL\GGGGTMPL\bin\Release\GGGGTMPL.exe out\GGGGTMPL.exe
rem COPY /B GGGGTMPL\GGGGTMPL\bin\Release\GGGGTMPL.exe out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\Chocolate.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLib.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLib_x64.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
C:\Factory\Tools\xcp.exe C:\Dev\Fairy\Donut3\doc out

C:\Factory\SubTools\zip.exe /PE- /RVE- /G out GGGGTMPL
C:\Factory\Tools\summd5.exe /M out

IF NOT "%1" == "/-P" PAUSE
