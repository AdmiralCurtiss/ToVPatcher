HyoutaTools tovfps4e chara.svo

REM copies custom skill tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT chara.svo.ext\EP_050_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_050_010.DAT.dec chara.svo.ext\EP_050_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_050_010.DAT.dec.ext\0002 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec
HyoutaTools BlockCopy assets\E_A031_SKILL_01.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009
del chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec
HyoutaTools BlockCopy assets\E_A032_SKILL_02_1.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec 180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_2.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec E1180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_3.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec 1C2180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_4.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec 2A3180 auto
HyoutaTools tlzc -c chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010
del chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec
HyoutaTools BlockCopy assets\E_A033_SKILL_03.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011
del chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec
HyoutaTools BlockCopy assets\E_A063_SKILL_NA01.TXV 0 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012
del chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_050_010.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext chara.svo.ext\EP_050_010.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_050_010.DAT.dec.ext\0002.new chara.svo.ext\EP_050_010.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_050_010.DAT.dec -a 0x80 chara.svo.ext\EP_050_010.DAT.dec.ext chara.svo.ext\EP_050_010.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_050_010.DAT.dec.new chara.svo.ext\EP_050_010.DAT
del chara.svo.ext\EP_050_010.DAT.dec
del chara.svo.ext\EP_050_010.DAT.dec.new
rd /S /Q chara.svo.ext\EP_050_010.DAT.dec.ext



REM copies custom cooking tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT chara.svo.ext\EP_060_040.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_060_040.DAT.dec chara.svo.ext\EP_060_040.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_060_040.DAT.dec.ext\0002 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools BlockCopy assets\E_A057_COOK01.TXV 0 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001
del chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools BlockCopy assets\E_A059_COOK03.TXV 0 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002
del chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec
HyoutaTools BlockCopy assets\E_A060_COOK04.TXV 0 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003
del chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec
HyoutaTools BlockCopy assets\E_A061_COOK05.TXV 0 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004
del chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_060_040.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext chara.svo.ext\EP_060_040.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_060_040.DAT.dec.ext\0002.new chara.svo.ext\EP_060_040.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_060_040.DAT.dec -a 0x80 chara.svo.ext\EP_060_040.DAT.dec.ext chara.svo.ext\EP_060_040.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_060_040.DAT.dec.new chara.svo.ext\EP_060_040.DAT
del chara.svo.ext\EP_060_040.DAT.dec
del chara.svo.ext\EP_060_040.DAT.dec.new
rd /S /Q chara.svo.ext\EP_060_040.DAT.dec.ext





REM skill tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT chara.svo.ext\EP_0080_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_0080_010.DAT.dec chara.svo.ext\EP_0080_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_0080_010.DAT.dec.ext\0002 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools BlockCopy assets\E_A031_SKILL_01.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000
del chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools BlockCopy assets\E_A032_SKILL_02_1.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec 180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_2.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec E1180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_3.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec 1C2180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_4.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec 2A3180 auto
HyoutaTools tlzc -c chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001
del chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools BlockCopy assets\E_A033_SKILL_03.TXV 0 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec 100 auto
HyoutaTools tlzc -c chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002
del chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_0080_010.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.new chara.svo.ext\EP_0080_010.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_0080_010.DAT.dec -a 0x80 chara.svo.ext\EP_0080_010.DAT.dec.ext chara.svo.ext\EP_0080_010.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_0080_010.DAT.dec.new chara.svo.ext\EP_0080_010.DAT
del chara.svo.ext\EP_0080_010.DAT.dec
del chara.svo.ext\EP_0080_010.DAT.dec.new
rd /S /Q chara.svo.ext\EP_0080_010.DAT.dec.ext



REM dice minigame images
HyoutaTools tlzc -d chara.svo.ext\EP_0670_010.DAT chara.svo.ext\EP_0670_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_0670_010.DAT.dec chara.svo.ext\EP_0670_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_0670_010.DAT.dec.ext\0002 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005.dec
HyoutaTools BlockCopy chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005.dec 100 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005.dec 100100 100000
HyoutaTools tlzc -c chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005.dec chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005
del chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0005.dec
HyoutaTools tlzc -d chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006.dec
HyoutaTools BlockCopy chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006.dec 100 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006.dec 100100 100000
HyoutaTools tlzc -c chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006.dec chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006
del chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext\0006.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_0670_010.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.new chara.svo.ext\EP_0670_010.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_0670_010.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_0670_010.DAT.dec -a 0x80 chara.svo.ext\EP_0670_010.DAT.dec.ext chara.svo.ext\EP_0670_010.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_0670_010.DAT.dec.new chara.svo.ext\EP_0670_010.DAT
del chara.svo.ext\EP_0670_010.DAT.dec
del chara.svo.ext\EP_0670_010.DAT.dec.new
rd /S /Q chara.svo.ext\EP_0670_010.DAT.dec.ext




