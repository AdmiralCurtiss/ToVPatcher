mkdir new\patches\chara

HyoutaTools tovfps4e chara.svo
HyoutaTools tovfps4e new\chara.svo

REM copies custom skill tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT chara.svo.ext\EP_050_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_050_010.DAT.dec chara.svo.ext\EP_050_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_050_010.DAT.dec.ext\0002 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec
HyoutaTools tlzc -d chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012 chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_050_010.DAT new\chara.svo.ext\EP_050_010.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_050_010.DAT.dec new\chara.svo.ext\EP_050_010.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002 new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009 new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010 new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011 new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012 new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec

mkdir new\patches\chara\EP_050_010\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec" "new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0009.dec" "new\patches\chara\EP_050_010\0002\0009.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec" "new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0010.dec" "new\patches\chara\EP_050_010\0002\0010.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec" "new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0011.dec" "new\patches\chara\EP_050_010\0002\0011.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec" "new\chara.svo.ext\EP_050_010.DAT.dec.ext\0002.ext\0012.dec" "new\patches\chara\EP_050_010\0002\0012.xdelta3" 




REM copies custom cooking tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT chara.svo.ext\EP_060_040.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_060_040.DAT.dec chara.svo.ext\EP_060_040.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_060_040.DAT.dec.ext\0002 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec
HyoutaTools tlzc -d chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004 chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_060_040.DAT new\chara.svo.ext\EP_060_040.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_060_040.DAT.dec new\chara.svo.ext\EP_060_040.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002 new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001 new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002 new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003 new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004 new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec

mkdir new\patches\chara\EP_060_040\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec" "new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0001.dec" "new\patches\chara\EP_060_040\0002\0001.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec" "new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0002.dec" "new\patches\chara\EP_060_040\0002\0002.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec" "new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0003.dec" "new\patches\chara\EP_060_040\0002\0003.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec" "new\chara.svo.ext\EP_060_040.DAT.dec.ext\0002.ext\0004.dec" "new\patches\chara\EP_060_040\0002\0004.xdelta3" 




REM skill tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT chara.svo.ext\EP_0080_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_0080_010.DAT.dec chara.svo.ext\EP_0080_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_0080_010.DAT.dec.ext\0002 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002 chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_0080_010.DAT new\chara.svo.ext\EP_0080_010.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_0080_010.DAT.dec new\chara.svo.ext\EP_0080_010.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002 new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000 new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001 new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec
HyoutaTools tlzc -d new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002 new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec

mkdir new\patches\chara\EP_0080_010\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec" "new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0000.dec" "new\patches\chara\EP_0080_010\0002\0000.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec" "new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0001.dec" "new\patches\chara\EP_0080_010\0002\0001.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec" "new\chara.svo.ext\EP_0080_010.DAT.dec.ext\0002.ext\0002.dec" "new\patches\chara\EP_0080_010\0002\0002.xdelta3" 





REM snowboarding minigame tutorial images
HyoutaTools tlzc -d chara.svo.ext\EP_1440_010.DAT chara.svo.ext\EP_1440_010.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_1440_010.DAT.dec chara.svo.ext\EP_1440_010.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_1440_010.DAT.dec.ext\0002 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_1440_010.DAT new\chara.svo.ext\EP_1440_010.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_1440_010.DAT.dec new\chara.svo.ext\EP_1440_010.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_1440_010.DAT.dec.ext\0002 new\chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000 new\chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec

mkdir new\patches\chara\EP_1440_010\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec" "new\chara.svo.ext\EP_1440_010.DAT.dec.ext\0002.ext\0000.dec" "new\patches\chara\EP_1440_010\0002\0000.xdelta3" 




HyoutaTools tlzc -d chara.svo.ext\EP_1440_020.DAT chara.svo.ext\EP_1440_020.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_1440_020.DAT.dec chara.svo.ext\EP_1440_020.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_1440_020.DAT.dec.ext\0002 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000 chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_1440_020.DAT new\chara.svo.ext\EP_1440_020.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_1440_020.DAT.dec new\chara.svo.ext\EP_1440_020.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_1440_020.DAT.dec.ext\0002 new\chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000 new\chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec

mkdir new\patches\chara\EP_1440_020\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec" "new\chara.svo.ext\EP_1440_020.DAT.dec.ext\0002.ext\0000.dec" "new\patches\chara\EP_1440_020\0002\0000.xdelta3" 



