/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
typedef struct Gnd_st
{
	taskList *EL; // EffectList
	int PrimaryPadId; // -1 == ���ݒ�
	SubScreen_t *MainScreen; // NULL == �s�g�p

	iRect_t Monitors[MONITOR_MAX];
	int MonitorNum;

	// app > @ Gnd_t

	// < app

	// SaveData {

	int RealScreen_W;
	int RealScreen_H;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	/*
		����
		0.0 - 1.0
		def: DEFAULT_VOLUME
	*/
	double MusicVolume;
	double SEVolume;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	/*
		-1 == ���蓖�ăi�V
		0 - (PAD_BUTTON_MAX - 1) == ���蓖�ă{�^��ID
		def: SNWPB_*
	*/
	struct PadBtnId_st
	{
		int Dir_2;
		int Dir_4;
		int Dir_6;
		int Dir_8;
		int A;
		int B;
		int C;
		int D;
		int E;
		int F;
		int L;
		int R;
		int Pause;
		int Start;
	}
	PadBtnId;

	struct PadBtnId_st KbdKeyId;

	int RO_MouseDispMode;

	// app > @ Gnd_t SaveData

	// < app

	// }
}
Gnd_t;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
extern Gnd_t Gnd;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Gnd_INIT(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Gnd_FNLZ(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ImportSaveData(void);
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ExportSaveData(void);

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UnassignAllPadBtnId(void);