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
	Hub . Set pl Singer1 "ＤＡＯＫＯ × 米津玄師" ; ^
	Hub . Set pl Singer2 "" ; ^
	Hub . SetInt pl SingerCurtainHPct 20 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\打上花火.mp3 ; ^
	Hub . Set mm MovieFile out\movies\uchiagehanabi.mp4 ; ^
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
	Hub . Set pl Singer1 スタァライト九九組 ; ^
	Hub . Set pl Singer2 小山百代・三森すずこ・富田麻帆・佐藤日向・岩田陽葵・小泉萌香・相羽あいな・生田輝・伊藤彩沙 ; ^
	Hub . SetInt pl Singer2Size 32 ; ^
	Hub . SetInt pl SingerCurtainHPct 27 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\少女歌劇レヴュースタァライト\星のダイアローグ.mp3 ; ^
	Hub . Set mm MovieFile out\movies\スタァライト.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream.png ; ^
	Hub . Set pl Title1 ２０１９年１月３日～３月２８日放送 ; ^
	Hub . Set pl Title2 "アニメ『BanG Dream! 2nd Season』" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 オープニングテーマ ; ^
	Hub . Set pl Title4 キズナミュージック♪ ; ^
	Hub . SetInt pl Title4Size 150 ; ^
	Hub . SetInt pl HalfCurtainWPct 92 ; ^
	Hub . Set pl Singer1 Poppin'Party ; ^
	Hub . Set pl Singer2 愛美・大塚紗英・西本りみ・大橋彩香・伊藤彩沙 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\バンドリ\キズナミュージック♪.mp3 ; ^
	Hub . Set mm MovieFile out\movies\バンドリ_キズナミュージック.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\交響詩篇エウレカセブンハイエボリューション1.png ; ^
	Hub . Set pl Title1 ２０１７年９月１６日公開 ; ^
	Hub . Set pl Title2 "映画『交響詩篇エウレカセブン　ハイエボリューション１』" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 主題歌 ; ^
	Hub . Set pl Title4 "Glory Days" ; ^
	Hub . SetInt pl HalfCurtainWPct 85 ; ^
	Hub . Set pl Singer1 尾崎裕哉 ; ^
	Hub . Set pl Singer2 "" ; ^
	Hub . SetInt pl SingerCurtainHPct 20 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\Glory Days.mp3" ; ^
	Hub . Set mm MovieFile "out\movies\Glory Days.mp4" ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream\Roselia.png ; ^
	Hub . Set pl Title1 ２０１９年１月３日～３月２８日放送 ; ^
	Hub . Set pl Title2 "アニメ『BanG Dream! 2nd Season』" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 オープニングテーマ② ; ^
	Hub . Set pl Title4 "BRAVE JEWEL" ; ^
	Hub . SetInt pl Title4Size 210 ; ^
	Hub . SetInt pl HalfCurtainWPct 92 ; ^
	Hub . Set pl Singer1 Roselia ; ^
	Hub . Set pl Singer2 相羽あいな・工藤晴香・中島由貴・櫻川めぐ・志崎樺音 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\バンドリ\BRAVE JEWEL.mp3" ; ^
	Hub . Set mm MovieFile out\movies\バンドリ_BraveJewel.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream\RAS_HeavenandEarth.jpg ; ^
	Hub . Set pl Title1 ２０１９年１月３日～３月２８日放送 ; ^
	Hub . Set pl Title2 "アニメ『BanG Dream! 2nd Season』" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 "第１０話　挿入歌" ; ^
	Hub . Set pl Title4 R･I･O･T ; ^
	Hub . Set pl Singer1 "RAISE A SUILEN" ; ^
	Hub . Set pl Singer2 Raychell・小原莉子・夏芽・倉知玲鳳・紡木吏佐 ; ^
	Hub . SetInt pl SingerCurtainAPct 80 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\バンドリ\R・I・O・T.mp3" ; ^
	Hub . Set mm MovieFile out\movies\バンドリ_Riot.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;
