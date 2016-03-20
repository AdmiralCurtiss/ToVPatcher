rd /S /Q "new\chat.svo.ext.raw"

HyoutaTools ToVfps4e new\chat.svo "new\chat.svo.ext.raw"

rd /S /Q "d:\Dropbox\ToV\PS3\mod\chat.svo.ext"
mkdir "d:\Dropbox\ToV\PS3\mod\chat.svo.ext"

FOR %%G IN (new\chat.svo.ext.raw\VC*) DO (
	HyoutaTools tlzc -d "%%G" "%%G.dec"
	HyoutaTools ToVfps4e "%%G.dec" "%%G.dec.ext"
	mkdir "d:\Dropbox\ToV\PS3\mod\chat.svo.ext\%%~nG.DAT.dec.ext"
	copy /b "%%G.dec.ext\0003" "d:\Dropbox\ToV\PS3\mod\chat.svo.ext\%%~nG.DAT.dec.ext\0003"
)

rd /S /Q "new\chat.svo.ext.raw"
