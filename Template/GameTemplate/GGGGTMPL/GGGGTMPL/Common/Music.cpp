/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct PlayInfo_st
{
	int Command; // "PVS"
	MusicInfo_t *Music;
	int OnceMode;
	int ResumeMode;
	double VolumeRate;
}
PlayInfo_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static PlayInfo_t *CreatePI(int command, MusicInfo_t *music, int once_mode, int resume_mode, double volume_rate)
{
	PlayInfo_t *i = nb_(PlayInfo_t);

	i->Command = command;
	i->Music = music;
	i->OnceMode = once_mode;
	i->ResumeMode = resume_mode;
	i->VolumeRate = volume_rate;

	return i;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void ReleasePI(PlayInfo_t *i)
{
	memFree(i);
}
static oneObject(autoQueue<PlayInfo_t *>, new autoQueue<PlayInfo_t *>(), GetPlayList);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
MusicInfo_t *CurrDestMusic;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
double CurrDestMusicVolumeRate;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static MusicInfo_t *LoadMusic(autoList<uchar> *fileData)
{
	MusicInfo_t *i = nb_(MusicInfo_t);

	i->Handle = LoadSound(fileData);
	i->Volume = 0.5; // 個別の音量のデフォルト 0.0 - 1.0

	switch(RC_ResId) // musId
	{
	// app > @ post LoadSound

	/*
	case MUS_xxx:
		break;
	*/

	// < app

	case -1: // dummy
	default:
		break;
	}

	SetVolume(i->Handle, 0.0); // ミュートしておく。
	return i;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void UnloadMusic(MusicInfo_t *i)
{
	// reset
	{
		GetPlayList()->Clear(ReleasePI);

		CurrDestMusic = NULL;
		CurrDestMusicVolumeRate = 0.0;
	}

	UnloadSound(i->Handle);
	memFree(i);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static oneObject(
	resCluster<MusicInfo_t *>,
	new resCluster<MusicInfo_t *>("Music.dat", "..\\..\\Music.txt", MUS_MAX, 120000000, LoadMusic, UnloadMusic),
	GetMusRes
	);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MusicEachFrame(void)
{
	PlayInfo_t *i = GetPlayList()->Dequeue(NULL);

	if(i)
	{
		switch(i->Command)
		{
		case 'P':
			SoundPlay(i->Music->Handle, i->OnceMode, i->ResumeMode);
			break;

		case 'V':
			SetVolume(i->Music->Handle, MixVolume(Gnd.MusicVolume, i->Music->Volume) * i->VolumeRate);
			break;

		case 'S':
			SoundStop(i->Music->Handle);
			break;

		default:
			error();
		}
		memFree(i);
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MusicPlay(int musId, int once_mode, int resume_mode, double volumeRate, int fadeFrameMax)
{
	errorCase(musId < 0 || MUS_MAX <= musId);

	MusicInfo_t *i = GetMusRes()->GetHandle(musId);

	if(CurrDestMusic)
	{
		if(CurrDestMusic == i)
			return;

		if(fadeFrameMax)
			MusicFade(fadeFrameMax);
		else
			MusicStop();
	}
	GetPlayList()->Enqueue(CreatePI('P', i, once_mode, resume_mode, 0.0));
//	GetPlayList()->Enqueue(NULL, 1);
	GetPlayList()->Enqueue(CreatePI('V', i, 0, 0, volumeRate));
	GetPlayList()->Enqueue(NULL, 3);

	CurrDestMusic = i;
	CurrDestMusicVolumeRate = volumeRate;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MusicFade(int frameMax, double destVRate, double startVRate)
{
	if(!CurrDestMusic)
		return;

	m_range(frameMax, 1, 3600); // 1 frame - 1 min
	m_range(destVRate, 0.0, 1.0);
	m_range(startVRate, 0.0, 1.0);

	for(int frmcnt = 0; frmcnt <= frameMax; frmcnt++)
	{
		double vRate;

		if(!frmcnt)
			vRate = startVRate;
		else if(frmcnt == frameMax)
			vRate = destVRate;
		else
			vRate = startVRate + ((destVRate - startVRate) * frmcnt) / frameMax;

		GetPlayList()->Enqueue(CreatePI('V', CurrDestMusic, 0, 0, vRate));
	}
	CurrDestMusicVolumeRate = destVRate;

	if(destVRate == 0.0) // ? フェード目標音量ゼロ -> 曲停止
	{
		MusicStop();
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MusicStop(void)
{
	if(!CurrDestMusic)
		return;

	GetPlayList()->Enqueue(CreatePI('V', CurrDestMusic, 0, 0, 0.0));
	GetPlayList()->Enqueue(NULL, 3);
	GetPlayList()->Enqueue(CreatePI('S', CurrDestMusic, 0, 0, 0.0));
	GetPlayList()->Enqueue(NULL, 3);

	CurrDestMusic = NULL;
	CurrDestMusicVolumeRate = 0.0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void MusicTouch(int musId)
{
	GetMusRes()->GetHandle(musId);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UpdateMusicVolume(void)
{
	MusicFade(0, 1.0);
}
