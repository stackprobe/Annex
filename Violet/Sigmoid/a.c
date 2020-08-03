#include "C:\Factory\Common\all.h"
#include "C:\Factory\SubTools\libs\bmptbl.h"

#define E 2.718281828459

#define BMP_WH 1000
#define LINE_SPAN 250

#define COLOR_BACK 0xffffff
#define COLOR_LINE 0x808080
#define COLOR_PLOT 0x0000ff

#define X_MIN -5.0
#define X_MAX  5.0

#define Y_MIN 0.0
#define Y_MAX 1.0

#define PLOT_MAX 200

#define LINE_PLOT_MAX 300

static uint GetColorBack(void)
{
	return COLOR_BACK;
}
static void DrawLine(autoTable_t *bmp, double x1, double y1, double x2, double y2)
{
	uint c;

	cout("%f, %f -> %f, %f\n", x1, y1, x2, y2);

	for(c = 0; c <= LINE_PLOT_MAX; c++)
	{
		double rate = (double)c / LINE_PLOT_MAX;
		double x;
		double y;
		sint ix;
		sint iy;

		x = rate * (x2 - x1) + x1;
		y = rate * (y2 - y1) + y1;

		ix = d2i(x);
		iy = d2i(y);

		m_range(ix, 0, BMP_WH - 1); // todo ???
		m_range(iy, 0, BMP_WH - 1); // todo ???

		putTableCell(bmp, ix, iy, COLOR_PLOT);
	}
}
static double GetSigmoid(double gain, double x)
{
	return 1.0 / (1.0 + pow(E, -gain * x));
}
static void DrawSigmoid(double gain)
{
	autoTable_t *bmp = newTable(GetColorBack, noop_u);
	uint c;
	uint d;

	for(c = 0; c < BMP_WH; c++)
	for(d = LINE_SPAN; d < BMP_WH; d += LINE_SPAN)
	{
		putTableCell(bmp, c, d, COLOR_LINE);
		putTableCell(bmp, d, c, COLOR_LINE);
	}
	for(c = 0; c < PLOT_MAX; c++)
	{
		double x1 = (c + 0) * (X_MAX - X_MIN) / PLOT_MAX + X_MIN;
		double x2 = (c + 1) * (X_MAX - X_MIN) / PLOT_MAX + X_MIN;
		double y1;
		double y2;

		cout("< %f, %f\n", x1, x2);

		y1 = GetSigmoid(gain, x1);
		y2 = GetSigmoid(gain, x2);

		cout("> %f, %f\n", y1, y2);

		{
			double dx1 = (c + 0) * (double)BMP_WH / PLOT_MAX;
			double dx2 = (c + 1) * (double)BMP_WH / PLOT_MAX;
			double dy1;
			double dy2;

			dy1 = (y1 - Y_MIN) * BMP_WH / (Y_MAX - Y_MIN);
			dy2 = (y2 - Y_MIN) * BMP_WH / (Y_MAX - Y_MIN);

			dy1 = BMP_WH - dy1;
			dy2 = BMP_WH - dy2;

			DrawLine(bmp, dx1, dy1, dx2, dy2);
		}
	}
	tWriteBMPFile_xx(getOutFile_x(xcout("gain_%07.3f.bmp", gain)), bmp);
}
int main(int argc, char **argv)
{
	uint g;

	for(g = 0; g <= 100; g++)
	{
		DrawSigmoid(g / 10.0);
	}
	openOutDir();
}
