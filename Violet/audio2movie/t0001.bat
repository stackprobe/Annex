CALL _Clean
MD out\movies

run Hub mm = new MovieMaker ; ^
	Hub . Set mm MovieFile out\movies\Singing.mp4 ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\ΕγΤΞ.jpg ; ^
	Hub . Set pl Title1 QOPVNWPWϊφJ ; ^
	Hub . Set pl Title2 fζwΕΏγ°ΤΞAΊ©η©ι©H‘©η©ι©Hx ; ^
	Hub . SetInt pl Title2Size 42 ; ^
	Hub . Set pl Title3 εθΜ ; ^
	Hub . Set pl Title4 ΕγΤΞ ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\ΕγΤΞ.mp3 ; ^
	Hub . Set mm MovieFile out\movies\ΕγΤΞ.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\X^@Cg.jpg ; ^
	Hub . Set pl Title1 QOPWNVPRϊ`XQWϊϊ ; ^
	Hub . Set pl Title2 "Ajw­Μ@[X^@Cgx" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 I[vjOe[} ; ^
	Hub . Set pl Title4 ―Μ_CA[O ; ^
	Hub . SetInt pl Title4Size 192 ; ^
	Hub . SetInt pl HalfCurtainWPct 95 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\­Μ[X^@Cg\―Μ_CA[O.mp3 ; ^
	Hub . Set mm MovieFile out\movies\X^@Cg.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream2ndSeason.png ; ^
	Hub . Set pl Title1 QOPXNPRϊ`RQWϊϊ ; ^
	Hub . Set pl Title2 "AjwBanG Dream! 2nd Seasonx" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 I[vjOe[} ; ^
	Hub . Set pl Title4 LYi~[WbNτ ; ^
	Hub . SetInt pl Title4Size 150 ; ^
	Hub . SetInt pl HalfCurtainWPct 90 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\oh\LYi~[WbNτ.mp3 ; ^
	Hub . Set mm MovieFile out\movies\LYi~[WbN.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\πΏΡGEJZunCG{[V1.jpg ; ^
	Hub . Set pl Title1 QOPVNXPUϊφJ ; ^
	Hub . Set pl Title2 "fζwπΏΡGEJZu@nCG{[VPx" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 εθΜ ; ^
	Hub . Set pl Title4 "Glory Days" ; ^
	Hub . SetInt pl HalfCurtainWPct 85 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\Glory Days.mp3" ; ^
	Hub . Set mm MovieFile "out\movies\Glory Days.mp4" ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;
