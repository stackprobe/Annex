/*
	SimilarPicMatrix.exe “ü—ÍDIR1 “ü—ÍDIR2
*/

#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csvtbl.h"
#include "C:\Factory\Common\Options\Collabo.h"
#include "C:\Factory\Meteor\BmpToCsv.h"

static char *GetBmpToCsvExe(void)
{
	static char *file;

	if(!file)
		file = GetCollaboFile(FILE_BMPTOCSV_EXE);

	return file;
}

#define GRID_W 30
#define GRID_H 30

// ---- ImgInfo_t ----

typedef struct ImgInfo_st
{
	char *File;
	float Lvs[GRID_W][GRID_H][3]; // [][][ R, G, B ]
}
ImgInfo_t;

static void ReleaseImgInfo(ImgInfo_t *i)
{
	memFree(i->File);
	memFree(i);
}

// ----

static void CsvFileToImgLvs(char *csvFile, ImgInfo_t *img)
{
	autoTable_t *csv = tReadCSVFile(csvFile);
	uint imgW;
	uint imgH;
	uint grdX;
	uint grdY;

	imgW = getTableWidth(csv);
	imgH = getTableHeight(csv);

	errorCase(UINTMAX / imgW < imgH); // ‘å‚«‚·‚¬‚éB
	errorCase(UINTMAX / GRID_W < imgW); // ‘å‚«‚·‚¬‚éB
	errorCase(UINTMAX / GRID_H < imgH); // ‘å‚«‚·‚¬‚éB

	for(grdX = 0; grdX < GRID_W; grdX++)
	for(grdY = 0; grdY < GRID_H; grdY++)
	{
		uint l = (imgW * grdX) / GRID_W;
		uint t = (imgH * grdY) / GRID_H;
		uint r = (imgW * (grdX + 1)) / GRID_W;
		uint b = (imgH * (grdY + 1)) / GRID_H;
		uint x;
		uint y;
		double ca = 0.0;
		double cr = 0.0;
		double cg = 0.0;
		double cb = 0.0;

		m_maxim(r, l + 1);
		m_maxim(b, t + 1);

		for(x = l; x < r; x++)
		for(y = t; y < b; y++)
		{
			uint c = toValueDigits((char *)getTableCell(csv, x, y), hexadecimal);

			ca += (double)(c >> 24 & 0xff);
			cr += (double)(c >> 16 & 0xff);
			cg += (double)(c >>  8 & 0xff);
			cb += (double)(c >>  0 & 0xff);
		}

		{
			double d = (double)((r - l) * (b - t));

			ca /= d;
			cr /= d;
			cg /= d;
			cb /= d;
		}

		// ”wŒi‚ð•(0,0,0)‚Æ‚µ‚Äa‚ðrgb‚É“K—p
		{
			ca /= 255.0;

			cr *= ca;
			cg *= ca;
			cb *= ca;
		}

		// 0.0 ` 1.0 ‚É‚·‚éB
		{
			cr /= 255.0;
			cg /= 255.0;
			cb /= 255.0;
		}

		img->Lvs[grdX][grdY][0] = (float)cr;
		img->Lvs[grdX][grdY][1] = (float)cg;
		img->Lvs[grdX][grdY][2] = (float)cb;
	}
	releaseTable(csv);
}
static ImgInfo_t *ToImg(char *file)
{
	ImgInfo_t *img = NULL;
	char *csvFile = makeTempPath("csv");

	coExecute_x(xcout("START \"\" /B /WAIT \"%s\" \"%s\" \"%s\"", GetBmpToCsvExe(), file, csvFile));

	if(existFile(csvFile))
	{
		img = (ImgInfo_t *)memAlloc(sizeof(ImgInfo_t));
		img->File = strx(file);
		CsvFileToImgLvs(csvFile, img);

		removeFile(csvFile);
	}
	memFree(csvFile);
	return img;
}
static void AddToImgs(autoList_t *files, autoList_t *imgs)
{
	char *file;
	uint index;

	foreach(files, file, index)
	{
		ImgInfo_t *img = ToImg(file);

		if(img)
			addElement(imgs, (uint)img);
	}
}

static autoList_t *XImgs;
static autoList_t *YImgs;

static autoTable_t *Matrix;

static void MakeMatrix(void)
{
	ImgInfo_t *xImg;
	ImgInfo_t *yImg;
	uint x;
	uint y;

	Matrix = newTable(getZero, (void (*)(uint))memFree); // elements are: double * | NULL

	resizeTable(Matrix, getCount(XImgs), getCount(YImgs));

	foreach(XImgs, xImg, x)
	foreach(YImgs, yImg, y)
	{
		double score = 0.0;
		double *pScore;
		uint grdX;
		uint grdY;
		uint c;

		for(grdX = 0; grdX < GRID_W; grdX++)
		for(grdY = 0; grdY < GRID_H; grdY++)
		for(c = 0; c < 3; c++)
		{
			double xLv = (double)xImg->Lvs[grdX][grdY][c];
			double yLv = (double)yImg->Lvs[grdX][grdY][c];
			double s;

			s = xLv - yLv;
			s *= s;

			score += s;
		}

		score /= GRID_W * GRID_H * 3; // 0.0 ` 1.0 ‚É‚·‚éB

		pScore = (double *)memAlloc(sizeof(double));
		*pScore = score;
		//pScore = (double *)memClone(&score, sizeof(double));
		setTableCell(Matrix, x, y, (uint)pScore);
	}
}
static void MakeSimilarPicMatrix(autoList_t *files1, autoList_t *files2)
{
	XImgs = newList();
	YImgs = newList();

	AddToImgs(files1, XImgs);
	AddToImgs(files2, YImgs);

	MakeMatrix();

//	releaseDim_BR(XImgs, 0, ReleaseImgInfo);
//	releaseDim_BR(YImgs, 0, ReleaseImgInfo);
}
static void OutputMatrix(void)
{
	FILE *fp;
	ImgInfo_t *img;
	uint index;
	uint x;
	uint y;

	fp = fileOpen(getOutFile("XImgFiles.txt"), "wt");

	foreach(XImgs, img, index)
		writeLine(fp, img->File);

	fileClose(fp);

	fp = fileOpen(getOutFile("YImgFiles.txt"), "wt");

	foreach(YImgs, img, index)
		writeLine(fp, img->File);

	fileClose(fp);

	fp = fileOpen(getOutFile("Matrix.csv"), "wt");

	for(y = 0; y < getTableHeight(Matrix); y++)
	{
		for(x = 0; x < getTableWidth(Matrix); x++)
		{
			if(x)
				writeChar(fp, ',');

			writeToken_x(fp, xcout("%.10f", *(double *)getTableCell(Matrix, x, y)));
		}
		writeChar(fp, '\n');
	}
	fileClose(fp);

	openOutDir();
}
static void Main2(char *dir1, char *dir2)
{
	autoList_t *files1 = lssFiles(dir1);
	autoList_t *files2 = lssFiles(dir2);

	sortJLinesICase(files1);
	sortJLinesICase(files2);

	MakeSimilarPicMatrix(files1, files2);

	OutputMatrix();

	releaseDim(files1, 1);
	releaseDim(files2, 1);
}
int main(int argc, char **argv)
{
	Main2(getArg(0), getArg(1));
}
