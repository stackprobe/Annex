IF NOT EXIST KCamera_is_here.sig GOTO END
CLS
rem �����[�X���� qrum ���܂��B
PAUSE

CALL newcsrr

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\KCamera.zip S:\�����[�X��\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
