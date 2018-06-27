/*
	SimilarPicMatrix.exe “ü—ÍDIR1 “ü—ÍDIR2
*/

#include "C:\Factory\Common\all.h"
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
	double Lvs[GRID_W][GRID_H][3];
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
	// TODO
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

	Matrix = newTable(getZero, noop_u);

	resizeTable(Matrix, getCount(XImgs), getCount(YImgs));

	foreach(XImgs, xImg, x)
	foreach(YImgs, yImg, y)
	{
		//
	}
}
static void MakeSimilarPicMatrix(autoList_t *files1, autoList_t *files2)
{
	XImgs = newList();
	YImgs = newList();

	AddToImgs(files1, XImgs);
	AddToImgs(files2, YImgs);

	MakeMatrix();

	releaseDim_BR(XImgs, 0, ReleaseImgInfo);
	releaseDim_BR(YImgs, 0, ReleaseImgInfo);
}
static void OutputMatrix(void)
{
	// TODO
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
