mkdir new
mkdir new\patches
rd /S /Q new\patches\chat
mkdir new\patches\chat

HyoutaTools tovfps4e chat.svo
HyoutaTools tovfps4e new\chat.svo

FOR %%G IN (chat.svo.ext\VC*.DAT) DO (
HyoutaTools tlzc -d chat.svo.ext\%%~nG.DAT chat.svo.ext\%%~nG.EX
HyoutaTools tovfps4e chat.svo.ext\%%~nG.EX
HyoutaTools tlzc -d new\chat.svo.ext\%%~nG.DAT new\chat.svo.ext\%%~nG.EX
HyoutaTools tovfps4e new\chat.svo.ext\%%~nG.EX

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chat.svo.ext\%%~nG.EX.ext\0003" "new\chat.svo.ext\%%~nG.EX.ext\0003" "new\patches\chat\%%~nxG.xdelta3" 

del /F /Q chat.svo.ext\%%~nG.EX
del /F /Q chat.svo.ext\%%~nG.EX.new
rd /S /Q chat.svo.ext\%%~nG.EX.ext
del /F /Q new\chat.svo.ext\%%~nG.EX
del /F /Q new\chat.svo.ext\%%~nG.EX.new
rd /S /Q new\chat.svo.ext\%%~nG.EX.ext
)

rd /S /Q chat.svo.ext
rd /S /Q new\chat.svo.ext