#include "C:\Factory\Common\all.h"
#include "C:\Factory\Common\Options\csvStream.h"

static void DBFFileToCSVFile(char *dbfFile, char *csvFile)
{
	FILE *rfp = fileOpen(dbfFile, "rb");
	FILE *wfp = fileOpen(csvFile, "wt");
	uint recordCount;
	uint recordSize;
	uint fieldTotalSize = 0;
	autoList_t *fieldNames = newList();
	autoList_t *fieldSizes = newList();

	//	1	H	�t�@�C���^�C�v
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "�t�@�C���^�C�v");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	3	H	�ŏI�X�V
	{
		int c1;
		int c2;
		int c3;

		c1 = neReadChar(rfp);
		c2 = neReadChar(rfp);
		c3 = neReadChar(rfp);

		writeCSVCell(wfp, "�ŏI�X�V");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%04d/%02d/%02d", 1970 + c1, c2, c3)); // todo: ����ł����̂��H
		writeChar(wfp, '\n');
	}

	//	4	i	���R�[�h��
	{
		uint value = readValue(rfp);

		writeCSVCell(wfp, "���R�[�h��");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');

		recordCount = value; // �������ۑ�
	}

	//	2	i	�ŏ��̃��R�[�h�̈ʒu
	{
		uint value = readValueWidth(rfp, 2);

		writeCSVCell(wfp, "�ŏ��̃��R�[�h�̈ʒu");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');
	}

	//	2	i	���R�[�h����
	{
		uint value = readValueWidth(rfp, 2);

		writeCSVCell(wfp, "���R�[�h����");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');

		recordSize = value; // �������ۑ�
	}

	//	16	H	�\��
	{
		char *line = insertLine(makeHexLine_x(neReadBinaryBlock(rfp, 16)), 0, "0x");

		writeCSVCell(wfp, "�\��");
		writeChar(wfp, ',');
		writeCSVCell(wfp, line);
		writeChar(wfp, '\n');

		memFree(line);
	}

	//	1	H	�e�[�u���t���O
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "�e�[�u���t���O");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	1	H	�R�[�h�y�[�W�}�[�N
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "�R�[�h�y�[�W�}�[�N");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	2	H	�\��
	{
		int c1;
		int c2;

		c1 = neReadChar(rfp);
		c2 = neReadChar(rfp);

		writeCSVCell(wfp, "�\��");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x%02x", c1, c2));
		writeChar(wfp, '\n');
	}

	writeChar(wfp, '\n'); // ��s

	                     writeCSVCell(wfp, "�t�B�[���h��");
	writeChar(wfp, ','); writeCSVCell(wfp, "�t�B�[���h�^�C�v");
	writeChar(wfp, ','); writeCSVCell(wfp, "�\��");
	writeChar(wfp, ','); writeCSVCell(wfp, "�t�B�[���h����");
	writeChar(wfp, ','); writeCSVCell(wfp, "�t�B�[���h����_decimal");
	writeChar(wfp, ','); writeCSVCell(wfp, "��Ɨ̈�ID");
	writeChar(wfp, ','); writeCSVCell(wfp, "��");
	writeChar(wfp, ','); writeCSVCell(wfp, "�\��");
	writeChar(wfp, ','); writeCSVCell(wfp, "�v���_�N�V����MDX�t�B�[���h�t���O");
	writeChar(wfp, '\n');

	while(neReadChar(rfp) != 0x0d)
	{
		fileSeek(rfp, SEEK_CUR, -1);

		//	11	SN	�t�B�[���h��
		{
			char *line = ab_makeLine_x(neReadBinaryBlock(rfp, 11));

			line2JLine(line, 1, 0, 0, 1);

			writeCSVCell(wfp, line);
			writeChar(wfp, ',');

			addElement(fieldNames, (uint)line); // �������ۑ�
//			memFree(line);
		}

		//	1	S	�t�B�[���h�^�C�v
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%c (0x%02x)", toHalf(c), c));
			writeChar(wfp, ',');
		}

		//	4	H	�\��
		{
			int c1;
			int c2;
			int c3;
			int c4;

			c1 = neReadChar(rfp);
			c2 = neReadChar(rfp);
			c3 = neReadChar(rfp);
			c4 = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x%02x%02x%02x", c1, c2, c3, c4));
			writeChar(wfp, ',');
		}

		//	1	i	�t�B�[���h����
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%d", c));
			writeChar(wfp, ',');

			fieldTotalSize += (uint)c;
			addElement(fieldSizes, (uint)c); // �������ۑ�
		}

		//	1	i	�t�B�[���h����_decimal
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%d", c));
			writeChar(wfp, ',');
		}

		//	2	H	��Ɨ̈�ID
		{
			int c1;
			int c2;

			c1 = neReadChar(rfp);
			c2 = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x%02x", c1, c2));
			writeChar(wfp, ',');
		}

		//	1	H	��
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x", c));
			writeChar(wfp, ',');
		}

		//	10	H	�\��
		{
			char *line = insertLine(makeHexLine_x(neReadBinaryBlock(rfp, 10)), 0, "0x");

			writeCSVCell(wfp, line);
			writeChar(wfp, ',');

			memFree(line);
		}

		//	1	H	�v���_�N�V����MDX�t�B�[���h�t���O
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x", c));

			writeChar(wfp, '\n'); // ���ŏI��
		}
	}

	errorCase(recordSize < fieldTotalSize);

	writeChar(wfp, '\n'); // ��s

	{
		uint rowidx;
		uint colidx;

		for(colidx = 0; colidx < getCount(fieldNames); colidx++)
		{
			if(colidx)
				writeChar(wfp, ',');

			writeCSVCell(wfp, getLine(fieldNames, colidx));
		}
		writeChar(wfp, '\n');

		for(rowidx = 0; rowidx < recordCount; rowidx++)
		{
			/*
				fixme: ���R�[�h�T�C�Y�ƃt�B�[���h���v�T�C�Y�̍��̓��R�[�h�̑O�̃p�f�B���O�Ɣ��f�B
					����ł����̂��H
			*/
			for(colidx = fieldTotalSize; colidx < recordSize; colidx++)
				neReadChar(rfp);

			for(colidx = 0; colidx < getCount(fieldSizes); colidx++)
			{
				char *line = ab_makeLine_x(neReadBinaryBlock(rfp, getElement(fieldSizes, colidx)));

				line2JLine(line, 1, 0, 0, 1); // todo: �S�������񈵂��B

				if(colidx)
					writeChar(wfp, ',');

				writeCSVCell_x(wfp, line);
			}
			writeChar(wfp, '\n');
		}
	}

	errorCase(neReadChar(rfp) != 0x1a); // ? �^�[�~�l�[�^ // todo: ����Ȃ̂���́H

	errorCase(readChar(rfp) != EOF); // ? �܂��t�@�C���̓r��

	fileClose(rfp);
	fileClose(wfp);
	releaseDim(fieldNames, 1);
	releaseAutoList(fieldSizes);
}
int main(int argc, char **argv)
{
	if(hasArgs(2))
	{
		DBFFileToCSVFile(getArg(0), getArg(1));
		return;
	}

	{
		char *dbfFile = dropFile();

		if(dbfFile)
		{
			DBFFileToCSVFile(dbfFile, getOutFile("_dbf.csv"));
			openOutDir();
			memFree(dbfFile);
		}
	}
}
