----
カメラのリスト

	> KCamera.exe /L

	名前の書式 ... MonikerString + "*" + Name

----
開始

	> KCamera.exe [/C] [/M] [/E MonitorCount DiffMagnifBorder VMMM] カメラの名前 出力先 画像クオリティ しきい値 予備フレーム数 遅延比較フレーム数

		/C                 ... 出力先をクリアする。
		/M                 ... 差分評価値をモニターする。
		MonitorCount       ... 1 ～ INF (整数)
		DiffMagnifBorder   ... 1 ～ INF (整数)
		VMMM               ... 0 ～ INF (double)
		カメラの名前       ... カメラのリストで表示された名前との部分一致
		出力先             ... ディレクトリ。存在しなければ作成する。
		画像クオリティ     ... 0 ～ 101 (整数)
		しきい値           ... 0 ～ INF (double)
		予備フレーム数     ... 1 ～ INF (整数) 検知したフレームの前後の保存するフレーム数
		遅延比較フレーム数 ... 1 ～ INF (整数)

実行例

	> START "KCamera" /MIN KCamera.exe /C /M /E 5 50 1.0E-15 *Venus C:\tmp\KCamera 90 0.00002 50 20

----
終了

	> KCamera.exe /S

	何かキーが押された (キーバッファが空ではなくなった) 場合も終了します。
