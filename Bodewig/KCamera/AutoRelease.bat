IF NOT EXIST KCamera_is_here.sig GOTO END
CLS
rem リリースして qrum します。
PAUSE

CALL newcsrr

CALL qq
cx **

CALL _Release.bat /-P

MOVE out\KCamera.zip S:\リリース物\.

START "" /B /WAIT /DC:\home\bat syncRev

CALL qrumauto rel

rem **** AUTO RELEASE COMPLETED ****

:END