REM snowboarding minigame tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_1440_010.DAT chara.svo.ext\EP_1440_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_1440_010.DAT.dec chara.svo.ext\EP_1440_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_1440_010.DAT.dec.ext\0002 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 200000 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec 100 80000
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 280000 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec 80100 80000
HyoutaTools tlzc -c chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000
del chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_1440_010.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.new chara.svo.ext\EP_1440_010.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_1440_010.DAT.dec -a 0x80 chara.svo.ext\EP_1440_010.DAT.dec.ext chara.svo.ext\EP_1440_010.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_1440_010.DAT.dec.new chara.svo.ext\EP_1440_010.DAT
del chara.svo.ext\EP_1440_010.DAT.dec
del chara.svo.ext\EP_1440_010.DAT.dec.new
rd /S /Q chara.svo.ext\EP_1440_010.DAT.dec.ext





HyoutaTools tlzc -d chara.svo.ext\EP_1440_020.DAT chara.svo.ext\EP_1440_020.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_1440_020.DAT.dec chara.svo.ext\EP_1440_020.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_1440_020.DAT.dec.ext\0002 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 200000 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec 100 80000
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 280000 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec 80100 80000
HyoutaTools tlzc -c chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000
del chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_1440_020.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.new
move /Y chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.new chara.svo.ext\EP_1440_020.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\EP_1440_020.DAT.dec -a 0x80 chara.svo.ext\EP_1440_020.DAT.dec.ext chara.svo.ext\EP_1440_020.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\EP_1440_020.DAT.dec.new chara.svo.ext\EP_1440_020.DAT
del chara.svo.ext\EP_1440_020.DAT.dec
del chara.svo.ext\EP_1440_020.DAT.dec.new
rd /S /Q chara.svo.ext\EP_1440_020.DAT.dec.ext




REM copies the EN "and they were never heard from again" images into the two JP instances
HyoutaTools tlzc -d chara.svo.ext\GAMEOVER.DAT chara.svo.ext\GAMEOVER.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\GAMEOVER.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\GAMEOVER.DAT.dec.ext\0002 chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001 chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools BlockCopy chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec 55D80 chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec D80 55000
HyoutaTools BlockCopy chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec 55D80 chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec AAD80 55000
HyoutaTools tlzc -c chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001
del chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tovfps4p -o chara.svo.ext\GAMEOVER.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.new
move /Y chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.new chara.svo.ext\GAMEOVER.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\GAMEOVER.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\GAMEOVER.DAT.dec -a 0x80 chara.svo.ext\GAMEOVER.DAT.dec.ext chara.svo.ext\GAMEOVER.DAT.new
HyoutaTools tlzc -c chara.svo.ext\GAMEOVER.DAT.new chara.svo.ext\GAMEOVER.DAT
rd /S /Q chara.svo.ext\GAMEOVER.DAT.dec.ext
del chara.svo.ext\GAMEOVER.DAT.dec
del chara.svo.ext\GAMEOVER.DAT.new


REM copies over the custom title screen copyright at the bottom and the press start graphic
HyoutaTools tlzc -d chara.svo.ext\TITLE.DAT chara.svo.ext\TITLE.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\TITLE.DAT.dec chara.svo.ext\TITLE.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\TITLE.DAT.dec.ext\0002 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec
HyoutaTools tovfps4e chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext -nometa
HyoutaTools BlockCopy assets\TITLE_CREDIT.TXV 0 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext\0101 8000 auto
HyoutaTools BlockCopy assets\TITLE_CREDIT.TXV 0 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext\0101 1D400 auto
HyoutaTools BlockCopy assets\TITLE_PUSH.TXV 0 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext\0101 2CB800 auto
HyoutaTools tovfps4p -o chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec -a 0x80 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.new
HyoutaTools tlzc -c chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.new chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003
rd /S /Q chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.ext
del chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec
del chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec.new
HyoutaTools tovfps4p -o chara.svo.ext\TITLE.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext chara.svo.ext\TITLE.DAT.dec.ext\0002.new
move /Y chara.svo.ext\TITLE.DAT.dec.ext\0002.new chara.svo.ext\TITLE.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\TITLE.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\TITLE.DAT.dec -a 0x80 chara.svo.ext\TITLE.DAT.dec.ext chara.svo.ext\TITLE.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\TITLE.DAT.dec.new chara.svo.ext\TITLE.DAT
rd /S /Q chara.svo.ext\TITLE.DAT.dec.ext
del chara.svo.ext\TITLE.DAT.dec
del chara.svo.ext\TITLE.DAT.dec.new




