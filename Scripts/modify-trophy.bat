mkdir TrophyUnpack
TrophyUnpack TROPHY.TRP TrophyUnpack
HyoutaTools GraceNote.Trophy.TropSfmExport db\VTrophies db\GracesJapanese
copy /B newTrophy.SFM TrophyUnpack\TROP.SFM
copy /B newTrophyConf.SFM TrophyUnpack\TROPCONF.SFM
TrophyPack TROPHY.TRP.new TrophyUnpack\
del newTrophy.SFM
del newTrophyConf.SFM
rd /S /Q TrophyUnpack\

mkdir new
move TROPHY.TRP.new new\TROPHY.TRP
