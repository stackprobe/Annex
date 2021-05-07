/*
	newest.txt        --  http://stackprobe.ccsp.mydns.jp:58946/newest.txt の内容をコピペして保存（或いはダウンロードして保存）
	公開ファイル一覧  --  Vectorにログイン -> 新規登録・登録ソフト一覧 -> 公開ファイル一覧 or 登録申請中ファイル一覧　このページをコピペして保存
*/

#include "C:\Factory\Common\all.h"

static autoList_t *HPApps;
static autoList_t *VtApps;
static autoList_t *BEApps;

static sint AppComp(uint v1, uint v2)
{
	char *a = (char *)v1;
	char *b = (char *)v2;

	return _strnicmp(a, b, 32);
}
static char *AppToRevision(char *app)
{
	char *revision = strx(ne_strchr(app, ' ') + 1);

	*ne_strchr(revision, ' ') = '\0';
	return revision;
}
static void CheckRevision(void)
{
	autoList_t *beApps1 = newList();
	autoList_t *beApps2 = newList();
	uint index;

	merge2_bothExist2 = beApps2;
	merge2(HPApps, VtApps, AppComp, NULL, beApps1, NULL);
	merge2_bothExist2 = NULL;

	for(index = 0; index < getCount(beApps1); index++)
	{
		char *app1 = getLine(beApps1, index);
		char *app2 = getLine(beApps2, index);
		char *rev1;
		char *rev2;

		rev1 = AppToRevision(app1);
		rev2 = AppToRevision(app2);

		if(strcmp(rev1, rev2))
		{
			cout("★★★リビジョンの更新忘れてるよ★★★\n");
			cout("%s\n", app1);
			cout("%s\n", app2);
		}
		memFree(rev1);
		memFree(rev2);
	}
	releaseAutoList(beApps1);
	releaseAutoList(beApps2);
}
static void PrintApps(autoList_t *lines, char *title)
{
	char *line;
	uint index;

	cout("==== %s\n", title);

	foreach(lines, line, index)
		cout("\t%s\n", line);

	cout("\n");
}
int main(int argc, char **argv)
{
	char *newestCppFile;
	char *vectorCppFile;
	FILE *fp;
	char *line;
	uint index;
	autoList_t *tokens;
	char *seLine = NULL;
	char *revision = NULL;

	hasArgs(0); // for //X opts

	cout("Drop newest.txt コピペ file\n");
	newestCppFile = dropFile();

	cout("Drop 公開ファイル一覧 page コピペ file\n");
	vectorCppFile = dropFile();

	HPApps = newList();
	fp = fileOpen(newestCppFile, "rt");

	while(line = readLine(fp))
	{
		trim(line, ' ');
		errorCase(!lineExp("<1,,__09AZaz> <1,,..09> <1,,09> <32,09AFaf>", line));
		tokens = tokenize(line, ' ');
		errorCase(getCount(tokens) != 4); // 2bs
		addElement(HPApps, (uint)xcout("%s %s %s %s", getLine(tokens, 3), getLine(tokens, 1), getLine(tokens, 2), getLine(tokens, 0)));
		releaseDim(tokens, 1);
		memFree(line);
	}
	fileClose(fp);

	VtApps = newList();
	fp = fileOpen(vectorCppFile, "rt");

	while(line = readLine(fp))
	{
		if(startsWith(line, "SE"))
		{
			errorCase(seLine);
			seLine = strx(line);
		}
		else if(startsWith(line, "PS"))
		{
			errorCase(revision);
			errorCase(!lineExp("PS<1,,09>\t<1,,..09>\t<>", line));
			revision = strx(ne_strchr(line, '\t') + 1);
			*ne_strchr(revision, '\t') = '\0';
		}
		else if(startsWith(line, "MD5\t"))
		{
			errorCase(!seLine);
			errorCase(!revision);
			errorCase(!lineExp("MD5\t<32,09AFaf>", line));
			addElement(VtApps, (uint)xcout("%s %s %s", line + 4, revision, seLine));
			memFree(seLine);
			memFree(revision);
			seLine = NULL;
			revision = NULL;
		}
		memFree(line);
	}
	fileClose(fp);
	errorCase(seLine);

	rapidSortLines(HPApps);
	rapidSortLines(VtApps);

	CheckRevision();

	BEApps = merge(HPApps, VtApps, AppComp, (void (*)(uint))memFree);

	PrintApps(HPApps, "HP (要アップデート + 要新規登録？)");
	PrintApps(VtApps, "Vector (要アップデート)");
	PrintApps(BEApps, "Both-exists");

	releaseDim(HPApps, 1);
	releaseDim(VtApps, 1);
	releaseDim(BEApps, 1);

	HPApps = NULL;
	VtApps = NULL;
	BEApps = NULL;
}
