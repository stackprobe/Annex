/*

0����9�̐������A�c�����ꂼ��N���ׂ��l�p�`������܂��B
���ォ��E���ɁA�E���邢�͉��ւƈړ����Ȃ���A���𑫂��Ă����܂��B
��������o�H�̂����A�ŏ����v�l�ƂȂ�o�H�����ǂ����ꍇ�́A���v�l�𓚂��Ă��������B

�������AN�́A2��N��20 �͈̔͂̐����Ƃ��܂��B<--- 20 ???????

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
