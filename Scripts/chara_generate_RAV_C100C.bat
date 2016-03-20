cd chara.svo.ext

SET source=EP_420_080
SET target=RAV_C100C

HyoutaTools tlzc -d %source%.DAT
HyoutaTools tovfps4e %source%.DAT.dec

REM extract the archives
HyoutaTools tovfps4e %source%.DAT.dec.ext\0000
HyoutaTools tovfps4e %source%.DAT.dec.ext\0001
HyoutaTools tovfps4e %source%.DAT.dec.ext\0002
HyoutaTools tovfps4e %source%.DAT.dec.ext\0003

REM remove files

rd /S /Q %source%.DAT.dec.ext\0000.ext\PC
rd /S /Q %source%.DAT.dec.ext\0000.ext\NPC
rd /S /Q %source%.DAT.dec.ext\0000.ext\ENPC\ARE_C000
rd /S /Q %source%.DAT.dec.ext\0000.ext\ENPC\AST_M000
del /Q %source%.DAT.dec.ext\0000.ext\ENPC\RAV_C100\CHEST\RAV_C100_CHEST.*

del /Q %source%.DAT.dec.ext\0001.ext\*
rd /S /Q %source%.DAT.dec.ext\0001.ext\MORPH
rd /S /Q %source%.DAT.dec.ext\0001.ext\ENEMY
rd /S /Q %source%.DAT.dec.ext\0001.ext\ENPC
rd /S /Q %source%.DAT.dec.ext\0001.ext\NPC
rd /S /Q %source%.DAT.dec.ext\0001.ext\PC

del /Q %source%.DAT.dec.ext\0002.ext\E_*
del /Q %source%.DAT.dec.ext\0002.ext\KK_EV_*
del /Q %source%.DAT.dec.ext\0002.ext\KK_RING*
REM missing KK_RAV_C001_01 ?
del /Q %source%.DAT.dec.ext\0002.ext\W_B*
del /Q %source%.DAT.dec.ext\0002.ext\W_S*
del /Q %source%.DAT.dec.ext\0002.ext\W_RAV_C001_00A*
del /Q %source%.DAT.dec.ext\0002.ext\W_RAV_C001_01*

del /Q %source%.DAT.dec.ext\0003.ext\*


REM pack them back up
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m p %source%.DAT.dec.ext\0000.ext %source%.DAT.dec.ext\0000.new
move /Y %source%.DAT.dec.ext\0000.new %source%.DAT.dec.ext\0000
rd /S /Q %source%.DAT.dec.ext\0000.ext
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m p %source%.DAT.dec.ext\0001.ext %source%.DAT.dec.ext\0001.new
move /Y %source%.DAT.dec.ext\0001.new %source%.DAT.dec.ext\0001
rd /S /Q %source%.DAT.dec.ext\0001.ext
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m n %source%.DAT.dec.ext\0002.ext %source%.DAT.dec.ext\0002.new
move /Y %source%.DAT.dec.ext\0002.new %source%.DAT.dec.ext\0002
rd /S /Q %source%.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -a 0x80 -b 0x0047 -e -s -m p %source%.DAT.dec.ext\0003.ext %source%.DAT.dec.ext\0003.new
move /Y %source%.DAT.dec.ext\0003.new %source%.DAT.dec.ext\0003
rd /S /Q %source%.DAT.dec.ext\0003.ext

HyoutaTools tovfps4p -a 0x80 -b 0x0007 -e -s -m p -c "%target%" %source%.DAT.dec.ext %target%.DAT.dec.new
HyoutaTools ByteFix JUD_C001_EVT01.DAT.dec.new 18-00 19-00 1A-00 1B-58 58-4A 59-55 5A-44 5B-5F 5C-43 5D-30 5E-30 5F-31 60-5F 61-45 62-56 63-54 64-30 65-31

HyoutaTools tlzc -c %target%.DAT.dec.new %target%.DAT
del %target%.DAT.dec.new
del %source%.DAT.dec

rd /S /Q %source%.DAT.dec.ext

cd..
