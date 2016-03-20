cd chara.svo.ext

HyoutaTools tlzc -d EP_1320_060.DAT
HyoutaTools tovfps4e EP_1320_060.DAT.dec

HyoutaTools tovfps4e EP_1320_060.DAT.dec.ext\0002
del EP_1320_060.DAT.dec.ext\0002.ext\W_SWO_FRE_C001.0000
del EP_1320_060.DAT.dec.ext\0002.ext\W_SWO_F_05_01.0001
del EP_1320_060.DAT.dec.ext\0002.ext\KK_ROD_WIT_C000.0002
del EP_1320_060.DAT.dec.ext\0002.ext\KK_GLASSES_WIT_C000.0003
del EP_1320_060.DAT.dec.ext\0002.ext\KK_EV_77.0005
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m n EP_1320_060.DAT.dec.ext\0002.ext EP_1320_060.DAT.dec.ext\0002.new
move /Y EP_1320_060.DAT.dec.ext\0002.new EP_1320_060.DAT.dec.ext\0002
rd /S /Q EP_1320_060.DAT.dec.ext\0002.ext

copy /b /Y EP_1320_060.DAT.dec.ext\0003 EP_1320_060.DAT.dec.ext\0001

HyoutaTools tovfps4e EP_1320_060.DAT.dec.ext\0000
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\ENPC
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C000
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\CHEST
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\FOOT_L
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\FOOT_R
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\HAND_L
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\HAND_R
rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext\PC\FRE_C202\LEG
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m p EP_1320_060.DAT.dec.ext\0000.ext EP_1320_060.DAT.dec.ext\0000.new
move /Y EP_1320_060.DAT.dec.ext\0000.new EP_1320_060.DAT.dec.ext\0000

rd /S /Q EP_1320_060.DAT.dec.ext\0000.ext

REM YUR_C201
REM HyoutaTools tovfps4p -a 0x80 -b 0x0007 -e -s -m p YUR_C000.DAT.dec.ext YUR_C000.DAT.dec.new
REM HyoutaTools ByteFix YUR_C000.DAT.dec.new 18-00 19-00 1A-00 1B-58 58-59 59-55 5A-52 5B-5F 5C-43 5D-30 5E-30 5F-30

HyoutaTools tovfps4p -a 0x80 -b 0x0007 -e -s -m p EP_1320_060.DAT.dec.ext YUR_C201.DAT.dec.new
HyoutaTools ByteFix YUR_C201.DAT.dec.new 18-00 19-00 1A-00 1B-58 58-59 59-55 5A-52 5B-5F 5C-43 5D-32 5E-30 5F-31

HyoutaTools tlzc -c YUR_C201.DAT.dec.new YUR_C201.DAT

del EP_1320_060.DAT.dec
del YUR_C201.DAT.dec.new
rd /S /Q EP_1320_060.DAT.dec.ext

cd..
