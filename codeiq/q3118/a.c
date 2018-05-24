/*

0から9の整数を、縦横それぞれN個並べた四角形があります。
左上から右下に、右あるいは下へと移動しながら、数を足していきます。
複数ある経路のうち、最小合計値となる経路をたどった場合の、合計値を答えてください。

ただし、Nは、2≦N≦20 の範囲の整数とします。<--- 20 ???????

*/

#include <stdio.h>
#include <string.h>

#define N_MAX 1000
// #define N_MAX 20

#define GetMin(a, b) \
	((a) < (b) ? (a) : (b))

int main()
{
	static char line[N_MAX + 1];
	static int sums[N_MAX];
	int n;
	int x;
	int y;

	scanf("%s", line);
	n = strlen(line);
	sums[0] = line[0] - '0';

	for(x = 1; x < n; x++)
		sums[x] = sums[x - 1] + line[x] - '0';

	for(y = 1; y < n; y++)
	{
		scanf("%s", line);
		sums[0] += line[0] - '0';

		for(x = 1; x < n; x++)
			sums[x] = GetMin(sums[x], sums[x - 1]) + line[x] - '0';
	}
	printf("%d", sums[n - 1]);
}
