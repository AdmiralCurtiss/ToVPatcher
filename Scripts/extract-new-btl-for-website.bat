rd /S /Q "new\btl.svo.ext"

HyoutaTools tovfps4e new\btl.svo
HyoutaTools tovfps4e new\btl.svo.ext\BTL_PACK.DAT
HyoutaTools tovfps4e new\btl.svo.ext\BTL_PACK.DAT.ext\0003 new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext

rd /S /Q "d:\Dropbox\ToV\PS3\mod\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext"
mkdir "d:\Dropbox\ToV\PS3\mod\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext"

FOR %%G IN (new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\AI_*) DO del "%%G"
FOR %%G IN (new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\EV_*) DO del "%%G"
FOR %%G IN (new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\MA_*) DO del "%%G"
FOR %%G IN (new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\*) DO (
	HyoutaTools tlzc -d "%%G" "d:\Dropbox\ToV\PS3\mod\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\%%~nG%%~xG.dec"
)

rd /S /Q "new\btl.svo.ext"
