mkdir new
mkdir new\patches
rd /S /Q new\patches\scenario
mkdir new\patches\scenario

mkdir scenario.dat.ext.ex
mkdir new\scenario.dat.ext.ex
HyoutaTools Tales.Vesperia.Scenario.Extract scenario.dat scenario.dat.ext
HyoutaTools Tales.Vesperia.Scenario.Extract new\scenario.dat new\scenario.dat.ext


FOR %%G IN (scenario.dat.ext\*) DO (
comptoe -d scenario.dat.ext\%%~nG scenario.dat.ext.ex\%%~nG
comptoe -d new\scenario.dat.ext\%%~nG new\scenario.dat.ext.ex\%%~nG

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "scenario.dat.ext.ex\%%~nxG" "new\scenario.dat.ext.ex\%%~nxG" "new\patches\scenario\%%~nxG.xdelta3" 

del /F scenario.dat.ext.ex\%%~nG
del /F new\scenario.dat.ext.ex\%%~nG
)

rd /S /Q new\scenario.dat.ext.ex
rd /S /Q scenario.dat.ext.ex
rd /S /Q new\scenario.dat.ext
rd /S /Q scenario.dat.ext

