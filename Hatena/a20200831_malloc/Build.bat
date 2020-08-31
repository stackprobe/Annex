CALL Clean.bat

CL /Ox /c MYLIB_malloc.c
CL /Ox t0001.c MYLIB_malloc.obj
CL /Ox t0002.c MYLIB_malloc.obj
