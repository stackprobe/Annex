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
	Hub . Set pl Singer1 "�c�`�n�j�n �~ �ĒÌ��t" ; ^
	Hub . Set pl Singer2 "" ; ^
	Hub . SetInt pl SingerCurtainHPct 20 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�ŏ�ԉ�.mp3 ; ^
	Hub . Set mm MovieFile out\movies\uchiagehanabi.mp4 ; ^
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
	Hub . Set pl Singer1 �X�^�@���C�g���g ; ^
	Hub . Set pl Singer2 ���R�S��E�O�X�������E�x�c�����E���������E��c�z���E����G���E���H�����ȁE���c�P�E�ɓ��ʍ� ; ^
	Hub . SetInt pl Singer2Size 32 ; ^
	Hub . SetInt pl SingerCurtainHPct 27 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�����̌��������[�X�^�@���C�g\���̃_�C�A���[�O.mp3 ; ^
	Hub . Set mm MovieFile out\movies\�X�^�@���C�g.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream.png ; ^
	Hub . Set pl Title1 �Q�O�P�X�N�P���R���`�R���Q�W������ ; ^
	Hub . Set pl Title2 "�A�j���wBanG Dream! 2nd Season�x" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 �I�[�v�j���O�e�[�} ; ^
	Hub . Set pl Title4 �L�Y�i�~���[�W�b�N�� ; ^
	Hub . SetInt pl Title4Size 150 ; ^
	Hub . SetInt pl HalfCurtainWPct 92 ; ^
	Hub . Set pl Singer1 Poppin'Party ; ^
	Hub . Set pl Singer2 �����E��ˎщp�E���{��݁E�勴�ʍ��E�ɓ��ʍ� ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile C:\etc\oto\CD\�o���h��\�L�Y�i�~���[�W�b�N��.mp3 ; ^
	Hub . Set mm MovieFile out\movies\�o���h��_�L�Y�i�~���[�W�b�N.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\�������уG�E���J�Z�u���n�C�G�{�����[�V����1.png ; ^
	Hub . Set pl Title1 �Q�O�P�V�N�X���P�U�����J ; ^
	Hub . Set pl Title2 "�f��w�������уG�E���J�Z�u���@�n�C�G�{�����[�V�����P�x" ; ^
	Hub . SetInt pl Title2Size 48 ; ^
	Hub . Set pl Title3 ���� ; ^
	Hub . Set pl Title4 "Glory Days" ; ^
	Hub . SetInt pl HalfCurtainWPct 85 ; ^
	Hub . Set pl Singer1 ����T�� ; ^
	Hub . Set pl Singer2 "" ; ^
	Hub . SetInt pl SingerCurtainHPct 20 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\Glory Days.mp3" ; ^
	Hub . Set mm MovieFile "out\movies\Glory Days.mp4" ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream\Roselia.png ; ^
	Hub . Set pl Title1 �Q�O�P�X�N�P���R���`�R���Q�W������ ; ^
	Hub . Set pl Title2 "�A�j���wBanG Dream! 2nd Season�x" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 �I�[�v�j���O�e�[�}�A ; ^
	Hub . Set pl Title4 "BRAVE JEWEL" ; ^
	Hub . SetInt pl Title4Size 210 ; ^
	Hub . SetInt pl HalfCurtainWPct 92 ; ^
	Hub . Set pl Singer1 Roselia ; ^
	Hub . Set pl Singer2 ���H�����ȁE�H�������E�����R�M�E�N��߂��E�u�芒�� ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\�o���h��\BRAVE JEWEL.mp3" ; ^
	Hub . Set mm MovieFile out\movies\�o���h��_BraveJewel.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;

run Hub pl = new PictureList ; ^
	Hub . Set pl ImageFile res\BanGDream\RAS_HeavenandEarth.jpg ; ^
	Hub . Set pl Title1 �Q�O�P�X�N�P���R���`�R���Q�W������ ; ^
	Hub . Set pl Title2 "�A�j���wBanG Dream! 2nd Season�x" ; ^
	Hub . SetInt pl Title2Size 60 ; ^
	Hub . Set pl Title3 "��P�O�b�@�}����" ; ^
	Hub . Set pl Title4 R�I�O�T ; ^
	Hub . Set pl Singer1 "RAISE A SUILEN" ; ^
	Hub . Set pl Singer2 Raychell�E����从q�E�ĉ�E�q�m��P�E�a�ؗ��� ; ^
	Hub . SetInt pl SingerCurtainAPct 80 ; ^
	mm = new MovieMaker ; ^
	Hub . Set mm AudioFile "C:\etc\oto\CD\�o���h��\R�EI�EO�ET.mp3" ; ^
	Hub . Set mm MovieFile out\movies\�o���h��_Riot.mp4 ; ^
	Hub . Set mm PicList *pl ; ^
	mm . Perform ;
