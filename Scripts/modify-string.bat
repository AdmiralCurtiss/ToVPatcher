HyoutaTools tovfps4e string.svo
move string.svo.ext\STRING_DIC.SO STRING_DIC.SO
REM note: points EN pointers at null, shouldn't cause issues but noting it here just in case
HyoutaTools GraceNote.Vesperia.StringDicExport
move STRING_DIC_translated.SO string.svo.ext\STRING_DIC.SO
del /F /Q STRING_DIC.SO
HyoutaTools tovfps4p -o string.svo -a 0x800 string.svo.ext string.svo.new
rd /S /Q string.svo.ext

mkdir new
move string.svo.new new\string.svo