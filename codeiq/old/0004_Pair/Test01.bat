FOR %%C IN (0, 1, 2, 3, 4, 5, 6) DO (
	a.exe < sample\%%C.in.txt > out.tmp
	TYPE sample\%%C.out.txt
	TYPE out.tmp
)

PAUSE
