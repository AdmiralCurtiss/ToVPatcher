HyoutaTools tovfps4e menu.svo

rem title modifications for unused costumes
copy /b assets\FAMEDATA.BIN menu.svo.ext\FAMEDATA.BIN

rem save data icon with removed katakana
copy /b assets\SAVEICONDATA.BIN menu.svo.ext\SAVEICONDATA.BIN

HyoutaTools tovfps4p -o menu.svo -a 0x800 menu.svo.ext menu.svo.new

rd /S /Q menu.svo.ext

mkdir new
move menu.svo.new new\menu.svo
