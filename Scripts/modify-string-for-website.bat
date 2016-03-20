HyoutaTools tovfps4e string.svo
move string.svo.ext\STRING_DIC.SO STRING_DIC.SO
HyoutaTools GraceNote.Vesperia.StringDicExport -real
move STRING_DIC_translated.SO string.svo.ext\STRING_DIC.SO
del /F /Q STRING_DIC.SO
HyoutaTools tovfps4p -o string.svo -a 0x800 string.svo.ext string.svo.new
rd /S /Q string.svo.ext

mkdir website
move string.svo.new website\string.svo

HyoutaTools tovfps4e website\string.svo
move website\string.svo.ext\STRING_DIC.SO website\STRING_DIC.SO
copy /b "website\STRING_DIC.SO" "d:\Dropbox\ToV\PS3\mod\string.svo.ext\STRING_DIC.SO"
rd /S /Q website\string.svo.ext
