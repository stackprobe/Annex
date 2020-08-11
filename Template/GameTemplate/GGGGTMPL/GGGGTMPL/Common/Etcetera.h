/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
enum
{
	ETC_DATSTRINGS,
	ETC_JCHAR,

	// app > @ ETC_

	ETC_FONT_RIIT,
	ETC_FONT_GENKAI_MINCHO,

	// < app

	ETC_MAX, // num of member
};

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
autoList<uchar> *GetEtcFileData(int etcId);
