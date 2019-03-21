/*
	----
	2019.3.21

	Windows 10 Pro 日本語, Visual Studio 2010 Express を使用

	1. このコードを CL printf_problem.c でコンパイルする。
	2. printf_problem.exe を実行すると 4100 文字目あたりで表示が乱れる。

		こんな感じ　⇒　<br/>ファイル<br/><br/>ファ・C

	リダイレクトすると問題無い。
*/

#include <stdio.h>
#include <string.h>

int main(int argc, char **argv)
{
	static char text[18001];
	int i;

	for(i = 0; i < 1000; i++)
	{
		strcpy(text + i * 18, "<br/>ファイル<br/>");
	}
	printf("%s", text);
}
