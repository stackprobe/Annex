C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe Resource out\Resource.dat Tools\MaskGZData.exe

C:\Factory\SubTools\CallConfuserCLI.exe MakeMovie\MakeMovie\bin\Release\MakeMovie.exe out\MakeMovie.exe
rem COPY /B MakeMovie\MakeMovie\bin\Release\MakeMovie.exe out
COPY /B MakeMovie\MakeMovie\bin\Release\Chocolate.dll out
COPY /B MakeMovie\MakeMovie\bin\Release\DxLib.dll out
COPY /B MakeMovie\MakeMovie\bin\Release\DxLib_x64.dll out
COPY /B MakeMovie\MakeMovie\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
C:\Factory\Tools\xcp.exe C:\Dev\Fairy\Donut2\doc out

C:\Factory\SubTools\zip.exe /PE- /RVE- /G out MakeMovie
C:\Factory\Tools\summd5.exe /M out

IF NOT "%1" == "/-P" PAUSE
