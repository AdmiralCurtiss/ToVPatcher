mkdir oldchat
mkdir newchat
HyoutaTools tovfps4e chat.svo
ren chat.svo.ext chat.filenames
del /F /Q chat.filenames\chat.dat
del /F /Q chat.filenames\chat.SO
ren chat.filenames\*.dat *.
HyoutaTools tovfps4e chat.svo
FOR /R %%G IN (chat.filenames\*) DO (
HyoutaTools tlzc -d chat.svo.ext\%%~nG.DAT chat.svo.ext\%%~nG.EX
HyoutaTools tovfps4e chat.svo.ext\%%~nG.EX
copy /B chat.svo.ext\%%~nG.EX.ext\0003 oldchat\%%~nG
GraceNote_TO8CHTX oldchat\%%~nG db\%%~nG newchat\%%~nG
copy /B newchat\%%~nG chat.svo.ext\%%~nG.EX.ext\0003
HyoutaTools tovfps4p -a 0x80 -o chat.svo.ext\%%~nG.EX chat.svo.ext\%%~nG.EX.ext chat.svo.ext\%%~nG.EX.new
HyoutaTools tlzc -c chat.svo.ext\%%~nG.EX.new chat.svo.ext\%%~nG.DAT
del /F /Q chat.svo.ext\%%~nG.EX
del /F /Q chat.svo.ext\%%~nG.EX.new
rd /S /Q chat.svo.ext\%%~nG.EX.ext
)
rd /S /Q chat.filenames
HyoutaTools tovfps4p -a 0x800 -o chat.svo chat.svo.ext chat.svo.new

rd /S /Q oldchat
rd /S /Q newchat
rd /S /Q chat.svo.ext

mkdir new
move chat.svo.new new\chat.svo