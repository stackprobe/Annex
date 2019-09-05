カメラのリスト

	KCamera.exe /L

開始

	KCamera.exe [/M] カメラの名前 出力先 画像クオリティ しきい値

		/M             ... 差分評価値をモニターする。
		カメラの名前   ... 部分一致
		出力先         ... ディレクトリ。存在しなければ作成する。
		画像クオリティ ... 0 ～ 101 (整数)
		しきい値       ... 0 ～ INF (double)

	実行例)

	START "KCamera" /MIN KCamera.exe /M Venus C:\tmp\KCamera 90 0.00001

終了

	KCamera.exe /S
