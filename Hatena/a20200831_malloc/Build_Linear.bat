CALL Clean.bat

CL /Ox /c MYLIB_malloc_Linear.c
CL /Ox t0001.c MYLIB_malloc_Linear.obj
CL /Ox t0002.c MYLIB_malloc_Linear.obj
