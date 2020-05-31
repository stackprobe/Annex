#include <stdio.h>

#define SCALE_MAX 100000000

static void TryScale(int curr, int scale)
{
	int n;

	for(n = 0; n <= 10; n++)
	{
		int next = curr + n * scale;
		int ans;

		ans = (int)(((__int64)next * next) % (scale * 10));

		if(next == ans)
		{
			if(scale < SCALE_MAX)
				TryScale(next, scale * 10);
			else
				printf("%d\n", next);
		}
	}
}
int main(int argc, char **argv)
{
	TryScale(0, 1);
}
