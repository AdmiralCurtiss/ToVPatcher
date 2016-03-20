mkdir new\patches
cd new

copy /y NUL checksums.md5 >NUL
md5sum btl.svo >> checksums.md5
md5sum chara.svo >> checksums.md5
md5sum chat.svo >> checksums.md5
md5sum effect.svo >> checksums.md5
md5sum menu.svo >> checksums.md5
md5sum PARAM.SFO >> checksums.md5
md5sum scenario.dat >> checksums.md5
md5sum string.svo >> checksums.md5
md5sum TROPHY.TRP >> checksums.md5
md5sum UI.svo >> checksums.md5

copy "..\..\..\..\..\Tales of Vesperia Translation\Repack\required\ToV_modified.elf" ToV.elf
md5sum ToV.elf >> checksums.md5
del ToV.elf

move checksums.md5 patches\checksums.md5

cd..
