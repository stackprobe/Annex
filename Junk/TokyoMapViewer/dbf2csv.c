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

	//	1	H	ファイルタイプ
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "ファイルタイプ");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	3	H	最終更新
	{
		int c1;
		int c2;
		int c3;

		c1 = neReadChar(rfp);
		c2 = neReadChar(rfp);
		c3 = neReadChar(rfp);

		writeCSVCell(wfp, "最終更新");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%04d/%02d/%02d", 1970 + c1, c2, c3)); // todo: これでいいのか？
		writeChar(wfp, '\n');
	}

	//	4	i	レコード数
	{
		uint value = readValue(rfp);

		writeCSVCell(wfp, "レコード数");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');

		recordCount = value; // ★★★保存
	}

	//	2	i	最初のレコードの位置
	{
		uint value = readValueWidth(rfp, 2);

		writeCSVCell(wfp, "最初のレコードの位置");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');
	}

	//	2	i	レコード長さ
	{
		uint value = readValueWidth(rfp, 2);

		writeCSVCell(wfp, "レコード長さ");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("%u", value));
		writeChar(wfp, '\n');

		recordSize = value; // ★★★保存
	}

	//	16	H	予約
	{
		char *line = insertLine(makeHexLine_x(neReadBinaryBlock(rfp, 16)), 0, "0x");

		writeCSVCell(wfp, "予約");
		writeChar(wfp, ',');
		writeCSVCell(wfp, line);
		writeChar(wfp, '\n');

		memFree(line);
	}

	//	1	H	テーブルフラグ
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "テーブルフラグ");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	1	H	コードページマーク
	{
		int c = neReadChar(rfp);

		writeCSVCell(wfp, "コードページマーク");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x", c));
		writeChar(wfp, '\n');
	}

	//	2	H	予約
	{
		int c1;
		int c2;

		c1 = neReadChar(rfp);
		c2 = neReadChar(rfp);

		writeCSVCell(wfp, "予約");
		writeChar(wfp, ',');
		writeCSVCell_x(wfp, xcout("0x%02x%02x", c1, c2));
		writeChar(wfp, '\n');
	}

	writeChar(wfp, '\n'); // 空行

	                     writeCSVCell(wfp, "フィールド名");
	writeChar(wfp, ','); writeCSVCell(wfp, "フィールドタイプ");
	writeChar(wfp, ','); writeCSVCell(wfp, "予約");
	writeChar(wfp, ','); writeCSVCell(wfp, "フィールド長さ");
	writeChar(wfp, ','); writeCSVCell(wfp, "フィールド長さ_decimal");
	writeChar(wfp, ','); writeCSVCell(wfp, "作業領域ID");
	writeChar(wfp, ','); writeCSVCell(wfp, "例");
	writeChar(wfp, ','); writeCSVCell(wfp, "予約");
	writeChar(wfp, ','); writeCSVCell(wfp, "プロダクションMDXフィールドフラグ");
	writeChar(wfp, '\n');

	while(neReadChar(rfp) != 0x0d)
	{
		fileSeek(rfp, SEEK_CUR, -1);

		//	11	SN	フィールド名
		{
			char *line = ab_makeLine_x(neReadBinaryBlock(rfp, 11));

			line2JLine(line, 1, 0, 0, 1);

			writeCSVCell(wfp, line);
			writeChar(wfp, ',');

			addElement(fieldNames, (uint)line); // ★★★保存
//			memFree(line);
		}

		//	1	S	フィールドタイプ
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%c (0x%02x)", toHalf(c), c));
			writeChar(wfp, ',');
		}

		//	4	H	予約
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

		//	1	i	フィールド長さ
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%d", c));
			writeChar(wfp, ',');

			fieldTotalSize += (uint)c;
			addElement(fieldSizes, (uint)c); // ★★★保存
		}

		//	1	i	フィールド長さ_decimal
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("%d", c));
			writeChar(wfp, ',');
		}

		//	2	H	作業領域ID
		{
			int c1;
			int c2;

			c1 = neReadChar(rfp);
			c2 = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x%02x", c1, c2));
			writeChar(wfp, ',');
		}

		//	1	H	例
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x", c));
			writeChar(wfp, ',');
		}

		//	10	H	予約
		{
			char *line = insertLine(makeHexLine_x(neReadBinaryBlock(rfp, 10)), 0, "0x");

			writeCSVCell(wfp, line);
			writeChar(wfp, ',');

			memFree(line);
		}

		//	1	H	プロダクションMDXフィールドフラグ
		{
			int c = neReadChar(rfp);

			writeCSVCell_x(wfp, xcout("0x%02x", c));

			writeChar(wfp, '\n'); // ★最終列★
		}
	}

	errorCase(recordSize < fieldTotalSize);

	writeChar(wfp, '\n'); // 空行

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
				fixme: レコードサイズとフィールド合計サイズの差はレコードの前のパディングと判断。
					これでいいのか？
			*/
			for(colidx = fieldTotalSize; colidx < recordSize; colidx++)
				neReadChar(rfp);

			for(colidx = 0; colidx < getCount(fieldSizes); colidx++)
			{
				char *line = ab_makeLine_x(neReadBinaryBlock(rfp, getElement(fieldSizes, colidx)));

				line2JLine(line, 1, 0, 0, 1); // todo: 全部文字列扱い。

				if(colidx)
					writeChar(wfp, ',');

				writeCSVCell_x(wfp, line);
			}
			writeChar(wfp, '\n');
		}
	}

	errorCase(neReadChar(rfp) != 0x1a); // ? ターミネータ // todo: こんなのあるの？

	errorCase(readChar(rfp) != EOF); // ? まだファイルの途中

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
