テスト実行

	run Hub Example Test01 ;

----
動画作成

	run Hub pl = new PictureList ; ^
		Hub . Set pl ImageFile イメージファイル ; ^
		Hub . Set pl Title1 映画公開日など ; ^
		Hub . Set pl Title2 映画タイトルなど ; ^
		Hub . Set pl Title3 タイトルのタイトルなど ; ^
		Hub . Set pl Title4 タイトルなど ; ^
		mm = new MovieMaker ; ^
		Hub . Set mm AudioFile 音楽ファイル ; ^
		Hub . Set mm MovieFile 動画ファイル ; ^
		Hub . Set mm PicList *pl ; ^
		mm . Perform ;

	実行例 ...

	run Hub pl = new PictureList ; ^
		Hub . Set pl ImageFile res\打上花火.jpg ; ^
		Hub . Set pl Title1 ２０１７年８月１８日公開 ; ^
		Hub . Set pl Title2 映画『打ち上げ花火、下から見るか？横から見るか？』 ; ^
		Hub . SetInt pl Title2Size 42 ; ^
		Hub . Set pl Title3 主題歌 ; ^
		Hub . Set pl Title4 打上花火 ; ^
		mm = new MovieMaker ; ^
		Hub . Set mm AudioFile C:\etc\oto\CD\打上花火.mp3 ; ^
		Hub . Set mm MovieFile C:\temp\打上花火.mp4 ; ^
		Hub . Set mm PicList *pl ; ^
		mm . Perform ;
