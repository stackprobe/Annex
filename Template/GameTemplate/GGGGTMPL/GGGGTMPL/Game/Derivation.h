/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	ENUM_RANGE(D_DUMMY_00, 4)

	// app > @ D_

	// < app

	D_MAX, // num of member
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct DerInfo_st
{
	int ParentPicId;
	int X;
	int Y;
	int W;
	int H;
}
DerInfo_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Der(resCluster<PicInfo_t *> *resclu, int derId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Der_W(int derId);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
int Der_H(int derId);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UnloadAllDer(autoList<int> *derHandleList);