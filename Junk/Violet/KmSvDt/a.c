#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	autoBlock_t *data1 = readBinary("C:\\var\\koumajouSaveData\\savedata_st8_extra_hiScore=55956.dat");
	autoBlock_t *data2 = readBinary("C:\\var\\koumajouSaveData\\savedata_st8_extra_hiScore=172177.dat");
	uint index;
	int msk;
	int d1c;
	int d2c;
	int raw;

	msk = 0x2d; // „’è
	d1c = getByte(data1, 0x34);
	d2c = getByte(data1, 0x34);

	raw = d1c ^ msk;

	cout("%02x --\n", raw);

	for(index = 0x35; index < getSize(data1); index++)
	{
		d1c = getByte(data1, index);
		d2c = getByte(data2, index);

		msk = d2c ^ raw;
		raw = d1c ^ msk;

		cout("%02x %02x\n", raw, msk);
	}

	{
		d2c = getByte(data2, index);

		msk = d2c ^ raw;

		cout("-- %02x\n", msk);
	}
}
