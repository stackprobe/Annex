CALL _Clean
MD out\movies

run Hub mm = new MovieMaker ; ^
	Hub . Set mm MovieFile out\movies\Singing.mp4 ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\�ŏ�ԉ�.jpg ; ^
	Hub . Set pl Title1 �Q�O�P�V�N�W���P�W�����J ; ^
	Hub . Set pl Title2 �f��w�ł��グ�ԉ΁A�����猩�邩�H�����猩�邩�H�x ; ^
	Hub . SetInt pl Title2Size 42 ; ^
	Hub . Set pl Title3 ���� ; ^
	Hub . Set pl Title4 �ŏ�ԉ� ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�ŏ�ԉ�.mp3 ; ^
	Hub . Set mm MovieFile out\movies\�ŏ�ԉ�.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\�X�^�@���C�g.jpg ; ^
	Hub . Set pl Title1 �Q�O�P�W�N�V���P�R���`�X���Q�W������ ; ^
	Hub . Set pl Title2 "�A�j���w�������̌��@�������[�X�^�@���C�g�x" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 �I�[�v�j���O�e�[�} ; ^
	Hub . Set pl Title4 ���̃_�C�A���[�O ; ^
	Hub . SetInt pl Title4Size 192 ; ^
	Hub . SetInt pl HalfCurtainWPct 95 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�����̌��������[�X�^�@���C�g\���̃_�C�A���[�O.mp3 ; ^
	Hub . Set mm MovieFile out\movies\�X�^�@���C�g.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream2ndSeason.png ; ^
	Hub . Set pl Title1 �Q�O�P�X�N�P���R���`�R���Q�W������ ; ^
	Hub . Set pl Title2 "�A�j���wBanG Dream! 2nd Season�x" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 �I�[�v�j���O�e�[�} ; ^
	Hub . Set pl Title4 �L�Y�i�~���[�W�b�N�� ; ^
	Hub . SetInt pl Title4Size 150 ; ^
	Hub . SetInt pl HalfCurtainWPct 90 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�o���h��\�L�Y�i�~���[�W�b�N��.mp3 ; ^
	Hub . Set mm MovieFile out\movies\�L�Y�i�~���[�W�b�N.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\�������уG�E���J�Z�u���n�C�G�{�����[�V����1.jpg ; ^
	Hub . Set pl Title1 �Q�O�P�V�N�X���P�U�����J ; ^
	Hub . Set pl Title2 "�f��w�������уG�E���J�Z�u���@�n�C�G�{�����[�V�����P�x" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 ���� ; ^
	Hub . Set pl Title4 "Glory Days" ; ^
	Hub . SetInt pl HalfCurtainWPct 85 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\Glory Days.mp3" ; ^
	Hub . Set mm MovieFile "out\movies\Glory Days.mp4" ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;
