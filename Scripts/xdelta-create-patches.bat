
rd /S /Q new\patches
mkdir new\patches

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "string.svo" "new\string.svo" "new\patches\string.svo.xdelta3" 
call xdelta-create-patches-scenario.bat
call xdelta-create-patches-chat.bat
call xdelta-create-patches-ui.bat
REM xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo" "new\UI.svo" "new\patches\UI.svo.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "TROPHY.TRP" "new\TROPHY.TRP" "new\patches\TROPHY.TRP.xdelta3" 
call xdelta-create-patches-btl.bat
call xdelta-create-patches-chara.bat
call xdelta-create-patches-effect.bat
REM PARAM.SFO doesn't need xdelta patches, is just patched via ByteFix
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "menu.svo" "new\menu.svo" "new\patches\menu.svo.xdelta3" 

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "..\..\..\..\Tales of Vesperia Translation\Repack\required\ToV_original.elf" "..\..\..\..\Tales of Vesperia Translation\Repack\required\ToV_modified.elf" "new\patches\ToV.elf.xdelta3" 

