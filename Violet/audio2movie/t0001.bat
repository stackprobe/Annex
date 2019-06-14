CALL _Clean
MD out\movies

run Hub mm = new MovieMaker ; ^
	Hub . Set mm MovieFile out\movies\Singing.mp4 ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\打上花火.jpg ; ^
	Hub . Set pl Title1 ２０１７年８月１８日公開 ; ^
	Hub . Set pl Title2 映画『打ち上げ花火、下から見るか？横から見るか？』 ; ^
	Hub . SetInt pl Title2Size 42 ; ^
	Hub . Set pl Title3 主題歌 ; ^
	Hub . Set pl Title4 打上花火 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\打上花火.mp3 ; ^
	Hub . Set mm MovieFile out\movies\打上花火.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\スタァライト.jpg ; ^
	Hub . Set pl Title1 ２０１８年７月１３日～９月２８日放送 ; ^
	Hub . Set pl Title2 "アニメ『少女☆歌劇　レヴュースタァライト』" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 オープニングテーマ ; ^
	Hub . Set pl Title4 星のダイアローグ ; ^
	Hub . SetInt pl Title4Size 192 ; ^
	Hub . SetInt pl HalfCurtainWPct 95 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\少女歌劇レヴュースタァライト\星のダイアローグ.mp3 ; ^
	Hub . Set mm MovieFile out\movies\スタァライト.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream2ndSeason.png ; ^
	Hub . Set pl Title1 ２０１９年１月３日～３月２８日放送 ; ^
	Hub . Set pl Title2 "アニメ『BanG Dream! 2nd Season』" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 オープニングテーマ ; ^
	Hub . Set pl Title4 キズナミュージック♪ ; ^
	Hub . SetInt pl Title4Size 150 ; ^
	Hub . SetInt pl HalfCurtainWPct 90 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\バンドリ\キズナミュージック♪.mp3 ; ^
	Hub . Set mm MovieFile out\movies\キズナミュージック.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\交響詩篇エウレカセブンハイエボリューション1.jpg ; ^
	Hub . Set pl Title1 ２０１７年９月１６日公開 ; ^
	Hub . Set pl Title2 "映画『交響詩篇エウレカセブン　ハイエボリューション１』" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 主題歌 ; ^
	Hub . Set pl Title4 "Glory Days" ; ^
	Hub . SetInt pl HalfCurtainWPct 85 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\Glory Days.mp3" ; ^
	Hub . Set mm MovieFile "out\movies\Glory Days.mp4" ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;