REM copies the EN dice minigame textures over the JP
HyoutaTools tlzc -d chara.svo.ext\POR_C.DAT chara.svo.ext\POR_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\POR_C.DAT.dec chara.svo.ext\POR_C.DAT.dec.ext -nometa
HyoutaTools tovfps4e chara.svo.ext\POR_C.DAT.dec.ext\0002 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026.dec
HyoutaTools BlockCopy chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026.dec 100 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026.dec 100100 100000
HyoutaTools tlzc -c chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026.dec chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026
del chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0026.dec
HyoutaTools tlzc -d chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027.dec
HyoutaTools BlockCopy chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027.dec 100 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027.dec 100100 100000
HyoutaTools tlzc -c chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027.dec chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027
del chara.svo.ext\POR_C.DAT.dec.ext\0002.ext\0027.dec
HyoutaTools tovfps4p -o chara.svo.ext\POR_C.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\POR_C.DAT.dec.ext\0002.ext chara.svo.ext\POR_C.DAT.dec.ext\0002.new
move /Y chara.svo.ext\POR_C.DAT.dec.ext\0002.new chara.svo.ext\POR_C.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\POR_C.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\POR_C.DAT.dec -a 0x80 chara.svo.ext\POR_C.DAT.dec.ext chara.svo.ext\POR_C.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\POR_C.DAT.dec.new chara.svo.ext\POR_C.DAT
del chara.svo.ext\POR_C.DAT.dec
del chara.svo.ext\POR_C.DAT.dec.new
rd /S /Q chara.svo.ext\POR_C.DAT.dec.ext



REM copies over the custom repede snowboarding instruction images
HyoutaTools tlzc -d chara.svo.ext\XRG_C.DAT chara.svo.ext\XRG_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\XRG_C.DAT.dec chara.svo.ext\XRG_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\XRG_C.DAT.dec.ext\0002 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 200000 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec 100 80000
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 280000 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec 80100 80000
HyoutaTools tlzc -c chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006
del chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec
HyoutaTools tovfps4p -o chara.svo.ext\XRG_C.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext chara.svo.ext\XRG_C.DAT.dec.ext\0002.new
move /Y chara.svo.ext\XRG_C.DAT.dec.ext\0002.new chara.svo.ext\XRG_C.DAT.dec.ext\0002
rd /S /Q chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext
HyoutaTools tovfps4p -o chara.svo.ext\XRG_C.DAT.dec -a 0x80 chara.svo.ext\XRG_C.DAT.dec.ext chara.svo.ext\XRG_C.DAT.dec.new
HyoutaTools tlzc -c chara.svo.ext\XRG_C.DAT.dec.new chara.svo.ext\XRG_C.DAT
del chara.svo.ext\XRG_C.DAT.dec
del chara.svo.ext\XRG_C.DAT.dec.new
rd /S /Q chara.svo.ext\XRG_C.DAT.dec.ext







HyoutaTools tlzc -d chara.svo.ext\EP_650_050.DAT chara.svo.ext\EP_650_050.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_650_050.DAT.dec chara.svo.ext\EP_650_050.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_650_050.DAT.dec.ext\0002 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec
HyoutaTools BlockCopy assets\E_A102_CREDIT-2.DAT 0 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec D0200 auto
HyoutaTools tlzc -c chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044
del chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec
HyoutaTools tovfps4p -o chara.svo.ext\EP_650_050.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext chara.svo.ext\EP_650_050.DAT.dec.ext\0002.new
rd /S /Q chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext
del chara.svo.ext\EP_650_050.DAT.dec.ext\0002
move /Y chara.svo.ext\EP_650_050.DAT.dec.ext\0002.new chara.svo.ext\EP_650_050.DAT.dec.ext\0002
HyoutaTools tovfps4p -o chara.svo.ext\EP_650_050.DAT.dec -a 0x80 chara.svo.ext\EP_650_050.DAT.dec.ext chara.svo.ext\EP_650_050.DAT.dec.new
rd /S /Q chara.svo.ext\EP_650_050.DAT.dec.ext
HyoutaTools tlzc -c chara.svo.ext\EP_650_050.DAT.dec.new chara.svo.ext\EP_650_050.DAT
del chara.svo.ext\EP_650_050.DAT.dec
del chara.svo.ext\EP_650_050.DAT.dec.new


