if exist string.svo   call "modify-string.bat"
if exist scenario.dat call "modify-scenario.bat"
if exist chat.svo     call "modify-chat.bat"
if exist btl.svo      call "modify-btl.bat"
if exist TROPHY.TRP   call "modify-trophy.bat"
if exist UI.svo       call "modify-ui.bat"
if exist effect.svo   call "modify-effect.bat"
if exist chara.svo    call "modify-chara.bat"
if exist menu.svo     call "modify-menu.bat"
if exist PARAM.SFO (
	copy /b PARAM.SFO new\PARAM.SFO
	HyoutaTools ByteFix new\PARAM.SFO 378-54 379-61 37A-6C 37B-65 37C-73 37D-20 37E-6F 37F-66 380-20 381-56 382-65 383-73 384-70 385-65 386-72 387-69 388-61 389-00 38A-00 38B-00 38C-00 38D-00 38E-00 38F-00 390-00 391-00 392-00 393-00 394-00 395-00 396-00 397-00 398-00 399-00 39A-00 39B-00 39C-00 39D-00 A8-12
)
