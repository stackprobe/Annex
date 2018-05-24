#include <stdio.h>

#define ROOT_MAX 2000000000

typedef unsigned int uint;

static uint Map[ROOT_MAX / 32];
static int M;
static int N;

#define GetMap(i) \
	(Map[((i) - 1) / 32] & ((uint)1 << (((i) - 1) % 32)))

#define PutMap(i) \
	(Map[((i) - 1) / 32] |= ((uint)1 << (((i) - 1) % 32)))

static void Down(int r)
{
	if(r < 1 || GetMap(r))
		return;

	PutMap(r);

	Down(r / 2 - M);
	Down((int)(((uint)r * 2) / 3) - N);
}
int main()
{
	int r;

	scanf("%d %d %d", &r, &M, &N);

	Down(r);

	for(r = 1; GetMap(r); r++);

	printf("%d\n", r);
}
