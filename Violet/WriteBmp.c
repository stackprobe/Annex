#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>

// ----

#define MAXWIDTH  1000
#define MAXHEIGHT 1000

typedef struct
{
	unsigned char r;
	unsigned char g;
	unsigned char b;
}
color;

typedef struct
{
	long height;
	long width;
	color data[MAXHEIGHT][MAXWIDTH];
}
img;

// ---- WriteBmp ----

static void WrUI(FILE *fp, unsigned int value)
{
	fputc((value >>  0) & 0xff, fp);
	fputc((value >>  8) & 0xff, fp);
	fputc((value >> 16) & 0xff, fp);
	fputc((value >> 24) & 0xff, fp);
}
void WriteBmp(char *filename, img *tp)
{
	FILE *fp = fopen(filename, "wb");
	unsigned int szimg;
	int x;
	int y;

	if(!fp)
		return; // open error !

	szimg = ((tp->width * 3 + 3) / 4) * 4 * tp->height;

	fputc('B', fp);
	fputc('M', fp);
	WrUI(fp, szimg + 0x36);
	WrUI(fp, 0);
	WrUI(fp, 0x36);
	WrUI(fp, 0x28);
	WrUI(fp, tp->width);
	WrUI(fp, tp->height);
	WrUI(fp, 0x00180001);
	WrUI(fp, 0);
	WrUI(fp, szimg);
	WrUI(fp, 0);
	WrUI(fp, 0);
	WrUI(fp, 0);
	WrUI(fp, 0);

	for(y = tp->height - 1; 0 <= y; y--)
	{
		for(x = 0; x < tp->width; x++)
		{
			color *col = tp->data[y] + x;

			fputc(col->b, fp);
			fputc(col->g, fp);
			fputc(col->r, fp);
		}
		for(x = tp->width % 4; x; x--)
		{
			fputc(0x00, fp);
		}
	}
	fclose(fp);
}

// ----

int main(int argc, char **argv)
{
	int i,j;
	img *tmp1,*tmp2;

	tmp1=(img *)malloc(sizeof(img));
	tmp2=(img *)malloc(sizeof(img));

//	char *filename = argv[1];

#if 1
	tmp1->height = 200;
	tmp1->width = 300;

	for(i=0;i<tmp1->height;i++)
	for(j=0;j<tmp1->width;j++)
	{
		tmp1->data[i][j].r = rand()%256;
		tmp1->data[i][j].g = rand()%256;
		tmp1->data[i][j].b = rand()%256;
	}
#else
	tmp1->height = 500;
	tmp1->width = 400;

	for(i=0;i<tmp1->height;i++)
	for(j=0;j<tmp1->width;j++)
	{
		tmp1->data[i][j].r = i == 0 || j == 0 || i == tmp1->height - 1 || j == tmp1->width - 1 ? 255 : 0;
		tmp1->data[i][j].g = 0;
		tmp1->data[i][j].b = 0;
	}
#endif

//	WriteBmp("noise.bmp", tmp1);
	WriteBmp("1.bmp", tmp1);
}
