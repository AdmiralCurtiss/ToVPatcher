HyoutaTools tovfps4e effect.svo

REM this is the SURPRISE ENCOUNTER! etc. text at the start of battle
HyoutaTools tlzc -d effect.svo.ext\E_A023.DAT effect.svo.ext\E_A023.DAT.dec
HyoutaTools tlzc -d effect.svo.ext\E_A034.DAT effect.svo.ext\E_A034.DAT.dec
HyoutaTools BlockCopy effect.svo.ext\E_A034.DAT.dec 100 effect.svo.ext\E_A023.DAT.dec 100 80000
HyoutaTools tlzc -c effect.svo.ext\E_A023.DAT.dec effect.svo.ext\E_A023.DAT
del effect.svo.ext\E_A023.DAT.dec
del effect.svo.ext\E_A034.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A104_GAMEOVER.DAT effect.svo.ext\E_A104_GAMEOVER.DAT.dec
HyoutaTools BlockCopy effect.svo.ext\E_A104_GAMEOVER.DAT.dec 55D80 effect.svo.ext\E_A104_GAMEOVER.DAT.dec D80 55000
HyoutaTools BlockCopy effect.svo.ext\E_A104_GAMEOVER.DAT.dec 55D80 effect.svo.ext\E_A104_GAMEOVER.DAT.dec AAD80 55000
HyoutaTools tlzc -c effect.svo.ext\E_A104_GAMEOVER.DAT.dec effect.svo.ext\E_A104_GAMEOVER.DAT
del effect.svo.ext\E_A104_GAMEOVER.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A101_TITLE.DAT effect.svo.ext\E_A101_TITLE.DAT.dec
HyoutaTools BlockCopy assets\TITLE_CREDIT.TXV 0 effect.svo.ext\E_A101_TITLE.DAT.dec 14D00 auto
HyoutaTools BlockCopy assets\TITLE_CREDIT.TXV 0 effect.svo.ext\E_A101_TITLE.DAT.dec 2A100 auto
HyoutaTools BlockCopy assets\TITLE_PUSH.TXV 0 effect.svo.ext\E_A101_TITLE.DAT.dec 2D8500 auto
HyoutaTools tlzc -c effect.svo.ext\E_A101_TITLE.DAT.dec effect.svo.ext\E_A101_TITLE.DAT
del effect.svo.ext\E_A101_TITLE.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_MG_STONERESULT01.DAT effect.svo.ext\E_MG_STONERESULT01.DAT.dec
HyoutaTools BlockCopy effect.svo.ext\E_MG_STONERESULT01.DAT.dec 100 effect.svo.ext\E_MG_STONERESULT01.DAT.dec 100100 100000
HyoutaTools tlzc -c effect.svo.ext\E_MG_STONERESULT01.DAT.dec effect.svo.ext\E_MG_STONERESULT01.DAT
del effect.svo.ext\E_MG_STONERESULT01.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_MG_STONERESULT02.DAT effect.svo.ext\E_MG_STONERESULT02.DAT.dec
HyoutaTools BlockCopy effect.svo.ext\E_MG_STONERESULT02.DAT.dec 100 effect.svo.ext\E_MG_STONERESULT02.DAT.dec 100100 100000
HyoutaTools tlzc -c effect.svo.ext\E_MG_STONERESULT02.DAT.dec effect.svo.ext\E_MG_STONERESULT02.DAT
del effect.svo.ext\E_MG_STONERESULT02.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_MG_SNOW_HOW_TO.DAT effect.svo.ext\E_MG_SNOW_HOW_TO.DAT.dec
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 200000 effect.svo.ext\E_MG_SNOW_HOW_TO.DAT.dec 100 80000
HyoutaTools BlockCopy assets\MINIGAMESNOW.TXV 280000 effect.svo.ext\E_MG_SNOW_HOW_TO.DAT.dec 80100 80000
HyoutaTools tlzc -c effect.svo.ext\E_MG_SNOW_HOW_TO.DAT.dec effect.svo.ext\E_MG_SNOW_HOW_TO.DAT
del effect.svo.ext\E_MG_SNOW_HOW_TO.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A031.DAT effect.svo.ext\E_A031.DAT.dec
HyoutaTools BlockCopy assets\E_A031_SKILL_01.TXV 0 effect.svo.ext\E_A031.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A031.DAT.dec effect.svo.ext\E_A031.DAT
del effect.svo.ext\E_A031.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A032.DAT effect.svo.ext\E_A032.DAT.dec
HyoutaTools BlockCopy assets\E_A032_SKILL_02_1.TXV 0 effect.svo.ext\E_A032.DAT.dec 180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_2.TXV 0 effect.svo.ext\E_A032.DAT.dec E1180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_3.TXV 0 effect.svo.ext\E_A032.DAT.dec 1C2180 auto
HyoutaTools BlockCopy assets\E_A032_SKILL_02_4.TXV 0 effect.svo.ext\E_A032.DAT.dec 2A3180 auto
HyoutaTools tlzc -c effect.svo.ext\E_A032.DAT.dec effect.svo.ext\E_A032.DAT
del effect.svo.ext\E_A032.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A033.DAT effect.svo.ext\E_A033.DAT.dec
HyoutaTools BlockCopy assets\E_A033_SKILL_03.TXV 0 effect.svo.ext\E_A033.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A033.DAT.dec effect.svo.ext\E_A033.DAT
del effect.svo.ext\E_A033.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A057.DAT effect.svo.ext\E_A057.DAT.dec
HyoutaTools BlockCopy assets\E_A057_COOK01.TXV 0 effect.svo.ext\E_A057.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A057.DAT.dec effect.svo.ext\E_A057.DAT
del effect.svo.ext\E_A057.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A058.DAT effect.svo.ext\E_A058.DAT.dec
HyoutaTools BlockCopy assets\E_A058_COOK02.TXV 0 effect.svo.ext\E_A058.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A058.DAT.dec effect.svo.ext\E_A058.DAT
del effect.svo.ext\E_A058.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A059.DAT effect.svo.ext\E_A059.DAT.dec
HyoutaTools BlockCopy assets\E_A059_COOK03.TXV 0 effect.svo.ext\E_A059.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A059.DAT.dec effect.svo.ext\E_A059.DAT
del effect.svo.ext\E_A059.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A060.DAT effect.svo.ext\E_A060.DAT.dec
HyoutaTools BlockCopy assets\E_A060_COOK04.TXV 0 effect.svo.ext\E_A060.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A060.DAT.dec effect.svo.ext\E_A060.DAT
del effect.svo.ext\E_A060.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A061.DAT effect.svo.ext\E_A061.DAT.dec
HyoutaTools BlockCopy assets\E_A061_COOK05.TXV 0 effect.svo.ext\E_A061.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A061.DAT.dec effect.svo.ext\E_A061.DAT
del effect.svo.ext\E_A061.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A063.DAT effect.svo.ext\E_A063.DAT.dec
HyoutaTools BlockCopy assets\E_A063_SKILL_NA01.TXV 0 effect.svo.ext\E_A063.DAT.dec 100 auto
HyoutaTools tlzc -c effect.svo.ext\E_A063.DAT.dec effect.svo.ext\E_A063.DAT
del effect.svo.ext\E_A063.DAT.dec

HyoutaTools tlzc -d effect.svo.ext\E_A102_CREDIT.DAT effect.svo.ext\E_A102_CREDIT.DAT.dec
HyoutaTools BlockCopy assets\E_A102_CREDIT-2.DAT 0 effect.svo.ext\E_A102_CREDIT.DAT.dec D0200 auto
HyoutaTools tlzc -c effect.svo.ext\E_A102_CREDIT.DAT.dec effect.svo.ext\E_A102_CREDIT.DAT
del effect.svo.ext\E_A102_CREDIT.DAT.dec

HyoutaTools tovfps4p -o effect.svo -a 0x800 effect.svo.ext effect.svo.new

rd /S /Q effect.svo.ext

mkdir new
move effect.svo.new new\effect.svo