REM copies over the custom title screen copyright at the bottom and the press start graphic
HyoutaTools tlzc -d chara.svo.ext\TITLE.DAT chara.svo.ext\TITLE.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\TITLE.DAT.dec chara.svo.ext\TITLE.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\TITLE.DAT.dec.ext\0002 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003 chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec

HyoutaTools tlzc -d new\chara.svo.ext\TITLE.DAT new\chara.svo.ext\TITLE.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\TITLE.DAT.dec new\chara.svo.ext\TITLE.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\TITLE.DAT.dec.ext\0002 new\chara.svo.ext\TITLE.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003 new\chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec

mkdir new\patches\chara\TITLE\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec" "new\chara.svo.ext\TITLE.DAT.dec.ext\0002.ext\0003.dec" "new\patches\chara\TITLE\0002\0003.xdelta3" 






REM copies over the custom repede snowboarding instruction images
HyoutaTools tlzc -d chara.svo.ext\XRG_C.DAT chara.svo.ext\XRG_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\XRG_C.DAT.dec chara.svo.ext\XRG_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\XRG_C.DAT.dec.ext\0002 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006 chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec

HyoutaTools tlzc -d new\chara.svo.ext\XRG_C.DAT new\chara.svo.ext\XRG_C.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\XRG_C.DAT.dec new\chara.svo.ext\XRG_C.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\XRG_C.DAT.dec.ext\0002 new\chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006 new\chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec

mkdir new\patches\chara\XRG_C\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec" "new\chara.svo.ext\XRG_C.DAT.dec.ext\0002.ext\0006.dec" "new\patches\chara\XRG_C\0002\0006.xdelta3" 






HyoutaTools tlzc -d chara.svo.ext\EP_650_050.DAT chara.svo.ext\EP_650_050.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\EP_650_050.DAT.dec chara.svo.ext\EP_650_050.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\EP_650_050.DAT.dec.ext\0002 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044 chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec

HyoutaTools tlzc -d new\chara.svo.ext\EP_650_050.DAT new\chara.svo.ext\EP_650_050.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\EP_650_050.DAT.dec new\chara.svo.ext\EP_650_050.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\EP_650_050.DAT.dec.ext\0002 new\chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044 new\chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec

mkdir new\patches\chara\EP_650_050\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec" "new\chara.svo.ext\EP_650_050.DAT.dec.ext\0002.ext\0044.dec" "new\patches\chara\EP_650_050\0002\0044.xdelta3" 


HyoutaTools tlzc -d chara.svo.ext\NAM_C.DAT chara.svo.ext\NAM_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\NAM_C.DAT.dec chara.svo.ext\NAM_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\NAM_C.DAT.dec.ext\0002 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019 chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec

HyoutaTools tlzc -d new\chara.svo.ext\NAM_C.DAT new\chara.svo.ext\NAM_C.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\NAM_C.DAT.dec new\chara.svo.ext\NAM_C.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\NAM_C.DAT.dec.ext\0002 new\chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019 new\chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec

mkdir new\patches\chara\NAM_C\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec" "new\chara.svo.ext\NAM_C.DAT.dec.ext\0002.ext\0019.dec" "new\patches\chara\NAM_C\0002\0019.xdelta3" 


HyoutaTools tlzc -d chara.svo.ext\ZZZ_C.DAT chara.svo.ext\ZZZ_C.DAT.dec
HyoutaTools tovfps4e chara.svo.ext\ZZZ_C.DAT.dec chara.svo.ext\ZZZ_C.DAT.dec.ext
HyoutaTools tovfps4e chara.svo.ext\ZZZ_C.DAT.dec.ext\0002 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000 chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec

HyoutaTools tlzc -d new\chara.svo.ext\ZZZ_C.DAT new\chara.svo.ext\ZZZ_C.DAT.dec
HyoutaTools tovfps4e new\chara.svo.ext\ZZZ_C.DAT.dec new\chara.svo.ext\ZZZ_C.DAT.dec.ext
HyoutaTools tovfps4e new\chara.svo.ext\ZZZ_C.DAT.dec.ext\0002 new\chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext -nometa
HyoutaTools tlzc -d new\chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000 new\chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec

mkdir new\patches\chara\ZZZ_C\0002
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec" "new\chara.svo.ext\ZZZ_C.DAT.dec.ext\0002.ext\0000.dec" "new\patches\chara\ZZZ_C\0002\0000.xdelta3" 



rd /S /Q chara.svo.ext
rd /S /Q new\chara.svo.ext

