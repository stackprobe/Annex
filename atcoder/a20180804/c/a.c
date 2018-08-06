#include "C:\Factory\Common\all.h"

static char Rows[2000][2001];
static int H;
static int W;

static int Advance(int r, int c, int ra, int ca)
{
	int ans = 0;

	for(; ; )
	{
		r += ra;
		c += ca;

		if(r < 0 || H <= r || c < 0 || W <= c || Rows[r][c] == '#')
			break;

		ans++;
	}
	return ans;
}
int main(int argc, char **argv)
{
	int r;
	int c;
	__int64 ans = 0;

	scanf("%d", &H); // n
	scanf("%d", &W); // m

	for(r = 0; r < H; r++)
		scanf("%s", Rows[r]);

	for(r = 0; r < H; r++)
	for(c = 0; c < W; c++)
	{
		if(Rows[r][c] == '.')
		{
			int ec = Advance(r, c,  0,  1);
			int wc = Advance(r, c,  0, -1);
			int sc = Advance(r, c,  1,  0);
			int nc = Advance(r, c, -1,  0);

			ans += ec * nc;
			ans += nc * wc;
			ans += wc * sc;
			ans += sc * ec;
		}
	}
	cout("%I64d\n", ans);
}
