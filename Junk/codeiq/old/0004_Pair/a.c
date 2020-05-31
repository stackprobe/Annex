/*
	Language == C
*/

#include <stdio.h>

#define ATTEND 'O'
#define ABSENT 'X'
#define H_PAIRED 'H'
#define V_PAIRED 'V'

#define H_MAX 10
#define W_MAX 10

static int IsEven(char t[H_MAX][W_MAX + 1], int h, int w) {
	int a = 0;
	int b = 0;
	int r;
	int c;

	for(r = 0; r < h; r++)
	for(c = 0; c < w; c++)
	if(t[r][c] == ATTEND) {
		if((r + c) % 2)
			a++;
		else
			b++;
	}
	return a == b;
}
int main(void) {
	char t[H_MAX][W_MAX + 1];
	int h;
	int w;
	int i;
	int a = 1;
	int noPairs = 1;

	for(h = 0; h < H_MAX; h++)
		if(!gets(t[h]) || !*t[h])
			break;

	w = strlen(t[0]);

	if(IsEven(t, h, w))
	for(i = 0; 0 <= i; i += a) {
		int r = i / w;
		int c = i % w;

		if(h <= r) {
			if(noPairs)
				break;

			printf("yes\n");
			return 0;
		}
		if(t[r][c] == H_PAIRED)
			t[r][c + 1] = ATTEND;
		else if(t[r][c] == V_PAIRED)
			t[r + 1][c] = ATTEND;

		switch(t[r][c]) {
		case ATTEND:
			if(c < w - 1 && t[r][c + 1] == ATTEND) {
				t[r][c] = H_PAIRED;
				t[r][c + 1] = ABSENT;
				i++;
				a = 1;
				noPairs = 0;
				break;
			}
		case H_PAIRED:
			if(r < h - 1 && t[r + 1][c] == ATTEND) {
				t[r][c] = V_PAIRED;
				t[r + 1][c] = ABSENT;
				a = 1;
				noPairs = 0;
				break;
			}
		case V_PAIRED:
			t[r][c] = ATTEND;
			a = -1;
			break;
		}
	}
	printf("no\n");
	return 0;
}
