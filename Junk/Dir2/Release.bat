C:\Factory\Tools\RDMD.exe /RC out

COPY Dir2\Dir2\bin\Release\Dir2.exe out
COPY Dir2\Dir2Tools\bin\Release\Dir2Tools.exe out
COPY Dir2\AddFilePart\bin\Release\AddFilePart.exe out
COPY Dir2\GetFilePart\bin\Release\GetFilePart.exe out
COPY Dir2\SetFileTime\bin\Release\SetFileTime.exe out
C:\Factory\Tools\xcp.exe doc out

rem C:\Factory\Tools\zcp.exe out C:\app\Kit\Dir2
C:\Factory\SubTools\zip.exe /O out Dir2
rem COPY out\Dir2.zip S:\_kit
rem C:\Factory\SubTools\nrun.exe /s mimiko syncKit
rem COPY out\Dir2.zip S:\_hidden

PAUSE
