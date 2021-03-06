/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double GetDistance(double x, double y)
{
	return sqrt(x * x + y * y);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double GetDistance(double x1, double y1, double x2, double y2)
{
	return GetDistance(x1 - x2, y1 - y2);
}

// もしかして... getAngle, angleToXY, angleMoveXY, rotatePos

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MakeXYSpeed(double x, double y, double destX, double destY, double speed, double &speedX, double &speedY, double distanceMin)
{
	speedX = destX - x;
	speedY = destY - y;

	double distance = GetDistance(speedX, speedY);

	if(distance < distanceMin)
	{
		speedX = speed;
		speedY = 0;
	}
	else
	{
		double rate = speed / distance;

		speedX = speedX * rate;
		speedY = speedY * rate;
	}
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Circle(
	double x1, double y1, double rCir1,
	double x2, double y2, double rCir2
	)
{
	return GetDistance(x1, y1, x2, y2) < rCir1 + rCir2;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Point(
	double x1, double y1, double rCir,
	double x2, double y2
	)
{
	return GetDistance(x1, y1, x2, y2) < rCir;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Circle_Rect(
	double x, double y, double rCir,
	double l, double t, double r, double b
	)
{
	if(x < l) // 左
	{
		if(y < t) // 左上
		{
			return IsCrashed_Circle_Point(x, y, rCir, l, t);
		}
		else if(b < y) // 左下
		{
			return IsCrashed_Circle_Point(x, y, rCir, l, b);
		}
		else // 左中段
		{
			return l < x + rCir;
		}
	}
	else if(r < x) // 右
	{
		if(y < t) // 右上
		{
			return IsCrashed_Circle_Point(x, y, rCir, r, t);
		}
		else if(b < y) // 右下
		{
			return IsCrashed_Circle_Point(x, y, rCir, r, b);
		}
		else // 右中段
		{
			return x - rCir < r;
		}
	}
	else // 真上・真ん中・真下
	{
		return t - rCir < y && y < b + rCir;
	}
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Rect_Point(
	double l, double t, double r, double b,
	double x, double y
	)
{
	return
		l < x && x < r &&
		t < y && y < b;
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int IsCrashed_Rect_Rect(
	double l1, double t1, double r1, double b1,
	double l2, double t2, double r2, double b2
	)
{
	return
		l1 < r2 && l2 < r1 &&
		t1 < b2 && t2 < b1;
}
