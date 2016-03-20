HyoutaTools tovfps4e UI.svo

copy /b assets\TOWNMAPXGUD.TXM UI.svo.ext\TOWNMAPXGUD.TXM
copy /b assets\TOWNMAPXGUD.TXV UI.svo.ext\TOWNMAPXGUD.TXV
copy /b assets\TOWNMAPXLAD.TXM UI.svo.ext\TOWNMAPXLAD.TXM
copy /b assets\TOWNMAPXLAD.TXV UI.svo.ext\TOWNMAPXLAD.TXV
copy /b assets\TOWNMAPXPTD.TXM UI.svo.ext\TOWNMAPXPTD.TXM
copy /b assets\TOWNMAPXPTD.TXV UI.svo.ext\TOWNMAPXPTD.TXV
copy /b assets\TOWNMAPXTCT.TXM UI.svo.ext\TOWNMAPXTCT.TXM
copy /b assets\TOWNMAPXTCT.TXV UI.svo.ext\TOWNMAPXTCT.TXV
copy /b assets\TOWNMAPXTMD.TXM UI.svo.ext\TOWNMAPXTMD.TXM
copy /b assets\TOWNMAPXTMD.TXV UI.svo.ext\TOWNMAPXTMD.TXV
copy /b assets\FONTTEX10.txv UI.svo.ext\FONTTEX10.TXV
copy /b assets\FONTTEX11.txv UI.svo.ext\FONTTEX11.TXV
copy /b assets\FONTTEX12.txv UI.svo.ext\FONTTEX12.TXV
copy /b assets\MINIGAMESNOW.TXM UI.svo.ext\MINIGAMESNOW.TXM
copy /b assets\MINIGAMESNOW.TXV UI.svo.ext\MINIGAMESNOW.TXV

copy /b UI.svo.ext\MINIGAMEISHI_E.TXV UI.svo.ext\MINIGAMEISHI.TXV
copy /b UI.svo.ext\EVENTMAP_E.TXV UI.svo.ext\EVENTMAP.TXV
HyoutaTools BlockCopy UI.svo.ext\MINIGAMEPOKER_E.TXV 0 UI.svo.ext\MINIGAMEPOKER.TXV 0 6AB900
HyoutaTools BlockCopy UI.svo.ext\MINIGAMEPOKER_E.TXV 14519E0 UI.svo.ext\MINIGAMEPOKER.TXV 14519E0 5E620

HyoutaTools tovfps4p -o UI.svo -a 0x800 UI.svo.ext UI.svo.new

rd /S /Q UI.svo.ext

mkdir new
move UI.svo.new new\UI.svo
