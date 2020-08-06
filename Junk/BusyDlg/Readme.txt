■起動方法


ダイアログを開く

	BusyDlg.exe 1 [メッセージ]


ダイアログを閉じる

	BusyDlg.exe 0


javaから呼び出す。

	private static final String PROGRAM = "C:/Dev/Annex/Junk/BusyDlg/BusyDlg/bin/Release/BusyDlg.exe";

	public static void openDlg() throws IOException, InterruptedException {
		Runtime.getRuntime().exec("cmd /c start \"\" /wait \"" + PROGRAM + "\" 1").waitFor();
	}

	public static void openDlg(String message) throws IOException, InterruptedException {
		Runtime.getRuntime().exec("cmd /c start \"\" /wait \"" + PROGRAM + "\" 1 \"" + message + "\"").waitFor();
	}

	public static void closeDlg() throws IOException, InterruptedException {
		Runtime.getRuntime().exec("cmd /c start \"\" /wait \"" + PROGRAM + "\" 0").waitFor();
	}

