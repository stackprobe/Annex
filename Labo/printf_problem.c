/*
	----
	2019.3.21

	Windows 10 Pro ���{��, Visual Studio 2010 Express ���g�p

	1. ���̃R�[�h�� CL printf_problem.c �ŃR���p�C������B
	2. printf_problem.exe �����s����� 4100 �����ڂ�����ŕ\���������B

		����Ȋ����@�ˁ@<br/>�t�@�C��<br/><br/>�t�@�EC

	���_�C���N�g����Ɩ�薳���B
*/

#include <stdio.h>
#include <string.h>

int main(int argc, char **argv)
{
	static char text[18001];
	int i;

	for(i = 0; i < 1000; i++)
	{
		strcpy(text + i * 18, "<br/>�t�@�C��<br/>");
	}
	printf("%s", text);
}
