C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe Resource out\Resource.dat

COPY /B GGGGTMPL\GGGGTMPL\bin\Release\GGGGTMPL.exe out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\Chocolate.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLib.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLib_x64.dll out
COPY /B GGGGTMPL\GGGGTMPL\bin\Release\DxLibDotNet.dll out

C:\Factory\SubTools\zip.exe /G out GGGGTMPL
C:\Factory\Tools\summd5.exe /M out

PAUSE
