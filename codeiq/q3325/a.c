#include <stdio.h>
#include <math.h>

// VC++ はデフォで M_PI が無い。

#define PI 3.14159265358979323846

#define HCOMB_X_SZ_MAX 5
#define HCOMB_Y_SZ 5

typedef int HComb_t[HCOMB_Y_SZ][HCOMB_X_SZ_MAX]; // [y][x]

static int HCombXSzs[] = { 3, 4, 5, 4, 3 };

static double HCombYPosStep;

static double GetHCombYPos(int y)
{
	return HCombYPosStep * y;
}
static double GetHCombXPos(int y, int x)
{
	return abs(y - 2) * 0.5 + x;
}
static void LineToHComb(char *p, HComb_t hc)
{
	int x;
	int y;

	for(y = 0; y < HCOMB_Y_SZ; y++)
	{
		for(x = 0; x < HCombXSzs[y]; x++)
		{
			hc[y][x] = *p++ == '1';
		}
		p++; // skip '/'
	}
}
static void HCombToLine(HComb_t hc, char *p)
{
	int x;
	int y;

	for(y = 0; y < HCOMB_Y_SZ; y++)
	{
		for(x = 0; x < HCombXSzs[y]; x++)
		{
			*p++ = hc[y][x] ? '1' : '0';
		}
		*p++ = '/';
	}
	p[-1] = '\0';
}

static double RotX;
static double RotY;

static void Rotate(double rad)
{
	double x = RotX;
	double y = RotY;

	RotX = x * cos(rad) - y * sin(rad);
	RotY = x * sin(rad) + y * cos(rad);
}

static double RotOrgX;
static double RotOrgY;

static int IsNearPos(double x1, double y1, double x2, double y2)
{
	return fabs(x1 - x2) < 0.1 && fabs(y1 - y2) < 0.1; // マージン適当...
}
static int XYPosToXY(double xPos, double yPos, int *p_nX, int *p_nY)
{
	int x;
	int y;

	for(y = 0; y < HCOMB_Y_SZ; y++)
	for(x = 0; x < HCombXSzs[y]; x++)
	if(IsNearPos(xPos, yPos, GetHCombXPos(y, x), GetHCombYPos(y)))
	{
		*p_nX = x;
		*p_nY = y;
		return 1;
	}
	return 0;
}
static int RotHComb(HComb_t src, HComb_t ans, double rad)
{
	int x;
	int y;
	double xPos;
	double yPos;
	int nX;
	int nY;

	for(y = 0; y < HCOMB_Y_SZ; y++)
	for(x = 0; x < HCombXSzs[y]; x++)
		ans[y][x] = 0;

	for(y = 0; y < HCOMB_Y_SZ; y++)
	for(x = 0; x < HCombXSzs[y]; x++)
	if(src[y][x])
	{
		xPos = GetHCombXPos(y, x);
		yPos = GetHCombYPos(y);

		// rotate
		RotX = xPos - RotOrgX;
		RotY = yPos - RotOrgY;
		Rotate(rad);
		xPos = RotX + RotOrgX;
		yPos = RotY + RotOrgY;
		// rotate end

		if(!XYPosToXY(xPos, yPos, &nX, &nY))
			return 0;

		ans[nY][nX] = 1;
	}
	return 1;
}
int main()
{
	char line[100];
	int origin;
	HComb_t src;
	HComb_t ans;

	HCombYPosStep = sqrt(3.0 / 4.0);

	scanf("%s", line);
	origin = line[0];
	scanf("%s", line);

	LineToHComb(line, src);

	if(origin == 'a')
	{
		RotOrgX = 1.0;
		RotOrgY = 2.0 * HCombYPosStep;
	}
	else // 'b'
	{
		RotOrgX = 2.5;
		RotOrgY = (1.0 + 2.0 / 3.0) * HCombYPosStep;
	}
	if(RotHComb(src, ans, PI * (2.0 / 3.0)))
		HCombToLine(ans, line);
	else
		strcpy(line, "-");

	printf("%s\n", line);
}
