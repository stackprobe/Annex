#include "all.h"

static void Test_autoList(void)
{
	{
		autoList<int> *list = new autoList<int>();

		list->AddRepeat(0, 3);

		list->SetElement(0, 1);
		list->SetElement(1, 2);
		list->SetElement(2, 3);

		errorCase(list->GetElement(0) != 1);
		errorCase(list->GetElement(1) != 2);
		errorCase(list->GetElement(2) != 3);

		delete list;
	}

	{
		autoList<int> *list = new autoList<int>();

		list->AddElement(1);
		list->AddElement(2);
		list->AddElement(3);

		errorCase(list->UnaddElement() != 3);
		errorCase(list->UnaddElement() != 2);
		errorCase(list->UnaddElement() != 1);

		delete list;
	}

	{
		autoList<int> *list = new autoList<int>();

		list->InsertElement(0, 1);
		list->InsertElement(1, 2);
		list->InsertElement(2, 3);
		list->InsertElement(3, 4);

		list->InsertElement(2, 103);
		list->InsertElement(2, 102);
		list->InsertElement(2, 101);

		errorCase(list->DesertElement(6) != 4);
		errorCase(list->DesertElement(5) != 3);

		errorCase(list->DesertElement(2) != 101);
		errorCase(list->DesertElement(2) != 102);
		errorCase(list->DesertElement(2) != 103);

		errorCase(list->DesertElement(0) != 1);
		errorCase(list->DesertElement(0) != 2);

		delete list;
	}
}
static void Test_tokenize(void)
{
	char *lines[] =
	{
		"",
		"a",
		"/",
		"abc",
		"///",
		"///aaa///",
		":a:b:c:d:e:f:",
		"abc///////def",
		"http://www.google.com/index.html",
	};
	char *DLMTRS = ":/.";
	char *SPRTR = ":";

	for(int index = 0; index < lengthof(lines); index++)
	{
		autoList<char *> *tokens = tokenize(lines[index], DLMTRS);

		for(int tknidx = 0; tknidx < tokens->GetCount(); tknidx++)
			cout("token[%d]: [%s]\n", tknidx, tokens->GetElement(tknidx));

		char *line1 = untokenize(tokens, SPRTR);
		char *line2 = strx(lines[index]);

		for(char *d = DLMTRS; *d; d++)
			replaceChar(line2, *d, SPRTR[0]);

		cout("line1: [%s]\n", line1);
		cout("line2: [%s]\n", line2);

		errorCase(strcmp(line1, line2)); // ? line1 != line2

		tokens->CallAllElement((void (*)(char *))memFree);
		delete tokens;
		memFree(line1);
		memFree(line2);
	}
}
static void Test_replaceLine(void)
{
	struct
	{
		char *Line;
		char *Ptn1;
		char *Ptn2;
		char *AnsLine;
	}
	infos[] =
	{
		{ "ABCDEFGHI", "ABC", "-=+=-", "-=+=-DEFGHI" },
		{ "ABCDEFGHI", "DEF", "-=+=-", "ABC-=+=-GHI" },
		{ "ABCDEFGHI", "GHI", "-=+=-", "ABCDEF-=+=-" },
	};

	for(int index = 0; index < lengthof(infos); index++)
	{
		char *line = replaceLine(strx(infos[index].Line), infos[index].Ptn1, infos[index].Ptn2);

		cout("line1: %s\n", infos[index].AnsLine);
		cout("line2: %s\n", line);

		errorCase(strcmp(line, infos[index].AnsLine)); // ? •sˆê’v
		memFree(line);
	}
}
static void Test_csv(void)
{
	autoTable<char *> *table = readCsvFile("..\\..\\Data\\Test.csv");

	for(int x = 0; x < table->GetWidth(); x++)
	for(int y = 0; y < table->GetHeight(); y++)
	{
		cout("(%d %d): %s\n", x, y, table->GetCell(x, y));
	}
	delete table;
}
static void Test_existFile_existDir(void)
{
	char *paths[] =
	{
		"",
		"*",
		"?",
		".",
		"..",
		"C:\\",
		"D:\\",
		"E:\\",
		"F:\\",
		"C:\\Dev",
		"C:\\Dev_",
		"C:\\Dat\\Dummy.png",
		"C:\\Dat\\Dummy.png_",
	};

	for(int index = 0; index < lengthof(paths); index++)
	{
		cout("[%s] -> %d %d %d\n", paths[index], existPath(paths[index]), existFile(paths[index]), existDir(paths[index]));
	}
}
static int CompInt(int v1, int v2)
{
	return v1 - v2;
}
static void Test_combSort(void)
{
	LOGPOS();

	srand((uint)time(NULL));
	
	for(int c = 0; c < 10000; c++)
	{
		autoList<int> *list = new autoList<int>();
		int n = rand() % 10000;

		for(int i = 0; i < n; i++)
			list->AddElement(rand() % 10000);

		combSort(list, CompInt);

		for(int i = 1; i < n; i++)
			errorCase(list->GetElement(i) < list->GetElement(i - 1));

		delete list;
	}
	LOGPOS();
}
static void Test_sortedList(void)
{
	LOGPOS();

	srand((uint)time(NULL));

	for(int c = 0; c < 10000; c++)
	{
		sortedList<int> *list = new sortedList<int>(CompInt);
		int n = rand() % 10000;

		for(int i = 0; i < n; i++)
			list->AddElement(rand() % 10000);

		for(int tc = 0; tc < 100; tc++)
		{
			int target = rand() % 10000;
			int index = list->GetIndex(target);
			
//			cout("target_index: %d, %d\n", target, index);

			if(index == -1)
			{
				for(int i = 0; i < n; i++)
					errorCase(list->GetElement(i) == target);
			}
			else
				errorCase(list->GetElement(index) != target);
		}
		delete list;
	}
	LOGPOS();
}
int main(int argc, char **argv)
{
	Test_autoList();
	Test_tokenize();
	Test_replaceLine();
	Test_csv();
	Test_existFile_existDir();
	TFuncTest();
	Test_combSort();
	Test_sortedList();

	cout("OK!\n");

	return 0;
}
