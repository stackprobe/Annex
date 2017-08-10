DEL 1.bat
DEL 2.bat

seq.exe "ECHO " /c 1 1 10 1 " " /c 1 1 10 1 " " /c 1 0 20 1 "| a >> 1.txt" >> 1.bat
seq.exe "ECHO " /c 1 1 10 1 " " /c 1 1 10 1 " " /c 1 0 20 1 "| b >> 2.txt" >> 2.bat

DEL 1.txt
DEL 2.txt

CALL 1.bat
CALL 2.bat

FC /B 1.txt 2.txt
rem [ëäà·ì_ÇÕñ≥Ç¢ÇÕÇ∏ÅI]
