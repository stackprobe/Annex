IF NOT EXIST Chroco_is_here.sig GOTO END
CLS
rem �����[�X���� qrum ���܂��B
PAUSE

CALL newcsrr

CALL ff
cx **

CD /D %~dp0.

IF NOT EXIST Chroco_is_here.sig GOTO END

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\Chroco.zip S:\�����[�X��\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
