
mkdir new\patches
mkdir new\patches\UI

HyoutaTools tovfps4e UI.svo
HyoutaTools tovfps4e new\UI.svo

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXGUD.TXM" "new\UI.svo.ext\TOWNMAPXGUD.TXM" "new\patches\UI\TOWNMAPXGUD.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXGUD.TXV" "new\UI.svo.ext\TOWNMAPXGUD.TXV" "new\patches\UI\TOWNMAPXGUD.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXLAD.TXM" "new\UI.svo.ext\TOWNMAPXLAD.TXM" "new\patches\UI\TOWNMAPXLAD.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXLAD.TXV" "new\UI.svo.ext\TOWNMAPXLAD.TXV" "new\patches\UI\TOWNMAPXLAD.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXPTD.TXM" "new\UI.svo.ext\TOWNMAPXPTD.TXM" "new\patches\UI\TOWNMAPXPTD.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXPTD.TXV" "new\UI.svo.ext\TOWNMAPXPTD.TXV" "new\patches\UI\TOWNMAPXPTD.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXTCT.TXM" "new\UI.svo.ext\TOWNMAPXTCT.TXM" "new\patches\UI\TOWNMAPXTCT.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXTCT.TXV" "new\UI.svo.ext\TOWNMAPXTCT.TXV" "new\patches\UI\TOWNMAPXTCT.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXTMD.TXM" "new\UI.svo.ext\TOWNMAPXTMD.TXM" "new\patches\UI\TOWNMAPXTMD.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\TOWNMAPXTMD.TXV" "new\UI.svo.ext\TOWNMAPXTMD.TXV" "new\patches\UI\TOWNMAPXTMD.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\FONTTEX10.TXV" "new\UI.svo.ext\FONTTEX10.TXV" "new\patches\UI\FONTTEX10.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\FONTTEX11.TXV" "new\UI.svo.ext\FONTTEX11.TXV" "new\patches\UI\FONTTEX11.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\FONTTEX12.TXV" "new\UI.svo.ext\FONTTEX12.TXV" "new\patches\UI\FONTTEX12.TXV.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\MINIGAMESNOW.TXM" "new\UI.svo.ext\MINIGAMESNOW.TXM" "new\patches\UI\MINIGAMESNOW.TXM.xdelta3" 
xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "UI.svo.ext\MINIGAMESNOW.TXV" "new\UI.svo.ext\MINIGAMESNOW.TXV" "new\patches\UI\MINIGAMESNOW.TXV.xdelta3" 

rd /S /Q UI.svo.ext
rd /S /Q new\UI.svo.ext
