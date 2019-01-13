/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
#include "all.h"

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
Gnd_t Gnd;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Gnd_INIT(void)
{
	memset(&Gnd, 0x00, sizeof(Gnd_t));

	Gnd.EL = new taskList();
	Gnd.PrimaryPadId = -1;

	// app > @ Gnd_INIT

	// < app

	// SaveData -->

	Gnd.RealScreen_W = SCREEN_W;
	Gnd.RealScreen_H = SCREEN_H;

	Gnd.MusicVolume = DEFAULT_VOLUME;
	Gnd.SEVolume = DEFAULT_VOLUME;

	Gnd.PadBtnId.Dir_2 = SNWPB_DIR_2;
	Gnd.PadBtnId.Dir_4 = SNWPB_DIR_4;
	Gnd.PadBtnId.Dir_6 = SNWPB_DIR_6;
	Gnd.PadBtnId.Dir_8 = SNWPB_DIR_8;
	Gnd.PadBtnId.A = SNWPB_A1;
	Gnd.PadBtnId.B = SNWPB_B1;
	Gnd.PadBtnId.C = SNWPB_A2;
	Gnd.PadBtnId.D = SNWPB_B2;
	Gnd.PadBtnId.E = SNWPB_A3;
	Gnd.PadBtnId.F = SNWPB_B3;
	Gnd.PadBtnId.L = SNWPB_L;
	Gnd.PadBtnId.R = SNWPB_R;
	Gnd.PadBtnId.Pause = SNWPB_DSTART;
	Gnd.PadBtnId.Start = SNWPB_USTART;

	Gnd.KbdKeyId.Dir_2 = KEY_INPUT_DOWN;
	Gnd.KbdKeyId.Dir_4 = KEY_INPUT_LEFT;
	Gnd.KbdKeyId.Dir_6 = KEY_INPUT_RIGHT;
	Gnd.KbdKeyId.Dir_8 = KEY_INPUT_UP;
	Gnd.KbdKeyId.A = KEY_INPUT_Z;
	Gnd.KbdKeyId.B = KEY_INPUT_X;
	Gnd.KbdKeyId.C = KEY_INPUT_C;
	Gnd.KbdKeyId.D = KEY_INPUT_V;
	Gnd.KbdKeyId.E = KEY_INPUT_A;
	Gnd.KbdKeyId.F = KEY_INPUT_S;
	Gnd.KbdKeyId.L = KEY_INPUT_D;
	Gnd.KbdKeyId.R = KEY_INPUT_F;
	Gnd.KbdKeyId.Pause = KEY_INPUT_SPACE;
	Gnd.KbdKeyId.Start = KEY_INPUT_RETURN;

	Gnd.RO_MouseDispMode = 0;

	// app > @ Gnd_INIT SaveData

	Gnd.RO_MouseDispMode = 0;

	// < app
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void Gnd_FNLZ(void)
{
	delete Gnd.EL;

	// app > @ Gnd_FNLZ

	// < app

	// SaveData -->

	// app > @ Gnd_FNLZ SaveData

	// < app
}

// ---- SaveData ----

#define SAVE_FILE "SaveData.dat"
#define SAVEDATA_SIGNATURE "__Codevil__SaveData " __DATE__ " " __TIME__

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static autoList<uchar> *SaveData;
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int SDIndex;

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static char *SD_ReadLine(void) // ret: bind
{
	static char *line;

	memFree(line);
	line = readLine(SaveData, SDIndex);

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	if(!line)
		line = strx("");

	return line;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int SD_ReadBoolean(void)
{
	return atoi(SD_ReadLine()) ? 1 : 0;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int SD_ReadInt(int minval, int maxval)
{
	int value = atoi(SD_ReadLine());
	m_range(value, minval, maxval);
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static __int64 SD_ReadInt64(__int64 minval, __int64 maxval)
{
	__int64 value = _atoi64(SD_ReadLine());
	m_range(value, minval, maxval);
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static double SD_ReadDouble(double minval, double maxval, int denom)
{
	errorCase(denom < 1);

	double value = (double)atoi(SD_ReadLine()) / denom;
	m_range(value, minval, maxval);
	return value;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_ReadBitList(bitList *dest)
{
	dest->Clear();

	char *line = SD_ReadLine();

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; line[index]; index++)
	{
		switch(line[index])
		{
		case '0':
//			dest->PutBit(index, 0); // 不要
			break;

		case '1':
			dest->PutBit(index, 1);
			break;

		default:
			error();
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_ReadLines(autoList<char *> *lines)
{
	errorCase(!lines);

	int count = SD_ReadInt(0, IMAX);

	lines->BufferExtend(count);

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; index < count; index++)
	{
		lines->AddElement(strx(SD_ReadLine()));
	}
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteLine(char *line)
{
	writeLine(SaveData, line);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteLine_x(char *line)
{
	SD_WriteLine(line);
	memFree(line);
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteInt(int value)
{
	SD_WriteLine_x(xcout("%d", value));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteInt64(__int64 value)
{
	SD_WriteLine_x(xcout("%I64d", value));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteDouble(double value, int denom)
{
	SD_WriteInt(d2i(value * denom));
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteBitList(bitList *src)
{
	autoList<char> *buff = new autoList<char>();
	int finalBitPos = src->GetFinalBitPos();

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; index <= finalBitPos; index++)
	{
		buff->AddElement(src->RefBit(index) ? '1' : '0');
	}
	buff->AddElement('\0');
	SD_WriteLine_x(buff->UnbindBuffer());
	delete buff;
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteLines(autoList<char *> *lines)
{
	SD_WriteInt(lines->GetCount());

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	for(int index = 0; index < lines->GetCount(); index++)
	{
		SD_WriteLine(lines->GetElement(index));
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void SD_WriteLines_x(autoList<char *> *lines)
{
	SD_WriteLines(lines);
	releaseList(lines, (void (*)(char *))memFree);
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static int *PPadBtns[] =
{
	&Gnd.PadBtnId.Dir_8,
	&Gnd.PadBtnId.Dir_2,
	&Gnd.PadBtnId.Dir_4,
	&Gnd.PadBtnId.Dir_6,
	&Gnd.PadBtnId.A,
	&Gnd.PadBtnId.B,
	&Gnd.PadBtnId.C,
	&Gnd.PadBtnId.D,
	&Gnd.PadBtnId.E,
	&Gnd.PadBtnId.F,
	&Gnd.PadBtnId.L,
	&Gnd.PadBtnId.R,
	&Gnd.PadBtnId.Pause,
	&Gnd.PadBtnId.Start,
};
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
static void AntiPadBtnIdConflict(void)
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	/*
		重複して割り当てられていた場合、両方 -1 にする。
	*/
	for(int index = 1; index < lengthof(PPadBtns); index++)
	{
		for(int ndx = 0; ndx < index; ndx++)
		{
			if(*PPadBtns[index] == *PPadBtns[ndx])
			{
				*PPadBtns[index] = -1;
				*PPadBtns[ndx] = -1;
				break;
			}
		}
	}
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void UnassignAllPadBtnId(void)
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	/*
		全て割り当てナシにする。
	*/
	for(int index = 0; index < lengthof(PPadBtns); index++)
	{
		*PPadBtns[index] = -1;
	}
}

/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ImportSaveData(void)
{
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	if(!accessible(SAVE_FILE))
	{
		LOG("セーブデータファイル無し\n");
		return;
	}
	SaveData = readFile(SAVE_FILE);

#if LOG_ENABLED == 0
	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	if(!Jammer(SaveData, 0)) // ? セーブデータ破損
	{
		delete SaveData;
		return;
	}
#endif
	SDIndex = 0;

	/*
		copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	*/
	if(strcmp(SD_ReadLine(), SAVEDATA_SIGNATURE) != 0) // ? セーブデータ破損
	{
		LOG("セーブデータファイル_Signature不一致\n");
		delete SaveData;
		return;
	}

	// セーブデータ項目 {

	Gnd.RealScreen_W = SD_ReadInt(SCREEN_W, SCREEN_W_MAX);
	Gnd.RealScreen_H = SD_ReadInt(SCREEN_H, SCREEN_H_MAX);

	Gnd.MusicVolume = SD_ReadDouble(0.0, 1.0, IMAX);
	Gnd.SEVolume = SD_ReadDouble(0.0, 1.0, IMAX);

	Gnd.PadBtnId.Dir_2 = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.Dir_4 = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.Dir_6 = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.Dir_8 = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.A = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.B = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.C = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.D = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.E = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.F = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.L = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.R = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.Pause = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);
	Gnd.PadBtnId.Start = SD_ReadInt(-1, PAD_BUTTON_MAX - 1);

	Gnd.KbdKeyId.Dir_2 = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.Dir_4 = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.Dir_6 = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.Dir_8 = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.A = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.B = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.C = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.D = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.E = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.F = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.L = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.R = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.Pause = SD_ReadInt(-1, KEY_MAX - 1);
	Gnd.KbdKeyId.Start = SD_ReadInt(-1, KEY_MAX - 1);

	Gnd.RO_MouseDispMode = SD_ReadBoolean();

	// app > @ ImportSaveData

	// < app

	// }

	delete SaveData;

	// app > @ post ImportSaveData

	AntiPadBtnIdConflict();

	// < app
}
/*
	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
*/
void ExportSaveData(void)
{
	SaveData = new autoList<uchar>();

	SD_WriteLine(SAVEDATA_SIGNATURE);

	// セーブデータ項目 {

	SD_WriteInt(Gnd.RealScreen_W);
	SD_WriteInt(Gnd.RealScreen_H);

	SD_WriteDouble(Gnd.MusicVolume, IMAX);
	SD_WriteDouble(Gnd.SEVolume, IMAX);

	SD_WriteInt(Gnd.PadBtnId.Dir_2);
	SD_WriteInt(Gnd.PadBtnId.Dir_4);
	SD_WriteInt(Gnd.PadBtnId.Dir_6);
	SD_WriteInt(Gnd.PadBtnId.Dir_8);
	SD_WriteInt(Gnd.PadBtnId.A);
	SD_WriteInt(Gnd.PadBtnId.B);
	SD_WriteInt(Gnd.PadBtnId.C);
	SD_WriteInt(Gnd.PadBtnId.D);
	SD_WriteInt(Gnd.PadBtnId.E);
	SD_WriteInt(Gnd.PadBtnId.F);
	SD_WriteInt(Gnd.PadBtnId.L);
	SD_WriteInt(Gnd.PadBtnId.R);
	SD_WriteInt(Gnd.PadBtnId.Pause);
	SD_WriteInt(Gnd.PadBtnId.Start);

	SD_WriteInt(Gnd.KbdKeyId.Dir_2);
	SD_WriteInt(Gnd.KbdKeyId.Dir_4);
	SD_WriteInt(Gnd.KbdKeyId.Dir_6);
	SD_WriteInt(Gnd.KbdKeyId.Dir_8);
	SD_WriteInt(Gnd.KbdKeyId.A);
	SD_WriteInt(Gnd.KbdKeyId.B);
	SD_WriteInt(Gnd.KbdKeyId.C);
	SD_WriteInt(Gnd.KbdKeyId.D);
	SD_WriteInt(Gnd.KbdKeyId.E);
	SD_WriteInt(Gnd.KbdKeyId.F);
	SD_WriteInt(Gnd.KbdKeyId.L);
	SD_WriteInt(Gnd.KbdKeyId.R);
	SD_WriteInt(Gnd.KbdKeyId.Pause);
	SD_WriteInt(Gnd.KbdKeyId.Start);

	SD_WriteInt(GetMouseDispMode());

	// app > @ ExportSaveData

	// < app

	// }

#if LOG_ENABLED == 0
	errorCase(!Jammer(SaveData, 1));
#endif

	writeFile_cx(SAVE_FILE, SaveData);
}
