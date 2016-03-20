rd /S /Q "new\scenario.dat.ext.raw"

HyoutaTools Tales.Vesperia.Scenario.Extract new\scenario.dat "new\scenario.dat.ext.raw"

rd /S /Q "d:\Dropbox\ToV\PS3\mod\scenario.dat.ext"
mkdir "d:\Dropbox\ToV\PS3\mod\scenario.dat.ext"

FOR %%G IN (new\scenario.dat.ext.raw\*) DO (
comptoe -d "new\scenario.dat.ext.raw\%%~nG" "d:\Dropbox\ToV\PS3\mod\scenario.dat.ext\%%~nG.d"
)

copy /b "d:\Dropbox\ToV\PS3\mod\scenario.dat.ext\0.d" "d:\Dropbox\ToV\PS3\mod\scenario0"

rd /S /Q "new\scenario.dat.ext.raw"