rem ident with E_A102_CREDIT-2.DAT
rem chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec.ext\0002
HyoutaTools tlzc -d chara.svo.ext\NAM_C.DAT chara.svo.ext\NAM_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\NAM_C.DAT.dec chara.svo.ext\NAM_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\NAM_C.DAT.dec.ext\0002 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec
HyoutaTools BlockCopy assets\E_A102_CREDIT-2.DAT 0 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec D0200 auto
HyoutaTools tlzc -c chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019
del chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec
HyoutaTools tovfps4p -o chara.svo.ext\NAM_C.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext chara.svo.ext\NAM_C.DAT.dec.ext\0002.new
rd /S /Q chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext
del chara.svo.ext\NAM_C.DAT.dec.ext\0002
move /Y chara.svo.ext\NAM_C.DAT.dec.ext\0002.new chara.svo.ext\NAM_C.DAT.dec.ext\0002
HyoutaTools tovfps4p -o chara.svo.ext\NAM_C.DAT.dec -a 0x80 chara.svo.ext\NAM_C.DAT.dec.ext chara.svo.ext\NAM_C.DAT.dec.new
rd /S /Q chara.svo.ext\NAM_C.DAT.dec.ext
HyoutaTools tlzc -c chara.svo.ext\NAM_C.DAT.dec.new chara.svo.ext\NAM_C.DAT
del chara.svo.ext\NAM_C.DAT.dec
del chara.svo.ext\NAM_C.DAT.dec.new

rem chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec.ext\0002
HyoutaTools tlzc -d chara.svo.ext\ZZZ_C.DAT chara.svo.ext\ZZZ_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\ZZZ_C.DAT.dec chara.svo.ext\ZZZ_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\ZZZ_C.DAT.dec.ext\0002 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools BlockCopy assets\E_A102_CREDIT-2.DAT 0 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec D0200 auto
HyoutaTools tlzc -c chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000
del chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tovfps4p -o chara.svo.ext\ZZZ_C.DAT.dec.ext\0002 -a 0x80 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.new
rd /S /Q chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext
del chara.svo.ext\ZZZ_C.DAT.dec.ext\0002
move /Y chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.new chara.svo.ext\ZZZ_C.DAT.dec.ext\0002
HyoutaTools tovfps4p -o chara.svo.ext\ZZZ_C.DAT.dec -a 0x80 chara.svo.ext\ZZZ_C.DAT.dec.ext chara.svo.ext\ZZZ_C.DAT.dec.new
rd /S /Q chara.svo.ext\ZZZ_C.DAT.dec.ext
HyoutaTools tlzc -c chara.svo.ext\ZZZ_C.DAT.dec.new chara.svo.ext\ZZZ_C.DAT
del chara.svo.ext\ZZZ_C.DAT.dec
del chara.svo.ext\ZZZ_C.DAT.dec.new




REM =========== Gut EP_1320_060.DAT to get a custom YUR_C201 ============
call "chara_generate_yuri_c201.bat"
REM call "chara_generate_jud_c001_evt01.bat"
REM call "chara_generate_EST_C101A_EVT00.bat"
REM call "chara_generate_EST_C000_EVT00.bat"
REM call "chara_generate_RAV_C100C.bat"
REM call "chara_generate_RAV_C100B.bat"
REM pause


REM HyoutaTools tovfps4p -o chara.svo -a 0x800 chara.svo.ext chara.svo.new
HyoutaTools tovfps4p -a 0x800 -c "../Release/PS3/\u5171\u901A/chara.svo" chara.svo.ext chara.svo.new
HyoutaTools BlockCopy chara.svo 14 chara.svo.new 14 4

REM pause

rd /S /Q chara.svo.ext

mkdir new
move chara.svo.new new\chara.svo