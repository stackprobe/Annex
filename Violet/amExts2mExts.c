#include "C:\Factory\Common\all.h"

#define AM_EXTS_FILE "C:\\Dev\\Tools2\\NatsuConv\\natsuconv\\doc\\audio_movie_extensions.dat"

#define FFMPEG_FILE "C:\\app\\ffmpeg-3.2.2-win64-shared\\bin\\ffmpeg.exe"
#define FFPROBE_FILE "C:\\app\\ffmpeg-3.2.2-win64-shared\\bin\\ffprobe.exe"

#define MOVIE_DIR "C:\\var\\dat\\GirlsUndPanzerPv4.m4v"
#define VIDEO_FILES MOVIE_DIR "\\video\\%%010d.png"
#define AUDIO_FILE MOVIE_DIR "\\audio.wav"

static uint GetVideoStreamCount(char *movieFile)
{
	char *redirFile = makeTempPath(NULL);
	uint msCount = 0;

	coExecute_x(xcout(FFPROBE_FILE " %s 2> %s", movieFile, redirFile));

	{
		autoList_t *lines = readLines(redirFile);
		char *line;
		uint index;

		foreach(lines, line, index)
			if(strstr(line, "Stream") && strstr(line, "Video:"))
				msCount++;

		releaseDim(lines, 1);
	}

	removeFile(redirFile);
	memFree(redirFile);

	cout("msCount: %u\n", msCount);

	return msCount;
}
static Main2(char *amExtsFile)
{
	autoList_t *exts = readLines(amExtsFile);
	char *ext;
	uint index;
	autoList_t *mExts = newList();
	char *movieFileBase = makeTempPath(NULL);

	foreach(exts, ext, index)
	{
		char *movieFile = xcout("%s.%s", movieFileBase, ext);
		uint msCount;

		coExecute_x(xcout(FFMPEG_FILE " -r 24 -i " VIDEO_FILES " -i " AUDIO_FILE " -map 0:0 -map 1:0 %s", movieFile));

		if(!existFile(movieFile))
			goto goNext;

		msCount = GetVideoStreamCount(movieFile);
		removeFile(movieFile);

		if(!msCount)
			goto goNext;

		addElement(mExts, (uint)strx(ext));

	goNext:
		memFree(movieFile);
	}
	writeLines(getOutFile("movie_extensions.dat"), mExts);

	openOutDir();

	// HACK gomi...
}
int main(int argc, char **argv)
{
	Main2(AM_EXTS_FILE);
}
